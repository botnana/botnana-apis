extern crate botnanars;
use botnanars::botnana;
use botnanars::programmed::Program;
use std::{thread, time};
use std::sync::{Arc, Mutex};

fn main() {
    let mut botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |msg| {
        let script = "empty marker empty";
        btn.lock().unwrap().evaluate(script);
    });

    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
