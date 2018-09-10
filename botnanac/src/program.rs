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
    lines: Arc<Mutex<String>>,
}

impl Program {
    /// push program line
    pub fn push_line(&mut self, script: &str) {
        // 此處 script 使用 &str，因為後續會轉成 string, 所以應該不會被垃圾收集器回收

        // 處理 `"` 字元
        let cmd = script.replace(r#"""#, r#"\""#);
        let lines = self.lines.clone();
        lines.lock().unwrap().push_str(&(cmd.to_owned() + r#"\n"#));
    }

    /// clear program
    pub fn clear(&mut self) {
        // 此處 cmd 使用 &str，因為後續會轉成 string, 所以應該不會被垃圾收集器回收
        let lines = self.lines.clone();
        let mut line = lines.lock().unwrap();
        line.clear();
        line.push_str(&(r#": user$"#.to_owned() + &self.name + r#"\n"#));
    }
}

#[no_mangle]
pub extern "C" fn program_new(name: *const c_char) -> Box<Program> {
    let name = unsafe {
        assert!(!name.is_null());
        str::from_utf8(CStr::from_ptr(name).to_bytes()).unwrap()
    };

    let mut line = String::new();
    line.push_str(&(r#": user$"#.to_owned() + name + r#"\n"#));

    let lines = Arc::new(Mutex::new(line.clone()));

    let program = Program {
        name: name.to_string(),
        lines: lines.clone(),
    };

    Box::new(program)
}

/// deploy porgram
#[no_mangle]
pub extern "C" fn program_deploy(botnana: Box<Botnana>, program: Box<Program>) {
    let program = Box::into_raw(program);

    unsafe {
        (*program).push_line(&"end-of-program ;".to_owned());
        let lines = (*program).lines.clone();
        let msg = r#"{"jsonrpc":"2.0","method":"script.deploy","params":{"script":""#.to_owned()
            + &lines.lock().unwrap() + r#""}}"#;
        send_message(botnana, &msg.to_owned());
    }
}

/// push program line
#[no_mangle]
pub extern "C" fn program_line(program: Box<Program>, cmd: *const c_char) {
    let program = Box::into_raw(program);
    let cmd = unsafe {
        assert!(!cmd.is_null());
        str::from_utf8(CStr::from_ptr(cmd).to_bytes()).unwrap()
    };

    unsafe {
        (*program).push_line(cmd);
    }
}

/// clear program
#[no_mangle]
pub extern "C" fn program_clear(program: Box<Program>) {
    let program = Box::into_raw(program);

    unsafe {
        (*program).clear();
    }
}

/// run porgram
#[no_mangle]
pub extern "C" fn program_run(botnana: Box<Botnana>, program: Box<Program>) {
    let program = Box::into_raw(program);
    let name = unsafe { (*program).name.clone() };
    let msg = r#"deploy user$"#.to_owned() + &name + r#" ;deploy"#;
    evaluate(botnana, &msg)
}
