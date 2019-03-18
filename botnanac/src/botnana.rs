use libc;
use serde_json;
use std::{self,
          boxed::Box,
          collections::HashMap,
          ffi::CStr,
          os::raw::c_char,
          str,
          sync::{mpsc::{self, TryRecvError},
                 Arc, Mutex},
          thread,
          time::Duration};
use url;
use ws::{self, connect, util::Token, CloseCode, Error, ErrorKind, Handler, Handshake, Message,
         Result};

const WS_TIMEOUT_TOKEN: Token = Token(1);
const WS_WATCHDOG_PERIOD_MS: u64 = 2_000;

/// Botnana
#[repr(C)]
#[derive(Clone)]
pub struct Botnana {
    user_sender: mpsc::Sender<Message>,
    ws_out: Option<ws::Sender>,
    handlers: Arc<Mutex<HashMap<String, Vec<Box<Fn(*const c_char) + Send>>>>>,
    handler_counters: Arc<Mutex<HashMap<String, Vec<u32>>>>,
    on_error_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    on_send_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    on_message_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
}

impl Botnana {
    /// connect to botnana
    /// `address` is IP of botnana
    ///  `processor` is Client message processor
    pub fn connect(address: &str, on_error_cb: fn(*const c_char)) -> Option<Botnana> {
        // 從 user thread 送到 ws client thread，將指令透過 ws client thread 送到 motion server
        let (user_sender, client_receiver) = mpsc::channel();

        // 從 ws client thread 送到 user thread，將收到的資料送到 user thread
        let (client_sender, user_receiver) = mpsc::channel();

        // 從 pool thread 送到指令到 ws client thread，用來維持 WS 連線
        let (pool_sender, pool_receiver) = mpsc::channel();

        let mut botnana = Botnana {
            user_sender: user_sender,
            ws_out: None,
            handlers: Arc::new(Mutex::new(HashMap::new())),
            handler_counters: Arc::new(Mutex::new(HashMap::new())),
            on_error_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
            on_send_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
            on_message_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
        };

        botnana
            .on_error_cb
            .lock()
            .unwrap()
            .push(Box::new(on_error_cb));

        let url = match url::Url::parse(&("ws://".to_owned() + address + ":3012")) {
            Err(e) => {
                botnana.execute_on_error_cb(&format!("URL Error: {}\n", e));
                return Some(botnana);
            }
            Ok(url) => url,
        };

        // 用來傳送 ws::sender
        let (thread_tx, thread_rx) = mpsc::channel();

        let bna = botnana.clone();
        // Run client thread with channel to give it's WebSocket message sender back to us
        if let Err(e) = thread::Builder::new()
            .name("WS_CLIENT".to_string())
            .spawn(move || {
                println!("Connecting to {:?}", &url);

                // connect ws server
                if let Err(e) = connect(url.to_string(), |sender| Client {
                    ws_out: sender,
                    sender: client_sender.clone(),
                    thread_tx: thread_tx.clone(),
                    on_error_cb: bna.on_error_cb.clone(),
                    is_watchdog_refreshed: false,
                }) {
                    bna.execute_on_error_cb(&e.details.to_string());
                }
            })
        {
            botnana.execute_on_error_cb(&format!("Can't create WS CLIENT thread ({})\n", e));
        }

        // 等待 WS 連線後，將 ws_sender 回傳
        if let Ok(ws_sender) = thread_rx.recv_timeout(Duration::from_millis(10000)) {
            let ws_out = ws_sender.clone();
            // 使用 thread 處理 user 傳過來的 message，透過 ws 送到 botnana
            thread::spawn(move || {
                loop {
                    // 如果從 mpsc channel 接收到 user 傳過來的指令，就透過 WebSocket 送到 Server
                    if let Ok(msg) = client_receiver.recv() {
                        // 由 Client handler 處理錯誤
                        if ws_out.send(msg).is_err() {
                            break;
                        }
                    } else {
                        break;
                    }
                }
            });

            botnana.ws_out = Some(ws_sender.clone());
            let mut bna = botnana.clone();
            // 使用 thread 處理 WebSocket Client 透過 mpsc 傳過來的 message
            if let Err(e) = thread::Builder::new()
                .name("MESSAGE_PROCESSOR".to_string())
                .spawn(move || loop {
                    if let Ok(msg) = user_receiver.recv() {
                        let msg = msg.trim_start().trim_start_matches('|');
                        bna.handle_message(msg);
                    } else {
                        // 讓 user receiver 與 pool receiver 知道出現問題了
                        let _ = bna.user_sender.send(Message::Text(" ".to_string()));
                        let _ = pool_sender.send(Message::Text(" ".to_string()));
                        break;
                    }
                })
            {
                botnana.execute_on_error_cb(&format!(
                    "Can't create MESSAGE_PROCESSOR thread ({})\n",
                    e
                ));
            }

            let mut bna = botnana.clone();
            // poll thread
            // 不使用 ws::Handler on_timeout 的原因是在測試時產生 timeout 事件最少需要 200 ms
            if let Err(e) = thread::Builder::new()
                .name("POLL".to_string())
                .spawn(move || {
                    let poll_msg = Message::Text(
                        "{\"jsonrpc\":\"2.0\",\"method\":\"motion.poll\"}".to_owned(),
                    );
                    loop {
                        thread::sleep(std::time::Duration::from_millis(50));
                        // 當 WS 連線有問題時，用來接收通知，正常時應該不會收到東西
                        match pool_receiver.try_recv() {
                            Ok(_) | Err(TryRecvError::Disconnected) => {
                                break;
                            }
                            _ => {}
                        }

                        if ws_sender.send(poll_msg.clone()).is_err() {
                            break;
                        }
                    }
                })
            {
                bna.execute_on_error_cb(&format!("Can't create POLL thread ({})\n", e));
            }
        } else {
            botnana.execute_on_error_cb("Can't connect to WebSocket Server");
        }
        Some(botnana)
    }

