extern crate botnanars;
use botnanars::botnana::Botnana;
use std::{ffi::CStr, os::raw::c_char};

/// Sends one script for immediate evaluation.
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

/// Sends one motion poll request.
#[no_mangle]
pub extern "C" fn motion_poll(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).motion_poll();
    }
}

/// Restarts the server profiler.
#[no_mangle]
pub extern "C" fn botnana_profiler_restart(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).profiler_restart();
    }
}

/// Requests profiler output.
#[no_mangle]
pub extern "C" fn botnana_profiler_output(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).profiler_output();
    }
}

/// Requests the Botnana Control version.
#[no_mangle]
pub extern "C" fn version_get(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).version_get();
    }
}

/// Reads one slave configuration.
#[no_mangle]
pub extern "C" fn config_slave_get(botnana: Box<Botnana>, alias: u32, position: u32, channel: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_slave_get(alias, position, channel);
    }
}

/// Reads the motion configuration.
#[no_mangle]
pub extern "C" fn config_motion_get(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_motion_get();
    }
}

/// Reads one group configuration.
#[no_mangle]
pub extern "C" fn config_group_get(botnana: Box<Botnana>, position: u32) {
    let s = Box::into_raw(botnana);
    unsafe {
        (*s).config_group_get(position);
    }
}

/// Reads one axis configuration.
#[no_mangle]
pub extern "C" fn config_axis_get(botnana: Box<Botnana>, position: u32) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).config_axis_get(position) };
}

/// Powers off the system.
#[no_mangle]
pub extern "C" fn poweroff(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).poweroff() };
}

/// Reboots the system.
#[no_mangle]
pub extern "C" fn reboot(botnana: Box<Botnana>) {
    let s = Box::into_raw(botnana);
    unsafe { (*s).reboot() };
}
