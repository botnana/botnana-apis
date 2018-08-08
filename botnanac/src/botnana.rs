use std::thread;
use std::sync::{mpsc, Arc, Mutex};
use std::ffi::CStr;
use std::os::raw::c_char;
use std::str;
use std::collections::HashMap;
use std;
use std::boxed::Box;
use ws;
use ws::{connect, Error, ErrorKind, Handler, Handshake, Message, Result};
use libc;
use url;

/// Botnana
#[repr(C)]
#[derive(Clone)]
pub struct Botnana {
    user_sender: mpsc::Sender<Message>,
    handlers: Arc<Mutex<HashMap<String, Vec<Box<Fn(*const c_char) + Send>>>>>,
    handler_counters: Arc<Mutex<HashMap<String, Vec<u32>>>>,
    debug_callback: Arc<Mutex<Vec<Box<Fn(*const c_char) + Send>>>>,
}

impl Botnana {
    /// connect to botnana
    /// `address` is IP of botnana
    ///  `processor` is Client message processor
    pub fn connect(address: &str, processor: fn(*const c_char)) -> Botnana {
        let url = match url::Url::parse(&("ws://".to_owned() + address + ":3012")) {
            Err(e) => {
                panic!("URL Error: {}", e);
            }
            Ok(url) => url,
        };

        // 從 user thread 送到 ws client thread
        let (user_sender, client_receiver) = mpsc::channel();
        // 從 ws client thread 送到 user thread
        let (client_sender, user_receiver) = mpsc::channel();

        let botnana = Botnana {
            user_sender: user_sender,
            handlers: Arc::new(Mutex::new(HashMap::new())),
            handler_counters: Arc::new(Mutex::new(HashMap::new())),
            debug_callback: Arc::new(Mutex::new(Vec::with_capacity(1))),
        };

        // 用來傳送 ws::sender
        let (thread_tx, thread_rx) = mpsc::channel();

        // Run client thread with channel to give it's WebSocket message sender back to us
        if let Err(e) = thread::Builder::new()
            .name("WS_CLIENT".to_string())
            .spawn(move || {
                println!("Connecting to {:?}", url.clone());

                // connect ws server
                if let Err(e) = connect(url.to_string(), |sender| Client {
                    ws_out: sender,
                    sender: client_sender.clone(),
                    thread_tx: thread_tx.clone(),
                }) {
                    panic!("Can't connect to WebSocket Server ({})", e);
                }
            }) {
            panic!("Can't create WS_CLIENT thread ({})", e);
        }

        // 等待 WS 連線後，將 ws_sender 回傳
        if let Ok(ws_sender) = thread_rx.recv() {
            let ws_out = ws_sender.clone();
            // 使用 thread 處理 user 傳過來的 message，透過 ws 送到 botnana
            thread::spawn(move || {
                loop {
                    // 如果從 mpsc channel 接收到 user 傳過來的指令，就透過 WebSocket 送到 Server
                    if let Ok(msg) = client_receiver.recv() {
                        if let Err(e) = ws_out.send(msg) {
                            eprintln!("Can't send message via WebSocket {}", e);
                        }
                    }
                }
            });

            let mut bna = botnana.clone();
            // 使用 thread 處理 WebSocket Client 透過 mpsc 傳過來的 message
            if let Err(e) = thread::Builder::new()
                .name("MESSAGE_PROCESSOR".to_string())
                .spawn(move || {
                    loop {
                        if let Ok(msg) = user_receiver.recv() {
                            let msg = msg.trim_left().trim_left_matches('|');
                            bna.handle_message(msg);
                            // 將 &str 轉 *const c_char
                            // 將結尾強制放入一個 0 , 讓from_bytes_with_nul成功
                            let mut msg1 = String::from(msg).into_bytes();
                            msg1.push(0);
                            processor(
                                CStr::from_bytes_with_nul(msg1.as_slice())
                                    .expect("toCstr")
                                    .as_ptr(),
                            );
                        }
                    }
                }) {
                panic!("Can't create MESSAGE_PROCESSOR thread ({})", e);
            }

            // poll thread
            // 不使用 ws::Handler on_timeout 的原因是在測試時產生 timeout 事件最少需要 200 ms
            if let Err(e) = thread::Builder::new()
                .name("POLL".to_string())
                .spawn(move || {
                    let poll_msg = Message::Text(
                        "{\"jsonrpc\":\"2.0\",\"method\":\"motion.poll\"}".to_owned(),
                    );
                    loop {
                        thread::sleep(std::time::Duration::from_millis(100));
                        if let Err(e) = ws_sender.send(poll_msg.clone()) {
                            eprintln!("Can't send message via WebSocket {}", e);
                        }
                    }
                }) {
                panic!("Can't create POLL thread ({})", e);
            }
        }
        botnana
    }