    /// Disconnect
    pub fn disconnect(&mut self) {
        if let Some(ref mut ws_out) = self.ws_out {
            let _ = ws_out.close(CloseCode::Normal);
        }
    }

    /// send message to mpsc channel
    pub fn send_message(&mut self, msg: &str) {
        let cb = self.on_send_cb.lock().unwrap();
        if cb.len() > 0 && msg.len() > 0 {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();

            cb[0](msg);
        }
        if let Err(e) = self.user_sender.send(Message::Text(msg.to_string())) {
            self.execute_on_error_cb(&format!("Send Message Error: {}\n", e));
        }
    }

    /// handle message
    fn handle_message(&mut self, message: &str) {
        let cb = self.on_message_cb.lock().unwrap();
        if cb.len() > 0 && message.len() > 0 {
            let mut temp_msg = String::from(message).into_bytes();
            // 如果不是換行結束的,補上換行符號,如果沒有在 C 的輸出有問題
            if temp_msg[temp_msg.len() - 1] != 10 {
                temp_msg.push(10);
            }
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();
            cb[0](msg);
        }

        let lines: Vec<&str> = message.split("\n").collect();
        let mut handlers = self.handlers.lock().expect("self.handlers.lock()");
        let mut handler_counters = self
            .handler_counters
            .lock()
            .expect("self.handler_counters.lock()");

        for line in lines {
            let mut r: Vec<&str> = line.split("|").collect();
            let mut index = 0;
            let mut event = "";
            for e in r {
                if index % 2 == 0 {
                    event = e.trim_start();
                } else {
                    let mut remove_list = Vec::new();
                    let mut counter_exist = false;

                    match handlers.get(event) {
                        Some(handle) => {
                            counter_exist = true;
                            let counter = handler_counters.get_mut(event).unwrap();

                            let mut idx = 0;
                            for h in handle {
                                let mut msg = String::from(e).into_bytes();
                                msg.push(0);
                                let msg = CStr::from_bytes_with_nul(msg.as_slice())
                                    .expect("toCstr")
                                    .as_ptr();

                                h(msg);
                                if counter[idx] > 0 {
                                    counter[idx] -= 1;
                                    if counter[idx] == 0 {
                                        remove_list.push(idx);
                                    }
                                }
                                idx += 1;
                            }
                        }
                        None => {}
                    };

                    if counter_exist {
                        let counter = handler_counters.get_mut(event).unwrap();
                        let handler = handlers.get_mut(event).unwrap();

                        for i in &remove_list {
                            handler.remove(*i);
                            counter.remove(*i);
                        }
                    }
                }
                index += 1;
            }
        }
    }

    /// times
    /// `event` is event name
    /// `count` is handler called times
    /// `handler` is user function
    pub fn times<F>(&mut self, event: &'static str, count: u32, handler: F)
    where
        F: Fn(*const c_char) + Send + 'static,
    {
        let event = event.to_string();
        let mut handlers = self.handlers.lock().unwrap();
        let mut handler_counters = self.handler_counters.lock().unwrap();

        let h = handlers.entry(event.clone()).or_insert(Vec::new());
        let hc = handler_counters.entry(event).or_insert(Vec::new());
        h.push(Box::new(handler));
        hc.push(count);
    }

    /// Execute on_error callback
    fn execute_on_error_cb(&self, msg: &str) {
        let cb = self.on_error_cb.lock().unwrap();
        let mut temp_msg = String::from(msg).into_bytes();
        temp_msg.push(0);
        let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
            .expect("toCstr")
            .as_ptr();

        cb[0](msg);
    }

    pub fn set_on_send_cb<F>(&mut self, handler: F)
    where
        F: Fn(*const c_char) + Send + 'static,
    {
        let mut cb = self.on_send_cb.lock().unwrap();
        // 移除原來的, 如果原本是空的會回傳 Error
        let _output = cb.pop();
        // 放入新的 callback function
        cb.push(Box::new(handler));
    }

    pub fn set_on_message_cb<F>(&mut self, handler: F)
    where
        F: Fn(*const c_char) + Send + 'static,
    {
        let mut cb = self.on_message_cb.lock().unwrap();
        // 移除原來的, 如果原本是空的會回傳 Error
        let _output = cb.pop();
        // 放入新的 callback function
        cb.push(Box::new(handler));
    }
}

/// WebSocket Client
struct Client {
    ws_out: ws::Sender,
    sender: mpsc::Sender<String>,
    thread_tx: mpsc::Sender<ws::Sender>,
    on_error_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    is_watchdog_refreshed: bool,
}

impl Client {
    /// Execute on_error callback
    fn execute_on_error_cb(&self, msg: &str) {
        let cb = self.on_error_cb.lock().unwrap();
        let mut temp_msg = String::from(msg).into_bytes();
        temp_msg.push(0);
        let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
            .expect("toCstr")
            .as_ptr();

        cb[0](msg);
    }
}

impl Handler for Client {
    /// on_open
    fn on_open(&mut self, _: Handshake) -> Result<()> {
        self.ws_out
            .timeout(WS_WATCHDOG_PERIOD_MS, WS_TIMEOUT_TOKEN)?;
        self.thread_tx.send(self.ws_out.clone()).map_err(|err| {
            Error::new(
                ErrorKind::Internal,
                format!("Unable to communicate between threads: {:?}.", err),
            )
        })
    }

