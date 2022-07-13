extern crate botnanars;
use botnanars::botnana::Botnana;
use std::{ffi::CStr, os::raw::c_char, str};

/// motion_evaluate
#[no_mangle]
pub extern "C" fn script_evaluate(botnana: Box<Botnana>, script: *const c_char) -> i32 {
    if script.is_null() {
        -1
    } else {
        let script = unsafe { String::from_utf8_lossy(&CStr::from_ptr(script).to_bytes()) };
        let s = Box::into_raw(botnana);
        unsafe {
            (*s).evaluate(&script);
        }
        0
    }
}

/// get slave information
#[no_mangle]
pub extern "C" fn motion_poll(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).motion_poll();
    }
}

#[no_mangle]
pub extern "C" fn botnana_profiler_restart(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).profiler_restart();
    }
}

#[no_mangle]
pub extern "C" fn botnana_profiler_output(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).profiler_output();
    }
}

/// version.get
#[no_mangle]
pub extern "C" fn version_get(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).version_get();
    }
}

/// config.slave.set (homing_method)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_method(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_homing_method(alias, position, channel, value);
    }
}

/// config.slave.set (homing_speed_1)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_speed_1(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_homing_speed_1(alias, position, channel, value);
    }
}

/// config.slave.set (homing_speed_2)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_speed_2(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_homing_speed_2(alias, position, channel, value);
    }
}

/// config.slave.set (homing_acceleration)
#[no_mangle]
pub extern "C" fn config_slave_set_homing_acceleration(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_homing_acceleration(alias, position, channel, value);
    }
}

/// config.slave.set (profile_velocity)
#[no_mangle]
pub extern "C" fn config_slave_set_profile_velocity(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_profile_velocity(alias, position, channel, value);
    }
}

/// config.slave.set (profile_acceleration)
#[no_mangle]
pub extern "C" fn config_slave_set_profile_acceleration(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_profile_acceleration(alias, position, channel, value);
    }
}

/// config.slave.set (profile_deceleration)
#[no_mangle]
pub extern "C" fn config_slave_set_profile_deceleration(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_profile_deceleration(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_velocity_offset)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_velocity_offset(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_velocity_offset(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_torque_offset)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_torque_offset(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_torque_offset(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_digital_inputs)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_digital_inputs(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_digital_inputs(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_demand_position)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_demand_position(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_demand_position(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_demand_velocity)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_demand_velocity(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_demand_velocity(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_real_velocity)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_real_velocity(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_real_velocity(alias, position, channel, value);
    }
}

/// config.slave.set (pdo_real_torque)
#[no_mangle]
pub extern "C" fn config_slave_set_pdo_real_torque(
    botnana: Box<Botnana>,
    alias: u32,
    position: u32,
    channel: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_set_pdo_real_torque(alias, position, channel, value);
    }
}

/// config.slave.get
#[no_mangle]
pub extern "C" fn config_slave_get(botnana: Box<Botnana>, alias: u32, position: u32, channel: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_get(alias, position, channel);
    }
}

/// config.motion.set (period_us)
#[no_mangle]
pub extern "C" fn config_motion_set_period_us(botnana: Box<Botnana>, value: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_motion_set_period_us(value);
    }
}

/// config.motion.set (group_capacity)
#[no_mangle]
pub extern "C" fn config_motion_set_group_capacity(botnana: Box<Botnana>, value: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_motion_set_group_capacity(value);
    }
}

/// config.motion.set (axis_capacity)
#[no_mangle]
pub extern "C" fn config_motion_set_axis_capacity(botnana: Box<Botnana>, value: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_motion_set_axis_capacity(value);
    }
}

/// config.motion.get
#[no_mangle]
pub extern "C" fn config_motion_get(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_motion_get();
    }
}

/// config.group.set (name)
#[no_mangle]
pub extern "C" fn config_group_set_name(
    botnana: Box<Botnana>,
    position: u32,
    name: *const c_char,
) -> i32 {
    if name.is_null() {
        -1
    } else {
        let name = unsafe { str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap() };
        let s = Box::into_raw(botnana);
        unsafe {
            (*s).config_group_set_name(position, name);
        }
        0
    }
}

/// config.group.set (gtype as 1D)
#[no_mangle]
pub extern "C" fn config_group_set_type_as_1d(botnana: Box<Botnana>, position: u32, a1: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_type_as_1d(position, a1);
    }
}

/// config.group.set (gtype as 2D)
#[no_mangle]
pub extern "C" fn config_group_set_type_as_2d(
    botnana: Box<Botnana>,
    position: u32,
    a1: u32,
    a2: u32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_type_as_2d(position, a1, a2);
    }
}

/// config.group.set (gtype as 3D)
#[no_mangle]
pub extern "C" fn config_group_set_type_as_3d(
    botnana: Box<Botnana>,
    position: u32,
    a1: u32,
    a2: u32,
    a3: u32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_type_as_3d(position, a1, a2, a3);
    }
}

/// config.group.set (gtype as SINE)
#[no_mangle]
pub extern "C" fn config_group_set_type_as_sine(botnana: Box<Botnana>, position: u32, a1: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_type_as_sine(position, a1);
    }
}

/// config.group.set (vmax)
#[no_mangle]
pub extern "C" fn config_group_set_vmax(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_vmax(position, value);
    }
}

