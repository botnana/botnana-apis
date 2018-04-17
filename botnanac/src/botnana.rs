extern crate libc;
use std::thread;
use std::sync::{mpsc, Arc, Mutex};
use std::ffi::CStr;
use std::os::raw::c_char;
use std::str;
use websocket::{ClientBuilder, OwnedMessage};
use websocket::result::WebSocketError;
use std::collections::HashMap;

/// Botnana
#[repr(C)]
pub struct Botnana {
    sender: Option<mpsc::Sender<OwnedMessage>>,
    handlers: Arc<Mutex<HashMap<&'static str, Vec<Box<Fn(&str) + Send>>>>>,
    handler_counters: Arc<Mutex<HashMap<&'static str, Vec<u32>>>>,
}

/// hello botnana (test function)
#[no_mangle]
pub extern "C" fn hello_botnana() {
    println!("Hello Botnana");
}

/// connect to botnana
/// `address` of botnana
/// `msg_process` is botnana output message processor
#[no_mangle]
pub extern "C" fn connect_to_botnana(
    address: *const c_char,
    msg_processor: fn(*const c_char),
) -> Box<Botnana> {
    let address = unsafe {
        assert!(!address.is_null());
        str::from_utf8(CStr::from_ptr(address).to_bytes()).unwrap()
    };

    println!("connect to ws://{}", address);

    match ClientBuilder::new(&("ws://".to_owned() + address)) {
        Err(e) => {
            panic!("Err({})", e);
        }
        Ok(mut n) => {
            let (sender, receiver) = mpsc::channel();
            let botnana = Botnana {
                sender: Some(sender),
                handlers: Arc::new(Mutex::new(HashMap::new())),
                handler_counters: Arc::new(Mutex::new(HashMap::new())),
            };

            let (mut rx, mut tx) = n.connect_insecure().unwrap().split().unwrap();

            let handlers = Arc::clone(&botnana.handlers);
            let handler_counters = Arc::clone(&botnana.handler_counters);

            // 處理 botnana 傳過來的訊息
            thread::spawn(move || {
                for message in rx.incoming_messages() {
                    match message {
                        Ok(msg) => {
                            match msg {
                                OwnedMessage::Text(m) => {
                                    botnana_handle_message(&handlers, &handler_counters, &m);
                                    let mut msg = m.into_bytes();
                                    msg.push(0);
                                    let msg = CStr::from_bytes_with_nul(msg.as_slice())
                                        .expect("toCstr")
                                        .as_ptr();
                                    msg_processor(msg);
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

            Box::new(botnana)
        }
    }
}

fn botnana_handle_message(
    handlers: &Arc<Mutex<HashMap<&'static str, Vec<Box<Fn(&str) + Send>>>>>,
    handler_counters: &Arc<Mutex<HashMap<&'static str, Vec<u32>>>>,
    msg: &str,
) {
    if msg != "" {
        let lines: Vec<&str> = msg.split("\n").collect();
        let mut handlers = handlers.lock().unwrap();
        let mut handler_counters = handler_counters.lock().unwrap();
        for line in lines {
            let mut r: Vec<&str> = line.split("|").collect();
            let mut index: u32 = 0;
            let mut event = "";
            for e in r {
                println!("{}", e);
                if index % 2 == 0 {
                    event = e;
                } else {
                    let mut remove_list: Vec<usize> = Vec::new();
                    let mut idx = 0;
                    let mut counter_exist = false;
                    match handlers.get(event) {
                        Some(handler) => {
                            let counter = match handler_counters.get_mut(event) {
                                Some(c) => c,
                                None => {
                                    panic!("handler_counters.get_mut");
                                }
                            };
                            counter_exist = true;
                            for h in handler {
                                h(e);
                                if counter[idx] > 0 {
                                    counter[idx] -= 1;
                                    if counter[idx] == 0 {
                                        remove_list.push(idx);
                                        idx = idx + 1;
                                    }
                                }
                            }
                        }
                        None => {}
                    }
                    if counter_exist {
                        let counters = handler_counters.get_mut(event).unwrap();
                        let handlers = handlers.get_mut(event).unwrap();

                        for i in remove_list {
                            handlers.remove(i);
                            counters.remove(i);
                        }
                    }
                }
                index = index + 1;
            }
        }
    }
}

fn times<F>(
    handlers: &Arc<Mutex<HashMap<&'static str, Vec<Box<Fn(&str) + Send>>>>>,
    handler_counters: &Arc<Mutex<HashMap<&'static str, Vec<u32>>>>,
    event: &'static str,
    count: u32,
    handler: F,
) where
    F: Fn(&str) + Send + 'static,
{
    let mut handlers = handlers.lock().unwrap();
    let mut handler_counters = handler_counters.lock().unwrap();

    let h = handlers.entry(event).or_insert(Vec::new());
    let hc = handler_counters.entry(event).or_insert(Vec::new());

    h.push(Box::new(handler));
    hc.push(count);
}

/// Send message
#[no_mangle]
pub fn send_message(botnana: Box<Botnana>, msg: &str) {
    let botnana = Box::into_raw(botnana);
    let s = unsafe { &(*botnana).sender.clone() };
    match s {
        &Some(ref sender) => {
            let msg = OwnedMessage::Text(msg.to_string());
            sender.send(msg).expect("sender.send");
        }
        &None => {
            println!("No sender can find");
        }
    }
}

/// motion motion_evaluate
#[no_mangle]
pub fn motion_evaluate(botnana: Box<Botnana>, script: *const c_char) {
    let script = unsafe {
        assert!(!script.is_null());
        str::from_utf8(CStr::from_ptr(script).to_bytes()).unwrap()
    };
    let msg = r#"{"jsonrpc":"2.0","method":"motion.evaluate","params":{"script":""#.to_owned()
        + script + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// attach function to event
#[no_mangle]
pub fn attach_function_to_event(
    botnana: Box<Botnana>,
    event: *const c_char,
    count: libc::uint32_t,
    call: fn(&str),
) {
    let event = unsafe {
        assert!(!event.is_null());
        str::from_utf8(CStr::from_ptr(event).to_bytes()).unwrap()
    };

    let handlers = Arc::clone(&botnana.handlers);
    let handler_counters = Arc::clone(&botnana.handler_counters);
    times(&handlers, &handler_counters, event, count, call);
}
