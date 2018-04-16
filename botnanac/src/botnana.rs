use std::thread;
use std::sync::mpsc;
use std::ffi::CStr;
use std::os::raw::c_char;
use std::str;
use websocket::{ClientBuilder, OwnedMessage};
use websocket::result::WebSocketError;

/// Botnana
#[repr(C)]
pub struct Botnana {
    sender: Option<mpsc::Sender<OwnedMessage>>,
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
    msg_processor: fn(&str),
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
            };

            let (mut rx, mut tx) = n.connect_insecure().unwrap().split().unwrap();

            // 處理 botnana 傳過來的訊息
            thread::spawn(move || {
                for message in rx.incoming_messages() {
                    match message {
                        Ok(msg) => {
                            match msg {
                                OwnedMessage::Text(m) => {
                                    msg_processor(&m);
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

/// Send message
#[no_mangle]
pub fn send_message(botnana: Box<Botnana>, msg: &str) {
    let botnana = Box::into_raw(botnana);
    let s = unsafe { &(*botnana).sender };
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
