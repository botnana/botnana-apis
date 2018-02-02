extern crate botnanars;
use botnanars::botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        let script = "words";
        btn.lock().unwrap().evaluate(script);
    });

    botnana.lock().unwrap().start("ws://192.168.50.197:3012");

    loop {}
}
