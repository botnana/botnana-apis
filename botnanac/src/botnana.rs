extern crate libc;
use std::thread;
use std::sync::{mpsc, Arc, Mutex};
use std::ffi::CStr;
use std::os::raw::c_char;
use std::str;
use websocket::{ClientBuilder, OwnedMessage};
use websocket::result::WebSocketError;
use std::collections::HashMap;
use std::time;
use std::boxed::Box;

/// Botnana
#[repr(C)]
#[derive(Clone)]
pub struct Botnana {
    debug: bool,
    sender: Option<mpsc::Sender<OwnedMessage>>,
    handlers: Arc<Mutex<HashMap<&'static str, Vec<Box<Fn(*const c_char) + Send>>>>>,
    handler_counters: Arc<Mutex<HashMap<&'static str, Vec<u32>>>>,
}

impl Botnana {
    /// connect to botnana
    /// `address` is IP of botnana
    ///  `processor` is Client message processor
    pub fn connect(address: &str, processor: fn(*const c_char)) -> Botnana {
        println!("connect to ws://{}:3012", address);

        match ClientBuilder::new(&("ws://".to_owned() + address + ":3012")) {
            Err(e) => {
                panic!("Address Error({})", e);
            }
            Ok(mut n) => {
                let (sender, receiver) = mpsc::channel();
                let botnana = Botnana {
                    debug: false,
                    sender: Some(sender),
                    handlers: Arc::new(Mutex::new(HashMap::new())),
                    handler_counters: Arc::new(Mutex::new(HashMap::new())),
                };

                let (mut rx, mut tx) = n.connect_insecure().unwrap().split().unwrap();

                let mut bnana = botnana.clone();

                // 處理 botnana 傳過來的訊息
                thread::spawn(move || {
                    for message in rx.incoming_messages() {
                        match message {
                            Ok(msg) => {
                                match msg {
                                    OwnedMessage::Text(m) => {
                                        //去除左邊的空白與第1個 "|"
                                        let msg = m.trim_left().trim_left_matches('|');
                                        // 有資料才處置
                                        if m != "" {
                                            bnana.handle_message(msg);
                                            // 將 &str 轉 *const c_char
                                            // 將結尾強制放入一個 0 , 讓from_bytes_with_nul成功
                                            let mut msg = String::from(msg).into_bytes();
                                            msg.push(0);
                                            processor(
                                                CStr::from_bytes_with_nul(msg.as_slice())
                                                    .expect("toCstr")
                                                    .as_ptr(),
                                            );
                                        }
                                    }
                                    _ => panic!("Invalid message {:?}", msg),
                                };
                            }
                            Err(e) => {
                                eprintln!("{}", e);
                            }
                        }
                    }
                });

                // 處理 thread 傳過來的訊息
                thread::spawn(move || loop {
                    let _error = match receiver.recv() {
                        Ok(msg) => tx.send_message(&msg),
                        Err(_) => Err(WebSocketError::NoDataAvailable),
                    };
                });

                // pool thread
                let mut btn = botnana.clone();
                thread::spawn(move || loop {
                    let poll_msg = "{\"jsonrpc\":\"2.0\",\"method\":\"motion.poll\"}".to_owned();
                    btn.send_message(&poll_msg.to_owned());
                    thread::sleep(time::Duration::from_millis(100));
                });

                botnana
            }
        }
    }

    // send message to botnana
    pub fn send_message(&mut self, msg: &str) {
        match self.sender {
            Some(ref sender) => {
                if self.debug {
                    match msg.find("motion.poll") {
                        Some(_x) => {}
                        None => println!("{}", &msg),
                    }
                }
                let msg = OwnedMessage::Text(msg.to_string());
                sender.send(msg).expect("sender.send");
            }
            None => {
                eprintln!("No sender can find");
            }
        }
    }

    /// handle message
    fn handle_message(&mut self, message: &str) {
        let lines: Vec<&str> = message.split("\n").collect();
        let mut handlers = self.handlers.lock().unwrap();
        let mut handler_counters = self.handler_counters.lock().unwrap();

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
        let mut handlers = self.handlers.lock().unwrap();
        let mut handler_counters = self.handler_counters.lock().unwrap();

        let h = handlers.entry(event).or_insert(Vec::new());
        let hc = handler_counters.entry(event).or_insert(Vec::new());

        h.push(Box::new(handler));
        hc.push(count);
    }

    /// enable debug
    pub fn enable_debug(&mut self) {
        self.debug = true;
    }

    /// disable debug
    pub fn disable_debug(&mut self) {
        self.debug = false;
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
        str::from_utf8(CStr::from_ptr(event).to_bytes()).unwrap()
    };
    let s = Box::into_raw(botnana);

    unsafe { (*s).times(&event, count, processor) };
}

/// enable debug
#[no_mangle]
pub extern "C" fn botnana_enable_debug(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).enable_debug();
    }
}

/// enable debug
#[no_mangle]
pub extern "C" fn botnana_disable_debug(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).disable_debug() };
}

/// empty
#[no_mangle]
pub fn botnana_empty(botnana: Box<Botnana>) {
    evaluate(botnana, &r#"empty  marker empty"#.to_owned());
}

/// abort porgram
#[no_mangle]
pub extern "C" fn botnana_abort_program(botnana: Box<Botnana>) {
    evaluate(botnana, &r#"kill-task0"#.to_owned());
}
