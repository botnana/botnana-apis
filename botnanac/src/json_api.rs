extern crate libc;
use botnana::{evaluate, send_message, Botnana};
use std::{ffi::CStr, os::raw::c_char, str};

/// motion_evaluate
#[no_mangle]
pub extern "C" fn script_evaluate(botnana: Box<Botnana>, script: *const c_char) -> libc::int32_t {
    if script.is_null() {
        -1
    } else {
        let script = unsafe { String::from_utf8_lossy(&CStr::from_ptr(script).to_bytes()) };
        evaluate(botnana, &script);
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
fn config_slave_set(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    param: &str,
    value: i32,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.slave.set","params":{"alias":"#.to_owned()
        + alias.to_string().as_str()
        + r#","position":"#
        + position.to_string().as_str()
        + r#","channel":"#
        + channel.to_string().as_str()
        + r#",""#
        + param
        + r#"":"#
        + value.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.slave.set (homing_method)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_method(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(botnana, alias, position, channel, "homing_method", value);
}

/// config.slave.set (homing_speed_1)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_speed_1(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(botnana, alias, position, channel, "homing_speed_1", value);
}

/// config.slave.set (homing_speed_2)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_speed_2(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(botnana, alias, position, channel, "homing_speed_2", value);
}

/// config.slave.set (homing_acceleration)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_acceleration(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "homing_acceleration",
        value,
    );
}

/// config.slave.set (profile_velocity)
#[no_mangle]
pub extern "C" fn config_slave_set_profile_velocity(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(botnana, alias, position, channel, "profile_velocity", value);
}

/// config.slave.set (profile_acceleration)
#[no_mangle]
pub extern "C" fn config_slave_set_profile_acceleration(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "profile_acceleration",
        value,
    );
}

/// config.slave.set (profile_deceleration)
#[no_mangle]
pub extern "C" fn config_slave_set_profile_deceleration(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "profile_deceleration",
        value,
    );
}

/// config.slave.set (pdo_velocity_offset)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_velocity_offset(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_velocity_offset",
        value,
    );
}

/// config.slave.set (pdo_torque_offset)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_torque_offset(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_torque_offset",
        value,
    );
}

/// config.slave.set (pdo_digital_inputs)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_digital_inputs(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_digital_inputs",
        value,
    );
}

/// config.slave.set (pdo_demand_position)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_demand_position(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_demand_position",
        value,
    );
}

/// config.slave.set (pdo_demand_velocity)
#[no_mangle]
pub extern "C" fn config_slave_pdo_demand_velocity(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_demand_velocity",
        value,
    );
}

/// config.slave.set (pdo_demand_torque)
#[no_mangle]
pub extern "C" fn config_slave_pdo_demand_torque(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_demand_torque",
        value,
    );
}

/// config.slave.set (pdo_real_velocity)
#[no_mangle]
pub extern "C" fn config_slave_pdo_real_velocity(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(
        botnana,
        alias,
        position,
        channel,
        "pdo_real_velocity",
        value,
    );
}

/// config.slave.set (pdo_real_torque)
#[no_mangle]
pub extern "C" fn config_slave_pdo_real_torque(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    config_slave_set(botnana, alias, position, channel, "pdo_real_torque", value);
}

