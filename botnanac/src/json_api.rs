extern crate libc;
use std::os::raw::c_char;
use std::ffi::CStr;
use botnana::Botnana;
use std::str;
use botnana::{evaluate, send_message};

/// motion_evaluate
#[no_mangle]
pub extern "C" fn motion_evaluate(botnana: Box<Botnana>, script: *const c_char) {
    let script = unsafe {
        assert!(!script.is_null());
        str::from_utf8(CStr::from_ptr(script).to_bytes()).unwrap()
    };
    evaluate(botnana, &script.to_owned());
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

/// Set group config
#[no_mangle]
pub extern "C" fn botnana_set_group_config(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    name: *const c_char,
    gtype: *const c_char,
    mapping: *const c_char,
    vmax: libc::c_double,
    amax: libc::c_double,
    jmax: libc::c_double,
) {
    let name = unsafe {
        assert!(!name.is_null());
        str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap()
    };

    let gtype = unsafe {
        assert!(!gtype.is_null());
        str::from_utf8(CStr::from_ptr(gtype).to_bytes()).unwrap()
    };

    let mapping = unsafe {
        assert!(!mapping.is_null());
        str::from_utf8(CStr::from_ptr(mapping).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","name":""# + name
        + r#"","gtype":""# + gtype + r#"","mapping":["# + mapping + r#"],"vmax":"#
        + vmax.to_string().as_str() + r#","amax":"# + amax.to_string().as_str()
        + r#","jmax":"# + jmax.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set group config name
#[no_mangle]
pub extern "C" fn botnana_set_group_config_name(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    name: *const c_char,
) {
    let name = unsafe {
        assert!(!name.is_null());
        str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","name":""# + name
        + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set group config gtype
#[no_mangle]
pub extern "C" fn botnana_set_group_config_gtype(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    gtype: *const c_char,
) {
    let gtype = unsafe {
        assert!(!gtype.is_null());
        str::from_utf8(CStr::from_ptr(gtype).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","gtype":""# + gtype
        + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set group config map1d
#[no_mangle]
pub extern "C" fn botnana_set_group_map1d(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    a1: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","mapping":["#
        + a1.to_string().as_str() + r#"]}}"#;
    send_message(botnana.clone(), &msg.to_owned());

    let cmd = a1.to_string() + r#" "# + position.to_string().as_str() + r#" map1d"#;
    evaluate(botnana, &cmd.to_owned());
}

/// Set group config map2d
#[no_mangle]
pub extern "C" fn botnana_set_group_map2d(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    a1: libc::uint32_t,
    a2: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","mapping":["#
        + a1.to_string().as_str() + r#","# + a2.to_string().as_str() + r#"]}}"#;
    send_message(botnana.clone(), &msg.to_owned());
    let cmd = a1.to_string() + r#" "# + a2.to_string().as_str() + r#" "#
        + position.to_string().as_str() + r#" map2d"#;
    evaluate(botnana, &cmd.to_owned());
}

/// Set group config map3d
#[no_mangle]
pub extern "C" fn botnana_set_group_map3d(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    a1: libc::uint32_t,
    a2: libc::uint32_t,
    a3: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","mapping":["#
        + a1.to_string().as_str() + r#","# + a2.to_string().as_str() + r#","#
        + a3.to_string().as_str() + r#"]}}"#;
    send_message(botnana.clone(), &msg.to_owned());
    let cmd = a1.to_string() + r#" "# + a2.to_string().as_str() + r#" "# + a3.to_string().as_str()
        + r#" "# + position.to_string().as_str() + r#" map3d"#;
    evaluate(botnana, &cmd.to_owned());
}

/// Set group config vmax
#[no_mangle]
pub extern "C" fn botnana_set_group_vmax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    vmax: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","vmax":"#
        + vmax.to_string().as_str() + r#"}}"#;
    send_message(botnana.clone(), &msg.to_owned());
    let cmd = vmax.to_string() + r#"e "# + position.to_string().as_str() + r#" gvmax!"#;
    evaluate(botnana, &cmd.to_owned());
}

/// Set group config amax
#[no_mangle]
pub extern "C" fn botnana_set_group_amax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    amax: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","amax":"#
        + amax.to_string().as_str() + r#"}}"#;
    send_message(botnana.clone(), &msg.to_owned());
    let cmd = amax.to_string() + r#"e "# + position.to_string().as_str() + r#" gamax!"#;
    evaluate(botnana, &cmd.to_owned());
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
