extern crate botnanars;
use botnanars::botnana::Botnana;
use std::{
    boxed::Box,
    ffi::{CStr, CString},
    os::raw::{c_char, c_void},
    str,
};

const VERSION: &'static str = env!("CARGO_PKG_VERSION");

#[no_mangle]
/// Rust Library Version
pub extern "C" fn rust_library_version() -> *const c_char {
    let version = CString::new(Botnana::version()).expect("rust library version");
    version.into_raw()
}

#[no_mangle]
/// Library Version
pub extern "C" fn library_version() -> *const c_char {
    let version = CString::new(VERSION).expect("library version");
    version.into_raw()
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

/// Clone Botnana
#[no_mangle]
pub extern "C" fn botnana_clone(botnana: Box<Botnana>) -> Box<Botnana> {
    let s = Box::into_raw(botnana);
    unsafe { Box::new((*s).clone()) }
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
        CString::new((*s).ip()).expect("botnana_ip").into_raw()
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
    let s = Box::into_raw(botnana);
    unsafe { (*s).send_message(message) };
}

/// attach function to tag
/// `count` = 0 : always call function if event is posted
#[no_mangle]
pub extern "C" fn botnana_set_tag_cb(
    botnana: Box<Botnana>,
    tag: *const c_char,
    count: u32,
    pointer: *mut c_void,
    cb: extern "C" fn(*mut c_void, *const c_char),
) -> i32 {
    if tag.is_null() {
        -1
    } else {
        let tag = unsafe { CStr::from_ptr(tag).to_str().unwrap() };
        let s = Box::into_raw(botnana);
        unsafe { (*s).set_tag_callback(&tag, count, pointer, cb) };
        0
    }
}

/// attach function to name of tag
/// `count` = 0 : always call function if event is posted
#[no_mangle]
pub extern "C" fn botnana_set_tagname_cb(
    botnana: Box<Botnana>,
    name: *const c_char,
    count: u32,
    pointer: *mut c_void,
    cb: extern "C" fn(*mut c_void, u32, u32, *const c_char),
) -> i32 {
    if name.is_null() {
        -1
    } else {
        let name = unsafe { CStr::from_ptr(name).to_str().unwrap() };
        let s = Box::into_raw(botnana);
        unsafe { (*s).set_tagname_callback(&name, count, pointer, cb) };
        0
    }
}

/// Set on_open callback
#[no_mangle]
pub extern "C" fn botnana_set_on_open_cb(
    botnana: Box<Botnana>,
    pointer: *mut c_void,
    cb: extern "C" fn(*mut c_void, *const c_char),
) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_open_cb(pointer, cb) };
}

/// Set on_error callback
#[no_mangle]
pub extern "C" fn botnana_set_on_error_cb(
    botnana: Box<Botnana>,
    pointer: *mut c_void,
    cb: extern "C" fn(*mut c_void, *const c_char),
) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_error_cb(pointer, cb) };
}

/// Set on_message callback
#[no_mangle]
pub extern "C" fn botnana_set_on_message_cb(
    botnana: Box<Botnana>,
    pointer: *mut c_void,
    cb: extern "C" fn(*mut c_void, *const c_char),
) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_message_cb(pointer, cb) };
}

/// Set on_send callback
#[no_mangle]
pub extern "C" fn botnana_set_on_send_cb(
    botnana: Box<Botnana>,
    pointer: *mut c_void,
    cb: extern "C" fn(*mut c_void, *const c_char),
) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_on_send_cb(pointer, cb) };
}

#[no_mangle]
/// Send script to buffer
pub extern "C" fn send_script_to_buffer(botnana: Box<Botnana>, script: *const c_char) -> i32 {
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
pub extern "C" fn flush_scripts_buffer(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).flush_scripts_buffer() };
}

#[no_mangle]
/// Set scripts pop count
pub fn set_scripts_pop_count(botnana: Box<Botnana>, count: u32) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_scripts_pop_count(count) };
}

#[no_mangle]
/// Set poll interval
pub fn set_poll_interval_ms(botnana: Box<Botnana>, interval: u64) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).set_poll_interval_ms(interval) };
}

/// abort porgram
#[no_mangle]
pub extern "C" fn botnana_abort_program(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).abort_program();
    };
}

/// Connect to modbus port
#[no_mangle]
pub extern "C" fn botnana_mb_connect(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).mb_connect();
    }
}

/// Get modbus discrete input at `addr`.
///
/// Return FALSE if `addr` is invalid.
#[no_mangle]
pub extern "C" fn botnana_mb_bit(botnana: Box<Botnana>, addr: usize) -> bool {
    let s = Box::into_raw(botnana);
    match unsafe { (*s).mb_bit(addr) } {
        Ok(value) => value,
        Err(_) => false,
    }
}

/// Get modbus signed 16bit integer at input register `addr`.
///
/// Return 0 if `addr` is invalid.
#[no_mangle]
pub extern "C" fn botnana_mb_i16(botnana: Box<Botnana>, addr: usize) -> i16 {
    let s = Box::into_raw(botnana);
    match unsafe { (*s).mb_i16(addr) } {
        Ok(value) => value,
        Err(_) => 0,
    }
}

/// Get modbus unsigned 16bit integer at input register `addr`.
///
/// Return 0 if `addr` is invalid.
#[no_mangle]
pub extern "C" fn botnana_mb_u16(botnana: Box<Botnana>, addr: usize) -> u16 {
    let s = Box::into_raw(botnana);
    match unsafe { (*s).mb_u16(addr) } {
        Ok(value) => value,
        Err(_) => 0,
    }
}

/// Get modbus signed 32bit integer at input register `addr`.
///
/// Return 0 if `addr` is invalid.
#[no_mangle]
pub extern "C" fn botnana_mb_i32(botnana: Box<Botnana>, addr: usize) -> i32 {
    let s = Box::into_raw(botnana);
    match unsafe { (*s).mb_i32(addr) } {
        Ok(value) => value,
        Err(_) => 0,
    }
}

/// Get modbus unsigned 32bit integer at input register `addr`.
///
/// Return 0 if `addr` is invalid.
#[no_mangle]
pub extern "C" fn botnana_mb_u32(botnana: Box<Botnana>, addr: usize) -> u32 {
    let s = Box::into_raw(botnana);
    match unsafe { (*s).mb_u32(addr) } {
        Ok(value) => value,
        Err(_) => 0,
    }
}
