extern crate botnanars;
extern crate futures;
extern crate websocket;
extern crate libc;

use libc::c_char;
use std::ffi::CStr;
use std::str;
use futures::sync::mpsc;

#[no_mangle]
pub extern "C" fn start(connection: *const c_char) -> *mut mpsc::Sender<websocket::OwnedMessage> {
    let c_connection = unsafe {
        assert!(!connection.is_null());

        CStr::from_ptr(connection)
    };

    let r_connection = c_connection.to_str().unwrap();
    Box::into_raw(Box::new(botnanars::start(r_connection)))
}

#[no_mangle]
pub extern "C" fn poll(sender: *mut mpsc::Sender<websocket::OwnedMessage>) {
    let sender = unsafe {
        assert!(!sender.is_null());
        &mut *sender
    };
    botnanars::poll(sender);
}

#[no_mangle]
pub extern "C" fn sender_free(sender: *mut mpsc::Sender<websocket::OwnedMessage>) {
    if sender.is_null() { return }
        unsafe { Box::from_raw(sender); }
}

#[cfg(test)]
mod tests {
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
