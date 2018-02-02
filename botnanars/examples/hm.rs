extern crate botnanars;
use botnanars::botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));

    let btn = botnana.clone();

    botnana.lock().unwrap().once("ready", move |_| {
        let mut p = btn.lock().unwrap().program("hm");

        let s1 = p.ethercat.slave(1);
        match s1 {
            Some(mut s) => {
                s.hm();
            }
            None => {}
        }

        p.deploy();
    });

    let btn = botnana.clone();

    botnana.lock().unwrap().once("deployed", move |_| {
        let p = btn.lock().unwrap().program("hm");
        p.run();
    });

    // botnana.lock().unwrap().start("ws://192.168.50.240:3012");
    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
