use libc;
use serde_json;
use std::{self,
          boxed::Box,
          collections::{HashMap, VecDeque},
          ffi::{CStr, CString},
          os::raw::c_char,
          str,
          sync::{mpsc::{self, TryRecvError},
                 Arc, Mutex},
          thread};
use url;
use ws::{self, connect, util::Token, CloseCode, Error, ErrorKind, Handler, Handshake, Message,
         Result};
const WS_TIMEOUT_TOKEN: Token = Token(1);
const WS_WATCHDOG_PERIOD_MS: u64 = 10_000;
const AUTO_FLUSH_COUNT: u32 = 8;

/// Botnana
#[repr(C)]
#[derive(Clone)]
pub struct Botnana {
    url: String,
    user_sender: Option<mpsc::Sender<Message>>,
    ws_out: Option<ws::Sender>,
    handlers: Arc<Mutex<HashMap<String, Vec<Box<Fn(*const c_char) + Send>>>>>,
    handler_counters: Arc<Mutex<HashMap<String, Vec<u32>>>>,
    /// 用來存放 forth 命令的 buffer
    scripts_buffer: Arc<Mutex<VecDeque<String>>>,
    /// 收到 `auto_flush_count` 個暫存命令後，要送出給 Botnana motion server
    auto_flush_count: Arc<Mutex<u32>>,
    is_connecting: Arc<Mutex<bool>>,
    on_open_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    on_error_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    on_send_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    on_message_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
}

impl Botnana {
    /// New
    pub fn new() -> Botnana {
        Botnana {
            url: "ws://192.168.7.2:3012".to_string(),
            user_sender: None,
            ws_out: None,
            handlers: Arc::new(Mutex::new(HashMap::new())),
            handler_counters: Arc::new(Mutex::new(HashMap::new())),
            scripts_buffer: Arc::new(Mutex::new(VecDeque::new())),
            auto_flush_count: Arc::new(Mutex::new(AUTO_FLUSH_COUNT)),
            is_connecting: Arc::new(Mutex::new(false)),
            on_open_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
            on_error_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
            on_send_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
            on_message_cb: Arc::new(Mutex::new(Vec::with_capacity(1))),
        }
    }

    /// Set IP
    pub fn set_ip(&mut self, ip: &str) -> Option<&str> {
        if let Ok(url) = url::Url::parse(&("ws://".to_owned() + ip + ":3012")) {
            self.url = url.to_string();
            Some(self.url())
        } else {
            None
        }
    }

    /// URL
    pub fn url(&self) -> &str {
        &self.url
    }

    /// Set on_open callback
    pub fn set_on_open_cb<F>(&mut self, handler: F)
    where
        F: Fn(*const c_char) + Send + 'static,
    {
        let mut cb = self.on_open_cb.lock().expect("set_on_open_cb");
        // 移除原來的, 如果原本是空的會回傳 Error
        let _output = cb.pop();
        // 放入新的 callback function
        cb.push(Box::new(handler));
    }

    /// Set on_error callback
    pub fn set_on_error_cb<F>(&mut self, handler: F)
    where
        F: Fn(*const c_char) + Send + 'static,
    {
        let mut cb = self.on_error_cb.lock().expect("set_on_error_cb");
        // 移除原來的, 如果原本是空的會回傳 Error
        let _output = cb.pop();
        // 放入新的 callback function
        cb.push(Box::new(handler));
    }

