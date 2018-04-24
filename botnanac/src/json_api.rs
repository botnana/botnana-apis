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

/// Set motion config
#[no_mangle]
pub extern "C" fn botnana_set_motion_config(
    botnana: Box<Botnana>,
    period_us: libc::uint32_t,
    group_cap: libc::uint32_t,
    axis_cap: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{"#.to_owned()
        + r#""period_us":"# + period_us.to_string().as_str() + r#","group_capacity":"#
        + group_cap.to_string().as_str() + r#","axis_capacity":"#
        + axis_cap.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set motion config period_us
#[no_mangle]
pub extern "C" fn botnana_set_motion_config_period_us(
    botnana: Box<Botnana>,
    period_us: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{"#.to_owned()
        + r#""period_us":"# + period_us.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set motion config group_capacity
#[no_mangle]
pub extern "C" fn botnana_set_motion_config_group_capacity(
    botnana: Box<Botnana>,
    group_capacity: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{"#.to_owned()
        + r#""group_capacity":"# + group_capacity.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set motion config axis_capacity
#[no_mangle]
pub extern "C" fn botnana_set_motion_config_axis_capacity(
    botnana: Box<Botnana>,
    axis_capacity: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{"#.to_owned()
        + r#""axis_capacity":"# + axis_capacity.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Get motion config
#[no_mangle]
pub extern "C" fn botnana_get_motion_config(botnana: Box<Botnana>) {
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
    mapping: *const c_char,
) {
    let gtype = unsafe {
        assert!(!gtype.is_null());
        str::from_utf8(CStr::from_ptr(gtype).to_bytes()).unwrap()
    };
    let mapping = unsafe {
        assert!(!mapping.is_null());
        str::from_utf8(CStr::from_ptr(mapping).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","gtype":""# + gtype
        + r#"","mapping":["# + mapping + r#"]}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set group config vmax
#[no_mangle]
pub extern "C" fn botnana_set_group_config_vmax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    vmax: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","vmax":"#
        + vmax.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set group config amax
#[no_mangle]
pub extern "C" fn botnana_set_group_config_amax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    amax: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","amax":"#
        + amax.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set group config jmax
#[no_mangle]
pub extern "C" fn botnana_set_group_config_jmax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    jmax: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","jmax":"#
        + jmax.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// get group config
#[no_mangle]
pub extern "C" fn botnana_get_group_config(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.get","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set axis config
#[no_mangle]
pub extern "C" fn botnana_set_axis_config(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    name: *const c_char,
    home_offset: libc::c_double,
    encoder_ppu: libc::c_double,
    encoder_length_unit: *const c_char,
    encoder_direction: libc::uint32_t,
) {
    let name = unsafe {
        assert!(!name.is_null());
        str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap()
    };

    let encoder_length_unit = unsafe {
        assert!(!encoder_length_unit.is_null());
        str::from_utf8(CStr::from_ptr(encoder_length_unit).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","name":""# + name
        + r#"","encoder_length_unit":""# + encoder_length_unit + r#"","home_offset":"#
        + home_offset.to_string().as_str() + r#","encoder_ppu":"#
        + encoder_ppu.to_string().as_str() + r#","encoder_direction":"#
        + encoder_direction.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set axis config name
#[no_mangle]
pub extern "C" fn botnana_set_axis_config_name(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    name: *const c_char,
) {
    let name = unsafe {
        assert!(!name.is_null());
        str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","name":""# + name
        + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set axis config encoder_length_unit
#[no_mangle]
pub extern "C" fn botnana_set_axis_config_encoder_length_unit(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    encoder_length_unit: *const c_char,
) {
    let encoder_length_unit = unsafe {
        assert!(!encoder_length_unit.is_null());
        str::from_utf8(CStr::from_ptr(encoder_length_unit).to_bytes()).unwrap()
    };

    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str()
        + r#","encoder_length_unit":""# + encoder_length_unit + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set axis config encoder_ppu
#[no_mangle]
pub extern "C" fn botnana_set_axis_config_ppu(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    ppu: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","encoder_ppu":"#
        + ppu.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set axis config home_offset
#[no_mangle]
pub extern "C" fn botnana_set_axis_config_home_offset(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    offset: libc::c_double,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str() + r#","home_offset":"#
        + offset.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// Set axis config encoder_direction
#[no_mangle]
pub extern "C" fn botnana_set_axis_config_encoder_direction(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    direction: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"# + position.to_string().as_str()
        + r#","encoder_direction":"# + direction.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// get axis config
#[no_mangle]
pub extern "C" fn botnana_get_axis_config(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.get","params":{"position":"#.to_owned()
        + position.to_string().as_str() + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// save config
#[no_mangle]
pub extern "C" fn botnana_save_config(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.save"}"#;
    send_message(botnana, &msg.to_owned());
}
