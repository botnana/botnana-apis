extern crate libc;
use std::{
    str,
    sync::{Arc, Mutex},
};

/// Program
#[repr(C)]
#[derive(Clone)]
pub struct Program {
    pub name: String,
    pub lines: Arc<Mutex<String>>,
}

impl Program {
    /// New
    pub fn new(name: &str) -> Program {
        let mut line = String::new();
        line.push_str(&(": user$".to_owned() + name + "\n"));
        Program {
            name: name.to_string(),
            lines: Arc::new(Mutex::new(line)),
        }
    }

    /// push program line
    pub fn push_line(&mut self, script: &str) {
        let lines = self.lines.clone();
        lines.lock().unwrap().push_str(&(script.to_owned() + "\n"));
    }

    /// clear program
    pub fn clear(&mut self) {
        // 此處 cmd 使用 &str，因為後續會轉成 string, 所以應該不會被垃圾收集器回收
        let lines = self.lines.clone();
        let mut line = lines.lock().unwrap();
        line.clear();
        line.push_str(&(": user$".to_owned() + &self.name + "\n"));
    }
}
