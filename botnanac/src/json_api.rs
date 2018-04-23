extern crate libc;
use std::os::raw::c_char;
use std::ffi::CStr;
use botnana::Botnana;
use std::str;
use botnana::{evaluate, send_message};

/// motion_evaluate
#[no_mangle]
pub extern "C" fn botnana_motion_evaluate(botnana: Box<Botnana>, script: *const c_char) {
    let script = unsafe {
        assert!(!script.is_null());
        str::from_utf8(CStr::from_ptr(script).to_bytes()).unwrap()
    };
    evaluate(botnana, &script.to_owned());
}

/// get slave information
#[no_mangle]
pub extern "C" fn botnana_get_slave(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"ethercat.slave.get","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// get slave diff information
#[no_mangle]
pub extern "C" fn botnana_get_slave_diff(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"ethercat.slave.get_diff","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// set slave
#[no_mangle]
pub extern "C" fn botnana_set_slave(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    tag: *const c_char,
    value: libc::int32_t,
) {
    let tag = unsafe {
        assert!(!tag.is_null());
        str::from_utf8(CStr::from_ptr(tag).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"ethercat.slave.set","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#","tag":""# + tag + r#"","value":"#
        + value.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// set slave config
#[no_mangle]
pub extern "C" fn botnana_set_slave_config(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    tag: *const c_char,
    value: libc::int32_t,
) {
    let tag = unsafe {
        assert!(!tag.is_null());
        str::from_utf8(CStr::from_ptr(tag).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.set_slave","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#","tag":""# + tag + r#"","value":"#
        + value.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// get version
#[no_mangle]
pub extern "C" fn botnana_get_version(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"version.get"}"#;
    send_message(botnana, &msg.to_owned());
}

/// save config
#[no_mangle]
pub extern "C" fn botnana_save_config(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.save"}"#;
    send_message(botnana, &msg.to_owned());
}
