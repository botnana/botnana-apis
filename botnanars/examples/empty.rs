extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        let script = "empty marker empty";
        btn.lock().unwrap().evaluate(script);
    });

    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
