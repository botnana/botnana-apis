extern crate botnanars;
use botnanars::botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));
    let btn = botnana.clone();

    botnana.lock().unwrap().once("version", move |version| {
        println!("version is {:?}", version);
    });

    botnana.lock().unwrap().once("ready", move |_| {
        btn.lock().unwrap().version();
    });

    botnana.lock().unwrap().start("ws://192.68.7.2:3012");

    loop {}
}