/// config.slave.get
#[no_mangle]
pub extern "C" fn config_slave_get(
    botnana: Box<Botnana>,
    alias: libc::uint32_t,
    position: libc::uint32_t,
    channel: libc::uint32_t,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.slave.get","params":{"alias":"#.to_owned()
        + alias.to_string().as_str()
        + r#","position":"#
        + position.to_string().as_str()
        + r#","channel":"#
        + channel.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.motion.set
fn config_motion_set(botnana: Box<Botnana>, param: &str, value: u32) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{""#.to_owned()
        + param
        + r#"":"#
        + value.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.motion.set (period_us)
#[no_mangle]
pub extern "C" fn config_motion_set_period_us(botnana: Box<Botnana>, value: libc::uint32_t) {
    config_motion_set(botnana, "period_us", value);
}

/// config.motion.set (group_capacity)
#[no_mangle]
pub extern "C" fn config_motion_set_group_capacity(botnana: Box<Botnana>, value: libc::uint32_t) {
    config_motion_set(botnana, "group_capacity", value);
}

/// config.motion.set (axis_capacity)
#[no_mangle]
pub extern "C" fn config_motion_set_axis_capacity(botnana: Box<Botnana>, value: libc::uint32_t) {
    config_motion_set(botnana, "axis_capacity", value);
}

/// config.motion.get
#[no_mangle]
pub extern "C" fn config_motion_get(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.motion.get"}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.group.set for string data type
fn config_group_set_string(botnana: Box<Botnana>, position: u32, param: &str, value: &str) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"#
        + position.to_string().as_str()
        + r#",""#
        + param
        + r#"":""#
        + value
        + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.group.set (name)
#[no_mangle]
pub extern "C" fn config_group_set_name(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    name: *const c_char,
) -> libc::int32_t {
    if name.is_null() {
        -1
    } else {
        let name = unsafe { str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap() };
        config_group_set_string(botnana, position, "name", name);
        0
    }
}

/// config.group.set (gtype as 1D)
#[no_mangle]
pub extern "C" fn config_group_set_gtype_as_1d(botnana: Box<Botnana>, position: libc::uint32_t) {
    config_group_set_string(botnana, position, "gtype", "1D");
}

/// config.group.set (gtype as 2D)
#[no_mangle]
pub extern "C" fn config_group_set_gtype_as_2d(botnana: Box<Botnana>, position: libc::uint32_t) {
    config_group_set_string(botnana, position, "gtype", "2D");
}

/// config.group.set (gtype as 3D)
#[no_mangle]
pub extern "C" fn config_group_set_gtype_as_3d(botnana: Box<Botnana>, position: libc::uint32_t) {
    config_group_set_string(botnana, position, "gtype", "3D");
}

/// config.group.set (gtype as SINE)
#[no_mangle]
pub extern "C" fn config_group_set_gtype_as_sine(botnana: Box<Botnana>, position: libc::uint32_t) {
    config_group_set_string(botnana, position, "gtype", "SINE");
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
            + r#""position":"#
            + position.to_string().as_str()
            + r#","mapping":["#
            + value
            + r#"]}}"#;
        send_message(botnana, &msg.to_owned());
        0
    }
}

/// config.group.set for double data type
fn config_group_set_double(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    param: &str,
    value: f64,
) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
        + r#""position":"#
        + position.to_string().as_str()
        + r#",""#
        + param
        + r#"":"#
        + value.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.group.set (vmax)
#[no_mangle]
pub extern "C" fn config_group_set_vmax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    vmax: libc::c_double,
) {
    config_group_set_double(botnana, position, "vmax", vmax);
}

/// config.group.set (amax)
#[no_mangle]
pub extern "C" fn config_group_set_amax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    amax: libc::c_double,
) {
    config_group_set_double(botnana, position, "amax", amax);
}

/// config.group.set (jmax)
#[no_mangle]
pub extern "C" fn config_group_set_jmax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    jmax: libc::c_double,
) {
    config_group_set_double(botnana, position, "jmax", jmax);
}

/// config.group.get
#[no_mangle]
pub extern "C" fn config_group_get(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.group.get","params":{"position":"#.to_owned()
        + position.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.axis.set for string data type
fn config_axis_set_string(botnana: Box<Botnana>, position: u32, param: &str, value: &str) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"#
        + position.to_string().as_str()
        + r#",""#
        + param
        + r#"":""#
        + value
        + r#""}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.axis.set (name)
#[no_mangle]
pub extern "C" fn config_axis_set_name(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    name: *const c_char,
) -> libc::int32_t {
    if name.is_null() {
        -1
    } else {
        let name = unsafe { str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap() };
        config_axis_set_string(botnana, position, "name", name);
        0
    }
}

/// config.axis.set (encoder_length_unit)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_length_unit_as_meter(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
) {
    config_axis_set_string(botnana, position, "encoder_length_unit", "Meter");
}

/// config.axis.set (encoder_length_unit)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_length_unit_as_revolution(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
) {
    config_axis_set_string(botnana, position, "encoder_length_unit", "Revolution");
}