/// config.group.set (amax)
#[no_mangle]
pub extern "C" fn config_group_set_amax(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_amax(position, value);
    }
}

/// config.group.set (jmax)
#[no_mangle]
pub extern "C" fn config_group_set_jmax(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_set_jmax(position, value);
    }
}

/// config.group.get
#[no_mangle]
pub extern "C" fn config_group_get(botnana: Box<Botnana>, position: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_get(position);
    }
}

/// config.axis.set (name)
#[no_mangle]
pub extern "C" fn config_axis_set_name(
    botnana: Box<Botnana>,
    position: u32,
    name: *const c_char,
) -> i32 {
    if name.is_null() {
        -1
    } else {
        let name = unsafe { str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap() };
        let s = Box::into_raw(botnana);
        unsafe {
            (*s).config_axis_set_name(position, name);
        }
        0
    }
}

/// config.axis.set (encoder_length_unit)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_length_unit_as_meter(
    botnana: Box<Botnana>,
    position: u32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_encoder_length_unit_as_meter(position);
    }
}

/// config.axis.set (encoder_length_unit)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_length_unit_as_revolution(
    botnana: Box<Botnana>,
    position: u32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_encoder_length_unit_as_revolution(position);
    }
}

/// config.axis.set (encoder_length_unit)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_length_unit_as_pulse(
    botnana: Box<Botnana>,
    position: u32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_encoder_length_unit_as_pulse(position);
    }
}

/// config.group.set (home_offset)
#[no_mangle]
pub extern "C" fn config_axis_set_home_offset(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_home_offset(position, value);
    }
}

/// config.group.set (encoder_ppu)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_ppu(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_encoder_ppu(position, value);
    }
}

/// config.group.set (ext_encoder_ppu)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_ppu(
    botnana: Box<Botnana>,
    position: u32,
    value: f64,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_ext_encoder_ppu(position, value);
    }
}

/// config.group.set (closed_loop_filter)
#[no_mangle]
pub extern "C" fn config_axis_set_closed_loop_filter(
    botnana: Box<Botnana>,
    position: u32,
    value: f64,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_closed_loop_filter(position, value);
    }
}

/// config.group.set (max_position_deviation)
#[no_mangle]
pub extern "C" fn config_axis_set_max_position_deviation(
    botnana: Box<Botnana>,
    position: u32,
    value: f64,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_max_position_deviation(position, value);
    }
}

/// config.group.set (amax)
#[no_mangle]
pub extern "C" fn config_axis_set_amax(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_amax(position, value);
    }
}

/// config.group.set (vmax)
#[no_mangle]
pub extern "C" fn config_axis_set_vmax(botnana: Box<Botnana>, position: u32, value: f64) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_vmax(position, value);
    }
}

/// config.group.set (encoder_direction)
#[no_mangle]
pub extern "C" fn config_axis_set_encoder_direction(
    botnana: Box<Botnana>,
    position: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_encoder_direction(position, value);
    }
}

/// config.group.set (ext_encoder_direction)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_direction(
    botnana: Box<Botnana>,
    position: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_ext_encoder_direction(position, value);
    }
}

/// config.group.set (drive_alias)
#[no_mangle]
pub extern "C" fn config_axis_set_drive_alias(botnana: Box<Botnana>, position: u32, value: i32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_drive_alias(position, value);
    }
}

/// config.group.set (drive_slave_position)
#[no_mangle]
pub extern "C" fn config_axis_set_drive_slave_position(
    botnana: Box<Botnana>,
    position: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_drive_slave_position(position, value);
    }
}

/// config.group.set (drive_channel)
#[no_mangle]
pub extern "C" fn config_axis_set_drive_channel(botnana: Box<Botnana>, position: u32, value: i32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_drive_channel(position, value);
    }
}

/// config.group.set (ext_encoder_alias)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_alias(
    botnana: Box<Botnana>,
    position: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_ext_encoder_alias(position, value);
    }
}

/// config.group.set (ext_encoder_slave_position)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_slave_position(
    botnana: Box<Botnana>,
    position: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_ext_encoder_slave_position(position, value);
    }
}

/// config.group.set (ext_encoder_channel)
#[no_mangle]
pub extern "C" fn config_axis_set_ext_encoder_channel(
    botnana: Box<Botnana>,
    position: u32,
    value: i32,
) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_axis_set_ext_encoder_channel(position, value);
    }
}

/// config.axis.get
#[no_mangle]
pub extern "C" fn config_axis_get(botnana: Box<Botnana>, position: u32) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).config_axis_get(position) };
}

/// save config
#[no_mangle]
pub extern "C" fn config_save(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).config_save() };
}

/// System poweroff
#[no_mangle]
pub extern "C" fn poweroff(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).poweroff() };
}

/// System reboot
#[no_mangle]
pub extern "C" fn reboot(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).reboot() };
}

/// ec_slave.subscribe
#[no_mangle]
pub extern "C" fn subscribe_ec_slave(botnana: Box<Botnana>, alias: u32, position: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).subscribe_ec_slave(alias, position);
    }
}

/// ec_slave.unsubscribe
#[no_mangle]
pub extern "C" fn unsubscribe_ec_slave(botnana: Box<Botnana>, alias: u32, position: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).unsubscribe_ec_slave(alias, position);
    }
}
