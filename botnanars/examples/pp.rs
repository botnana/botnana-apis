extern crate botnanars;
use botnanars::botnana;
use botnanars::programmed::Program;
use std::{thread, time};
use std::sync::{Arc, Mutex};

fn main() {
    let mut botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |msg| {
        let mut p = btn.lock().unwrap().program("pp");
        let s1 = p.ethercat.slave(1);
        match s1 {
            Some(mut s) => {
                s.pp();
            }
            None => {}
        }

        p.deploy();
    });

    let btn = botnana.clone();
    botnana.lock().unwrap().once("deployed", move |msg| {
        let p = btn.lock().unwrap().program("pp");
        p.run();
    });

    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