    /// send message to mpsc channel
    pub fn send_message(&mut self, msg: &str) {
        let callback = self.debug_callback.lock().unwrap();
        if callback.len() > 0 {
            let mut temp_msg = String::from(msg.clone()).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();

            callback[0](msg);
        }
        self.user_sender
            .send(Message::Text(msg.to_string()))
            .expect("send_message");
    }

    /// handle message
    fn handle_message(&mut self, message: &str) {
        let lines: Vec<&str> = message.split("\n").collect();
        let mut handlers = self.handlers.lock().expect("self.handlers.lock()");
        let mut handler_counters = self.handler_counters
            .lock()
            .expect("self.handler_counters.lock()");

        for line in lines {
            let mut r: Vec<&str> = line.split("|").collect();
            let mut index = 0;
            let mut event = "";
            for e in r {
                if index % 2 == 0 {
                    event = e;
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

    pub fn set_debug_callback<F>(&mut self, handler: F)
    where
        F: Fn(*const c_char) + Send + 'static,
    {
        let mut callback = self.debug_callback.lock().unwrap();
        // 移除原來的, 如果原本是空的會回傳 Error
        let _output = callback.pop();
        // 放入新的 callback function
        callback.push(Box::new(handler));
    }
}

/// WebSocket Client
struct Client {
    ws_out: ws::Sender,
    sender: mpsc::Sender<String>,
    thread_tx: mpsc::Sender<ws::Sender>,
}

impl Handler for Client {
    /// on_open
    fn on_open(&mut self, _: Handshake) -> Result<()> {
        self.thread_tx.send(self.ws_out.clone()).map_err(|err| {
            Error::new(
                ErrorKind::Internal,
                format!("Unable to communicate between threads: {:?}.", err),
            )
        })
    }

    /// on_message
    fn on_message(&mut self, msg: Message) -> Result<()> {
        if let Message::Text(m) = msg {
            // 資料長度 > 0 送進 mpsc::channel
            if m.len() > 0 {
                self.sender.send(m).expect("Client::on_message");
            }
        } else {
            eprintln!("invalid message format!");
        }
        Ok(())
    }
}

/// Send message
pub fn send_message(botnana: Box<Botnana>, msg: &str) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).send_message(msg) };
}

/// evaluate
pub fn evaluate(botnana: Box<Botnana>, script: &str) {
    let msg = r#"{"jsonrpc":"2.0","method":"motion.evaluate","params":{"script":""#.to_owned()
        + script + r#""}}"#;
    send_message(botnana, &msg);
}

/// connect to botnana
/// `address` of botnana
/// `msg_process` is botnana output message processor
#[no_mangle]
pub extern "C" fn botnana_connect(
    address: *const c_char,
    msg_processor: fn(*const c_char),
) -> Box<Botnana> {
    let address = unsafe {
        assert!(!address.is_null());
        str::from_utf8(CStr::from_ptr(address).to_bytes()).unwrap()
    };

    Box::new(Botnana::connect(address, msg_processor))
}

/// attach function to event
/// `count` = 0 : always call function if event is posted
#[no_mangle]
pub extern "C" fn botnana_attach_event(
    botnana: Box<Botnana>,
    event: *const c_char,
    count: libc::uint32_t,
    processor: fn(*const c_char),
) {
    let event = unsafe {
        assert!(!event.is_null());
        CStr::from_ptr(event).to_str().unwrap()
    };
    let s = Box::into_raw(botnana);
    unsafe { (*s).times(&event, count, processor) };
}

///增加 send message callback
#[no_mangle]
pub extern "C" fn botnana_set_debug_callback(botnana: Box<Botnana>, processor: fn(*const c_char)) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_debug_callback(processor) };
}

/// empty
#[no_mangle]
pub fn botnana_empty(botnana: Box<Botnana>) {
    evaluate(botnana, &r#"empty  marker empty"#.to_owned());
}

/// abort porgram
#[no_mangle]
pub extern "C" fn botnana_abort_program(botnana: Box<Botnana>) {
    evaluate(botnana, &r#"abort-program"#.to_owned());
}
