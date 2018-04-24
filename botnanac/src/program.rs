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

impl Program {
    pub fn push_line(&mut self, cmd: &str) {
        let lines = self.lines.clone();
        lines.lock().unwrap().push_str(&(cmd.to_owned() + r#"\n"#));
    }

    pub fn until_target_reached(&mut self, position: u32) {
        let msg = position.to_string() + r#" until-target-reached"#;
        self.push_line(&msg);
    }

    pub fn until_no_requests(&mut self) {
        self.push_line(&r#"until-no-requests"#.to_owned());
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
        line: line.clone(),
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
pub extern "C" fn program_push_line(program: Box<Program>, cmd: *const c_char) {
    let program = Box::into_raw(program);
    let cmd = unsafe {
        assert!(!cmd.is_null());
        str::from_utf8(CStr::from_ptr(cmd).to_bytes()).unwrap()
    };

    unsafe {
        (*program).push_line(&cmd);
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

/// program servo_on
#[no_mangle]
pub extern "C" fn program_push_servo_on(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = position.to_string() + r#" servo-on "# + position.to_string().as_str()
            + r#" until-servo-on"#;
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// program hm
#[no_mangle]
pub extern "C" fn program_push_hm(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = r#"hm "#.to_owned() + position.to_string().as_str() + r#" op-mode!"#;
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// program pp
#[no_mangle]
pub extern "C" fn program_push_pp(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = r#"pp "#.to_owned() + position.to_string().as_str() + r#" op-mode!"#;
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// program csp
#[no_mangle]
pub extern "C" fn program_push_csp(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = r#"csp "#.to_owned() + position.to_string().as_str() + r#" op-mode!"#;
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// program set target position
#[no_mangle]
pub extern "C" fn program_push_target_p(
    program: Box<Program>,
    position: libc::uint32_t,
    target: libc::uint32_t,
) {
    let program = Box::into_raw(program);
    let msg = target.to_string() + r#" "# + position.to_string().as_str() + r#" target-p!"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// program go
#[no_mangle]
pub extern "C" fn program_push_go(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    let msg = r#"pause pause pause pause "#.to_owned() + position.to_string().as_str() + r#" go"#;
    unsafe {
        (*program).push_line(&msg);
        (*program).until_target_reached(position);
    }
}

/// reset_fault
#[no_mangle]
pub extern "C" fn program_push_reset_fault(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    let msg = position.to_string() + r#" reset-fault"#;
    unsafe {
        (*program).push_line(&msg);
    }
}
