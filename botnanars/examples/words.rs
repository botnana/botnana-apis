extern crate botnanars;
use botnanars::Botnana;
use std::{ffi::CStr, str, thread, time};

static mut IS_OPENED: bool = false;

/// On ws open
fn on_ws_open(msg: *const i8) {
    let message = unsafe {
        assert!(!msg.is_null());
        str::from_utf8(CStr::from_ptr(msg).to_bytes()).unwrap()
    };
    unsafe {
        IS_OPENED = true;
    }
    println!("WS Open : {}", message);
}

/// On ws error
fn on_ws_error(msg: *const i8) {
    let message = unsafe {
        assert!(!msg.is_null());
        str::from_utf8(CStr::from_ptr(msg).to_bytes()).unwrap()
    };
    unsafe {
        IS_OPENED = false;
    }
    println!("WS Error : {}", message);
}

/// On ws message
fn on_ws_message(msg: *const i8) {
    let message = unsafe {
        assert!(!msg.is_null());
        str::from_utf8(CStr::from_ptr(msg).to_bytes()).unwrap()
    };
    println!("WS message : {}", message);
}

fn main() {
    let mut botnana = Botnana::new();

    botnana.set_on_open_cb(on_ws_open);
    botnana.set_on_error_cb(on_ws_error);
    botnana.set_on_message_cb(on_ws_message);
    botnana.connect();
    unsafe {
        while !IS_OPENED {
            thread::sleep(time::Duration::from_millis(1000));
        }
    }

    botnana.evaluate("words");

    loop {
        thread::sleep(time::Duration::from_millis(2000));
    }
}