/// config.axis.set (encoder_length_unit)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_length_unit_as_pulse(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
) {
    config_axis_set_string(botnana, position, "encoder_length_unit", "Pulse");
}

/// config.axis.set for double data type
fn config_axis_set_double(botnana: Box<Botnana>, position: u32, param: &str, value: f64) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"#
        + position.to_string().as_str()
        + r#",""#
        + param
        + r#"":"#
        + value.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.group.set (home_offset)
#[no_mangle]
pub extern "C" fn config_axis_set_home_offset(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    offset: libc::c_double,
) {
    config_axis_set_double(botnana, position, "home_offset", offset);
}

/// config.group.set (encoder_ppu)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_ppu(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::c_double,
) {
    config_axis_set_double(botnana, position, "encoder_ppu", value);
}

/// config.group.set (ext_encoder_ppu)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_ppu(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::c_double,
) {
    config_axis_set_double(botnana, position, "ext_encoder_ppu", value);
}

/// config.group.set (closed_loop_filter)
#[no_mangle]
pub extern "C" fn config_axis_set_closed_loop_filter(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::c_double,
) {
    config_axis_set_double(botnana, position, "closed_loop_filter", value);
}

/// config.group.set (max_position_deviation)
#[no_mangle]
pub extern "C" fn config_axis_set_max_position_deviation(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::c_double,
) {
    config_axis_set_double(botnana, position, "max_position_deviation", value);
}

/// config.group.set (amax)
#[no_mangle]
pub extern "C" fn config_axis_set_amax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::c_double,
) {
    config_axis_set_double(botnana, position, "amax", value);
}

/// config.group.set (vmax)
#[no_mangle]
pub extern "C" fn config_axis_set_vmax(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::c_double,
) {
    config_axis_set_double(botnana, position, "vmax", value);
}

/// config.axis.set for integer data type
fn config_axis_set_integer(botnana: Box<Botnana>, position: u32, param: &str, value: i32) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
        + r#""position":"#
        + position.to_string().as_str()
        + r#",""#
        + param
        + r#"":"#
        + value.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// config.group.set (encoder_direction)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_direction(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "encoder_direction", value);
}

/// config.group.set (ext_encoder_direction)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_direction(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "ext_encoder_direction", value);
}

/// config.group.set (drive_alias)
#[no_mangle]
pub extern "C" fn config_axis_set_drive_alias(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "drive_alias", value);
}

/// config.group.set (drive_slave_position)
#[no_mangle]
pub extern "C" fn config_axis_set_drive_slave_position(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "drive_slave_position", value);
}

/// config.group.set (drive_channel)
#[no_mangle]
pub extern "C" fn config_axis_set_drive_channel(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "drive_channel", value);
}

/// config.group.set (ext_encoder_alias)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_alias(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "ext_encoder_alias", value);
}

/// config.group.set (ext_encoder_slave_position)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_slave_position(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "ext_encoder_slave_position", value);
}

/// config.group.set (ext_encoder_channel)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_channel(
    botnana: Box<Botnana>,
    position: libc::uint32_t,
    value: libc::int32_t,
) {
    config_axis_set_integer(botnana, position, "ext_encoder_channel", value);
}

/// config.axis.get
#[no_mangle]
pub extern "C" fn config_axis_get(botnana: Box<Botnana>, position: libc::uint32_t) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.axis.get","params":{"position":"#.to_owned()
        + position.to_string().as_str()
        + r#"}}"#;
    send_message(botnana, &msg.to_owned());
}

/// save config
#[no_mangle]
pub extern "C" fn config_save(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"config.save"}"#;
    send_message(botnana, &msg.to_owned());
}

/// System poweroff
#[no_mangle]
pub extern "C" fn poweroff(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"system.poweroff"}"#;
    send_message(botnana, &msg.to_owned());
}

/// System reboot
#[no_mangle]
pub extern "C" fn reboot(botnana: Box<Botnana>) {
    let msg = r#"{"jsonrpc":"2.0","method":"system.reboot"}"#;
    send_message(botnana, &msg.to_owned());
}
