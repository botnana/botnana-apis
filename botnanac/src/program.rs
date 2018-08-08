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
    pub fn push_line(&mut self, cmd: &str) {
        // 此處 cmd 使用 &str，因為後續會轉成 string, 所以應該不會被垃圾收集器回收
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

    /// push until_target_reached command to program
    pub fn until_target_reached(&mut self, position: u32) {
        let msg = position.to_string() + r#" until-target-reached"#;
        self.push_line(&msg);
    }

    /// push until_no_requests command to program
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
pub extern "C" fn program_push_script(program: Box<Program>, cmd: *const c_char) {
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

/// program servo_on
#[no_mangle]
pub extern "C" fn program_push_servo_on(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = position.to_string() + r#" servo-on "# + position.to_string().as_str()
            + r#" until-servo-on"#;
        (*program).push_line(&msg);
    }
}

/// program servo_off
#[no_mangle]
pub extern "C" fn program_push_servo_off(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = position.to_string() + r#" servo-off "# + position.to_string().as_str();
        (*program).push_line(&msg);
    }
}

/// program hm
#[no_mangle]
pub extern "C" fn program_push_hm(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = r#"6 "#.to_owned() + position.to_string().as_str() + r#" op-mode!"#;
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// program pp
#[no_mangle]
pub extern "C" fn program_push_pp(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = r#"1 "#.to_owned() + position.to_string().as_str() + r#" op-mode!"#;
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// program csp
#[no_mangle]
pub extern "C" fn program_push_csp(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    unsafe {
        let msg = r#"8 "#.to_owned() + position.to_string().as_str() + r#" op-mode!"#;
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
    let msg = position.to_string() + r#" go"#;
    unsafe {
        (*program).push_line(&msg);
        (*program).until_target_reached(position);
    }
}

/// reset_fault
#[no_mangle]
pub extern "C" fn program_push_reset_fault(program: Box<Program>, position: libc::uint32_t) {
    let program = Box::into_raw(program);
    let msg = position.to_string() + r#" reset-fault "# + position.to_string().as_str()
        + r#" until-no-fault"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// enable_ain
#[no_mangle]
pub extern "C" fn program_push_enable_ain(
    program: Box<Program>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
) {
    let program = Box::into_raw(program);
    let msg = channel.to_string() + r#" "# + position.to_string().as_str() + r#" +ec-ain"#;
    unsafe {
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// disable_ain
#[no_mangle]
pub extern "C" fn program_push_disable_ain(
    program: Box<Program>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
) {
    let program = Box::into_raw(program);
    let msg = channel.to_string() + r#" "# + position.to_string().as_str() + r#" -ec-ain"#;
    unsafe {
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// enable_aout
#[no_mangle]
pub extern "C" fn program_push_enable_aout(
    program: Box<Program>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
) {
    let program = Box::into_raw(program);
    let msg = channel.to_string() + r#" "# + position.to_string().as_str() + r#" +ec-aout"#;
    unsafe {
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// disable_aout
#[no_mangle]
pub extern "C" fn program_push_disable_aout(
    program: Box<Program>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
) {
    let program = Box::into_raw(program);
    let msg = channel.to_string() + r#" "# + position.to_string().as_str() + r#" -ec-aout"#;
    unsafe {
        (*program).push_line(&msg);
        (*program).until_no_requests();
    }
}

/// set aout
#[no_mangle]
pub extern "C" fn program_push_set_aout(
    program: Box<Program>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    let program = Box::into_raw(program);
    let msg = value.to_string() + r#" "# + channel.to_string().as_str() + r#" "#
        + position.to_string().as_str() + r#" ec-aout!"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// set dout
#[no_mangle]
pub extern "C" fn program_push_set_dout(
    program: Box<Program>,
    position: libc::uint32_t,
    channel: libc::uint32_t,
    value: libc::int32_t,
) {
    let program = Box::into_raw(program);
    let msg = value.to_string() + r#" "# + channel.to_string().as_str() + r#" "#
        + position.to_string().as_str() + r#" ec-dout!"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// enable coordinator
#[no_mangle]
pub extern "C" fn program_push_enable_coordinator(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"+coordinator"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// start-trj
#[no_mangle]
pub extern "C" fn program_push_start_trj(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"start"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// disable coordinator
#[no_mangle]
pub extern "C" fn program_push_disable_coordinator(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"-coordinator"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// clear path
#[no_mangle]
pub extern "C" fn program_push_clear_path(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"0path"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// set feedrate
#[no_mangle]
pub extern "C" fn program_push_set_feedrate(program: Box<Program>, feedrate: libc::c_double) {
    let program = Box::into_raw(program);
    let msg = feedrate.to_string() + r#"e  feedrate!"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// set vcmd
#[no_mangle]
pub extern "C" fn program_push_set_vcmd(program: Box<Program>, vcmd: libc::c_double) {
    let program = Box::into_raw(program);
    let msg = vcmd.to_string() + r#"e  vcmd!"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// enable group
#[no_mangle]
pub extern "C" fn program_push_enable_group(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"+group"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// disable group
#[no_mangle]
pub extern "C" fn program_push_disable_group(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"-group"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// select group
#[no_mangle]
pub extern "C" fn program_push_select_group(program: Box<Program>, group: libc::uint32_t) {
    let program = Box::into_raw(program);
    let msg = group.to_string() + r#" group!"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// wait group end
#[no_mangle]
pub extern "C" fn program_push_wait_group_end(program: Box<Program>, group: libc::uint32_t) {
    let program = Box::into_raw(program);
    let msg = group.to_string() + r#" until-grp-end"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// move mcs
#[no_mangle]
pub extern "C" fn program_push_move_mcs(program: Box<Program>) {
    let program = Box::into_raw(program);
    let msg = r#"mcs"#.to_string();
    unsafe {
        (*program).push_line(&msg);
    }
}

/// move1d
#[no_mangle]
pub extern "C" fn program_push_move1d(program: Box<Program>, x: libc::c_double) {
    let program = Box::into_raw(program);
    let msg = x.to_string() + r#"e move1d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// line1d
#[no_mangle]
pub extern "C" fn program_push_line1d(program: Box<Program>, x: libc::c_double) {
    let program = Box::into_raw(program);
    let msg = x.to_string() + r#"e line1d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// move2d
#[no_mangle]
pub extern "C" fn program_push_move2d(program: Box<Program>, x: libc::c_double, y: libc::c_double) {
    let program = Box::into_raw(program);
    let msg = x.to_string() + r#"e "# + y.to_string().as_str() + r#"e move2d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// line2d
#[no_mangle]
pub extern "C" fn program_push_line2d(program: Box<Program>, x: libc::c_double, y: libc::c_double) {
    let program = Box::into_raw(program);
    let msg = x.to_string() + r#"e "# + y.to_string().as_str() + r#"e line2d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// arc2d
#[no_mangle]
pub extern "C" fn program_push_arc2d(
    program: Box<Program>,
    cx: libc::c_double,
    cy: libc::c_double,
    tx: libc::c_double,
    ty: libc::c_double,
    rev: libc::int32_t,
) {
    let program = Box::into_raw(program);
    let msg = cx.to_string() + r#"e "# + cy.to_string().as_str() + r#"e "# + tx.to_string().as_str()
        + r#"e "# + ty.to_string().as_str() + r#"e "# + rev.to_string().as_str()
        + r#" arc2d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// move3d
#[no_mangle]
pub extern "C" fn program_push_move3d(
    program: Box<Program>,
    x: libc::c_double,
    y: libc::c_double,
    z: libc::c_double,
) {
    let program = Box::into_raw(program);
    let msg = x.to_string() + r#"e "# + y.to_string().as_str() + r#"e "# + z.to_string().as_str()
        + r#"e move3d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// line3d
#[no_mangle]
pub extern "C" fn program_push_line3d(
    program: Box<Program>,
    x: libc::c_double,
    y: libc::c_double,
    z: libc::c_double,
) {
    let program = Box::into_raw(program);
    let msg = x.to_string() + r#"e "# + y.to_string().as_str() + r#"e "# + z.to_string().as_str()
        + r#"e line3d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}

/// helix3d
#[no_mangle]
pub extern "C" fn program_push_helix3d(
    program: Box<Program>,
    cx: libc::c_double,
    cy: libc::c_double,
    tx: libc::c_double,
    ty: libc::c_double,
    tz: libc::c_double,
    rev: libc::int32_t,
) {
    let program = Box::into_raw(program);
    let msg = cx.to_string() + r#"e "# + cy.to_string().as_str() + r#"e "# + tx.to_string().as_str()
        + r#"e "# + ty.to_string().as_str() + r#"e "# + tz.to_string().as_str()
        + r#"e "# + rev.to_string().as_str() + r#" helix3d"#;
    unsafe {
        (*program).push_line(&msg);
    }
}
