extern crate libc;
use std::os::raw::c_char;
use std::ffi::CStr;
use botnana::Botnana;
use std::str;
use botnana::{evaluate, send_message};

/// motion_evaluate
#[no_mangle]
pub extern "C" fn script_evaluate(botnana: Box<Botnana>, script: *const c_char) -> libc::int32_t {
    if script.is_null() {
        -1
    } else {
        let script = unsafe { str::from_utf8(CStr::from_ptr(script).to_bytes()).unwrap() };
        evaluate(botnana, &script.to_owned());
        0
    }
}

/// get slave information
#[no_mangle]
pub extern "C" fn motion_poll(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"motion.poll"}"#;
    send_message(botnana, &msg.to_owned());
}

#[no_mangle]
pub extern "C" fn botnana_profiler_restart(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"profiler.restart"}"#;
    send_message(botnana, &msg.to_owned());
}

#[no_mangle]
pub extern "C" fn botnana_profiler_output(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"profiler.output"}"#;
    send_message(botnana, &msg.to_owned());
}

/// version.get
#[no_mangle]
pub extern "C" fn version_get(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"version.get"}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.slave.set
#[no_mangle]
pub extern "C" fn config_slave_set(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    param: *const c_char,
    value: libc::int32_t,
) -> libc::int32_t {
    if param.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.slave.set","params":{"position":"#.to_owned()
            + position.to_string().as_str() + r#","channel":"#
            + channel.to_string().as_str() + r#",""# + param + r#"":"#
            + value.to_string().as_str() + r#"}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.slave.get
#[no_mangle]
pub extern "C" fn config_slave_get(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.slave.get","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#","channel":"#
        + channel.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.motion.set
#[no_mangle]
pub extern "C" fn config_motion_set(
    botnana: Box<Botnana>,
    param: *const c_char,
    value: libc::uint32_t,
) -> libc::int32_t {
    if param.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{""#.to_owned() + param
            + r#"":"# + value.to_string().as_str() + r#"}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.motion.get
#[no_mangle]
pub extern "C" fn config_motion_get(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.get"}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.group.set for string data type
#[no_mangle]
pub extern "C" fn config_group_set_string(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    param: *const c_char,
    value: *const c_char,
) -> libc::int32_t {
    if param.is_null() || value.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };
        let value = unsafe { str::from_utf8(CStr::from_ptr(value).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"# + position.to_string().as_str() + r#",""# + param
            + r#"":""# + value + r#""}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.group.set for mapping
#[no_mangle]
pub extern "C" fn config_group_set_mapping(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: *const c_char,
) -> libc::int32_t {
    if value.is_null() {
        -1
    } else {
        let value = unsafe { str::from_utf8(CStr::from_ptr(value).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"# + position.to_string().as_str() + r#","mapping":["#
            + value + r#"]}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.group.set for double data type
#[no_mangle]
pub extern "C" fn config_group_set_double(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    param: *const c_char,
    value: libc::c_double,
) -> libc::int32_t {
    if param.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"# + position.to_string().as_str() + r#",""# + param
            + r#"":"# + value.to_string().as_str() + r#"}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.group.get
#[no_mangle]
pub extern "C" fn config_group_get(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.get","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.axis.set for string data type
#[no_mangle]
pub extern "C" fn config_axis_set_string(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    param: *const c_char,
    value: *const c_char,
) -> libc::int32_t {
    if param.is_null() || value.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };
        let value = unsafe { str::from_utf8(CStr::from_ptr(value).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
            + r#""position":"# + position.to_string().as_str() + r#",""# + param
            + r#"":""# + value + r#""}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.axis.set for double data type
#[no_mangle]
pub extern "C" fn config_axis_set_double(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    param: *const c_char,
    value: libc::c_double,
) -> libc::int32_t {
    if param.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
            + r#""position":"# + position.to_string().as_str() + r#",""# + param
            + r#"":"# + value.to_string().as_str() + r#"}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.axis.set for integer data type
#[no_mangle]
pub extern "C" fn config_axis_set_integer(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    param: *const c_char,
    value: libc::int32_t,
) -> libc::int32_t {
    if param.is_null() {
        -1
    } else {
        let param = unsafe { str::from_utf8(CStr::from_ptr(param).to_bytes()).unwrap() };

        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
            + r#""position":"# + position.to_string().as_str() + r#",""# + param
            + r#"":"# + value.to_string().as_str() + r#"}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.axis.get
#[no_mangle]
pub extern "C" fn config_axis_get(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.get","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// save config
#[no_mangle]
pub extern "C" fn config_save(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.save"}"#;
    send_message(botnana, &msg.to_owned());
}