    /// Connect to botnana
    fn connect(&mut self) {
        // 如果已經在等待連線就跳出
        if *self.is_connecting.lock().expect("connecting") {
            return;
        } else {
            *self.is_connecting.lock().expect("connecting") = true;
        }

        // 從 user thread 送到 ws client thread，將指令透過 ws client thread 送到 motion server
        let (user_sender, client_receiver) = mpsc::channel();

        // 從 ws client thread 送到 user thread，將收到的資料送到 user thread
        let (client_sender, user_receiver) = mpsc::channel();

        // 從 poll thread 送到指令到 ws client thread，用來維持 WS 連線
        let (poll_sender, poll_receiver) = mpsc::channel();

        // 用來傳送 ws::sender
        let (thread_tx, thread_rx) = mpsc::channel();

        self.user_sender = Some(user_sender);
        let mut botnana = self.clone();

        thread::Builder::new()
            .name("Try Connection".to_string())
            .spawn(move || {
                // 連線到 WS Server，因為 ws::connect 會被 block，所以要使用 thread 來連線才能執行後續的程序。
                let mut bna = botnana.clone();
                if let Err(e) =
                    thread::Builder::new()
                        .name("WS_CLIENT".to_string())
                        .spawn(move || {
                            // connect ws server
                            let _ = connect(bna.url.clone(), |sender| Client {
                                url: bna.url.clone(),
                                ws_out: sender,
                                sender: client_sender.clone(),
                                thread_tx: thread_tx.clone(),
                                on_open_cb: bna.on_open_cb.clone(),
                                on_error_cb: bna.on_error_cb.clone(),
                                is_watchdog_refreshed: false,
                            });
                            // 直到 WS Client Event loop 結束， 才會執行以下程式。
                            *bna.is_connecting.lock().expect("Try connecting") = false;
                            bna.ws_out = None;
                        })
                {
                    botnana
                        .execute_on_error_cb(&format!("Can't create WS CLIENT thread ({})\n", e));
                }

                // 等待 WS 連線後，將 ws_sender 回傳
                if let Ok(ws_sender) = thread_rx.recv() {
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
                                // 讓 client receiver 與 poll receiver 知道出現問題了
                                // 故意送任一個訊息，讓 user receiver 與 poll receiver 收到訊息
                                // 依 client receiver 與 poll receiver 的機制進行後續處理。
                                if let Some(sender) = bna.user_sender {
                                    let _ = sender.send(Message::Text(" ".to_string()));
                                }
                                let _ = poll_sender.send(Message::Text(" ".to_string()));
                                break;
                            }
                        })
                    {
                        botnana.execute_on_error_cb(&format!(
                            "Can't create MESSAGE_PROCESSOR thread ({})\n",
                            e
                        ));
                    }

                    // poll thread
                    // 不使用 ws::Handler on_timeout 的原因是在測試時產生 timeout 事件最少需要 200 ms
                    let mut bna = botnana.clone();
                    if let Err(e) =
                        thread::Builder::new()
                            .name("POLL".to_string())
                            .spawn(move || {
                                let poll_msg = Message::Text(
                                    "{\"jsonrpc\":\"2.0\",\"method\":\"motion.poll\"}".to_owned(),
                                );
                                loop {
                                    thread::sleep(std::time::Duration::from_millis(50));
                                    // 當 WS 連線有問題時，用來接收通知，正常時應該不會收到東西
                                    match poll_receiver.try_recv() {
                                        Ok(_) | Err(TryRecvError::Disconnected) => {
                                            break;
                                        }
                                        _ => {}
                                    }

                                    if ws_sender.send(poll_msg.clone()).is_ok() {
                                        bna.flush_scripts_buffer();
                                    } else {
                                        break;
                                    }
                                }
                            })
                    {
                        botnana.execute_on_error_cb(&format!("Can't create POLL thread ({})\n", e));
                    }
                }
            })
            .expect("Create Try Connection Thread");
    }

    /// Disconnect
    pub fn disconnect(&mut self) {
        if let Some(ref mut ws_out) = self.ws_out {
            let _ = ws_out.close(CloseCode::Normal);
        }
    }

    /// Send message to mpsc channel
    pub fn send_message(&mut self, msg: &str) {
        {
            let cb = self.on_send_cb.lock().unwrap();
            if cb.len() > 0 && msg.len() > 0 {
                let mut temp_msg = String::from(msg).into_bytes();
                temp_msg.push(0);
                let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                    .expect("toCstr")
                    .as_ptr();

                cb[0](msg);
            }
        }
        let mut error_info = Ok(());
        if let Some(ref sender) = self.user_sender {
            error_info = sender.send(Message::Text(msg.to_string()));
        }

        if let Err(e) = error_info {
            self.execute_on_error_cb(&format!("Send Message Error: {}\n", e));
        }
    }

    /// Evaluate (立即送出)
    pub fn evaluate(&mut self, script: &str) {
        if let Ok(x) = serde_json::to_value(script) {
            let msg = r#"{"jsonrpc":"2.0","method":"script.evaluate","params":{"script":"#
                .to_owned()
                + &x.to_string()
                + r#"}}"#;
            if let Some(ref sender) = self.user_sender {
                sender
                    .send(Message::Text(msg.to_string()))
                    .expect("send_message");
            }
        }
    }

    /// Send script to command buffer （將命令送到緩衝區）
    pub fn send_script_to_buffer(&mut self, script: &str) {
        let queues_len: u32;
        let queues_threshold: u32;
        {
            let mut queues = self.scripts_buffer.lock().expect("");
            queues.push_back(script.to_owned() + r#" "#);
            queues_len = queues.len() as u32;
            queues_threshold = *self.auto_flush_count.lock().expect("");
        }
        if queues_len >= queues_threshold {
            self.flush_scripts_buffer();
        }
    }

    /// Set auto flush count
    pub fn set_auto_flush_count(&mut self, count: u32) {
        *self.auto_flush_count.lock().unwrap() = count;
    }

    /// Flush scripts buffer (將緩衝區內的命令送出)
    pub fn flush_scripts_buffer(&mut self) {
        let mut msg = String::new();
        {
            let mut queues = self.scripts_buffer.lock().expect("");
            loop {
                match queues.pop_front() {
                    Some(x) => {
                        msg.push_str(&x);
                    }
                    None => break,
                }
            }
        }
        if msg.len() > 0 {
            self.evaluate(&msg);
        }
    }

    /// Handle message
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
    fn execute_on_error_cb(&mut self, msg: &str) {
        self.user_sender = None;
        self.ws_out = None;
        let cb = self.on_error_cb.lock().expect("execute_on_error_cb");
        if cb.len() > 0 {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();

            cb[0](msg);
        }
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
    url: String,
    ws_out: ws::Sender,
    sender: mpsc::Sender<String>,
    thread_tx: mpsc::Sender<ws::Sender>,
    on_open_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    on_error_cb: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
    is_watchdog_refreshed: bool,
}

impl Client {
    /// Execute on_open callback
    fn execute_on_open_cb(&self, msg: &str) {
        let cb = self.on_open_cb.lock().expect("execute_on_open_cb");
        if cb.len() > 0 {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();

            cb[0](msg);
        }
    }

    /// Execute on_error callback
    fn execute_on_error_cb(&self, msg: &str) {
        let cb = self.on_error_cb.lock().expect("execute_on_error_cb");
        if cb.len() > 0 {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();
            cb[0](msg);
        }
    }
}

impl Handler for Client {
    /// on_open
    fn on_open(&mut self, _: Handshake) -> Result<()> {
        self.ws_out
            .timeout(WS_WATCHDOG_PERIOD_MS, WS_TIMEOUT_TOKEN)?;
        self.execute_on_open_cb(&("Connect to ".to_owned() + &self.url));
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

/// Evaluate
pub fn evaluate(botnana: Box<Botnana>, script: &str) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).evaluate(script) };
}