    /// on_message
    fn on_message(&mut self, msg: Message) -> Result<()> {
        self.is_watchdog_refreshed = true;
        if let Message::Text(m) = msg {
            // 資料長度 > 0 送進 mpsc::channel
            if m.len() > 0 {
                self.sender.send(m).expect("Client::on_message");
            }
        } else {
            unreachable!();
        }
        Ok(())
    }

    /// on error
    fn on_error(&mut self, err: Error) {
        self.execute_on_error_cb(&format!("{:?}\n", err));
    }

    /// on close
    fn on_close(&mut self, code: CloseCode, reason: &str) {
        self.execute_on_error_cb(&format!(
            "WS Client close code = {:?}, reason = {}\n",
            code, reason
        ));
    }

    /// Called when a timeout is triggered.
    fn on_timeout(&mut self, _event: Token) -> Result<()> {
        if !self.is_watchdog_refreshed {
            self.execute_on_error_cb(&format!("WS Client timeout!\n"));
            // 連線斷掉時，要用 shutdown，才能關掉。
            self.ws_out.shutdown()
        } else {
            self.is_watchdog_refreshed = false;
            self.ws_out.timeout(WS_WATCHDOG_PERIOD_MS, WS_TIMEOUT_TOKEN)
        }
    }
}

/// Send message
pub fn send_message(botnana: Box<Botnana>, msg: &str) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).send_message(msg) };
}

/// evaluate
pub fn evaluate(botnana: Box<Botnana>, script: &str) {
    match serde_json::to_value(script) {
        Ok(x) => {
            let msg = r#"{"jsonrpc":"2.0","method":"script.evaluate","params":{"script":"#
                .to_owned()
                + &x.to_string()
                + r#"}}"#;
            send_message(botnana, &msg);
        }
        _ => {
            unreachable!();
        }
    }
}

/// connect to botnana
/// `address` of botnana
/// `msg_process` is botnana output message processor
#[no_mangle]
pub extern "C" fn botnana_connect(
    address: *const c_char,
    on_ws_error_cb: fn(*const c_char),
) -> Box<Botnana> {
    let address = unsafe {
        assert!(!address.is_null());
        str::from_utf8(CStr::from_ptr(address).to_bytes()).unwrap()
    };

    if let Some(botnana) = Botnana::connect(address, on_ws_error_cb) {
        Box::new(botnana)
    } else {
        unreachable!()
    }
}

/// disconnection
/// `address` of botnana
#[no_mangle]
pub extern "C" fn botnana_disconnect(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).disconnect();
    }
}

/// Send Message
#[no_mangle]
pub extern "C" fn botnana_send_message(botnana: Box<Botnana>, msg: *const c_char) {
    let message = unsafe {
        assert!(!msg.is_null());
        str::from_utf8(CStr::from_ptr(msg).to_bytes()).unwrap()
    };
    send_message(botnana, message);
}

/// attach function to event
/// `count` = 0 : always call function if event is posted
#[no_mangle]
pub extern "C" fn botnana_set_tag_cb(
    botnana: Box<Botnana>,
    tag: *const c_char,
    count: libc::uint32_t,
    cb: fn(*const c_char),
) -> libc::int32_t {
    if tag.is_null() {
        -1
    } else {
        let tag = unsafe { CStr::from_ptr(tag).to_str().unwrap() };
        let s = Box::into_raw(botnana);
        unsafe { (*s).times(&tag, count, cb) };
        0
    }
}

/// Set on_message callback
#[no_mangle]
pub extern "C" fn botnana_set_on_message_cb(botnana: Box<Botnana>, cb: fn(*const c_char)) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_message_cb(cb) };
}

/// Set on_send callback
#[no_mangle]
pub extern "C" fn botnana_set_on_send_cb(botnana: Box<Botnana>, cb: fn(*const c_char)) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_send_cb(cb) };
}

/// abort porgram
#[no_mangle]
pub extern "C" fn botnana_abort_program(botnana: Box<Botnana>) {
    evaluate(botnana, &r#"abort-program"#.to_owned());
}
