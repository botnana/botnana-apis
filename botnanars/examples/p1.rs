extern crate botnanars;
use botnanars::botnana;
use botnanars::programmed::Program;
use std::{thread, time};
use std::sync::{Arc, Mutex};

fn main() {
    let mut botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |msg| {
        let mut p = btn.lock().unwrap().program("p1");
        p.deploy();
    });

    let btn = botnana.clone();
    botnana.lock().unwrap().once("deployed", move |msg| {
        let p = btn.lock().unwrap().program("p1");
        p.run();
    });

    botnana.lock().unwrap().start("ws://192.168.50.197:3012");
    loop {}
}