#[no_mangle]
/// Send script to buffer
pub extern "C" fn send_script_to_buffer(
    botnana: Box<Botnana>,
    script: *const c_char,
) -> libc::int32_t {
    if script.is_null() {
        -1
    } else {
        let script = unsafe { String::from_utf8_lossy(&CStr::from_ptr(script).to_bytes()) };
        let s = Box::into_raw(botnana);
        unsafe { (*s).send_script_to_buffer(&(script.to_owned())) };
        0
    }
}

#[no_mangle]
/// Flush scripts buffer
pub extern "C" fn flush_scripts_buffer(botnana: Box<Botnana>) -> libc::int32_t {
    let s = Box::into_raw(botnana);
    unsafe { (*s).flush_scripts_buffer() };
    0
}

#[no_mangle]
/// Set auto flush count
pub fn set_auto_flush_count(botnana: Box<Botnana>, count: libc::uint32_t) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_auto_flush_count(count) };
}

/// New Botnana
/// `ip`: IP of botnana
#[no_mangle]
pub extern "C" fn botnana_new(ip: *const c_char) -> Box<Botnana> {
    let ip = unsafe {
        assert!(!ip.is_null());
        str::from_utf8(CStr::from_ptr(ip).to_bytes()).unwrap()
    };
    let mut botnana = Botnana::new();
    botnana.set_ip(ip);
    Box::new(botnana)
}

/// Set motion server IP
/// `ip`: IP of botnana
#[no_mangle]
pub extern "C" fn botnana_set_ip(botnana: Box<Botnana>, ip: *const c_char) -> *const c_char {
    let ip = unsafe {
        assert!(!ip.is_null());
        str::from_utf8(CStr::from_ptr(ip).to_bytes()).unwrap()
    };
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).set_ip(ip);
        let url = CString::new((*s).url()).expect("botnana_url");
        url.into_raw()
    }
}

/// Get URL of motion server
#[no_mangle]
pub extern "C" fn botnana_url(botnana: Box<Botnana>) -> *const c_char {
    let s = Box::into_raw(botnana);
    unsafe {
        let url = CString::new((*s).url()).expect("botnana_url");
        url.into_raw()
    }
}

/// Connection
/// `botnana`: Botnana descriptor
#[no_mangle]
pub extern "C" fn botnana_connect(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).connect();
    }
}

/// Disconnection
/// `botnana`: Botnana descriptor
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

/// Set on_open callback
#[no_mangle]
pub extern "C" fn botnana_set_on_open_cb(botnana: Box<Botnana>, cb: fn(*const c_char)) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_open_cb(cb) };
}

/// Set on_error callback
#[no_mangle]
pub extern "C" fn botnana_set_on_error_cb(botnana: Box<Botnana>, cb: fn(*const c_char)) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_error_cb(cb) };
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
