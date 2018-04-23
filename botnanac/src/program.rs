extern crate libc;
use std::os::raw::c_char;
use std::ffi::CStr;
use botnana::Botnana;
use std::str;
use botnana::{evaluate, send_message};

use std::sync::{Arc, Mutex};

/// Program
#[repr(C)]
pub struct Program {
    name: String,
    line: String,
    lines: Arc<Mutex<String>>,
}

#[no_mangle]
pub fn botnana_new_program(name: *const c_char) -> Box<Program> {
    let name = unsafe {
        assert!(!name.is_null());
        str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap()
    };

    let mut line = String::new();
    line.push_str(&(": user$".to_owned() + name + "\\n"));

    let lines = Arc::new(Mutex::new(line.clone()));

    let program = Program {
        name: name.to_string(),
        line: line.clone(),
        lines: lines.clone(),
    };

    Box::new(program)
}

/// deploy porgram
#[no_mangle]
pub fn botnana_deploy_program(botnana: Box<Botnana>, program: Box<Program>) {
    let program = Box::into_raw(program);
    let lines = unsafe { (*program).lines.clone() };

    lines.lock().unwrap().push_str("end-of-program ;");

    let msg = r#"{"jsonrpc":"2.0","method":"script.deploy","params":{"script":""#.to_owned()
        + &lines.lock().unwrap() + r#""}}"#;

    send_message(botnana, &msg.to_owned());
}

/// abort porgram
#[no_mangle]
pub fn botnana_abort_program(botnana: Box<Botnana>) {
    evaluate(botnana, &"kill-task0".to_owned());
}

/// empty porgram
#[no_mangle]
pub fn botnana_empty_program(botnana: Box<Botnana>) {
    evaluate(botnana, &"empty  marker empty".to_owned());
}

/// push program line
#[no_mangle]
pub fn botnana_push_program_line(program: Box<Program>, cmd: *const c_char) {
    let program = Box::into_raw(program);
    let lines = unsafe { (*program).lines.clone() };

    let cmd = unsafe {
        assert!(!cmd.is_null());
        str::from_utf8(CStr::from_ptr(cmd).to_bytes()).unwrap()
    };

    lines.lock().unwrap().push_str(&(cmd.to_owned() + "\\n"));
}

/// run porgram
#[no_mangle]
pub fn botnana_run_program(botnana: Box<Botnana>, program: Box<Program>) {
    let program = Box::into_raw(program);
    let name = unsafe { (*program).name.clone() };
    let msg = "deploy user$".to_owned() + &name + " ;deploy";
    evaluate(botnana, &msg)
}
