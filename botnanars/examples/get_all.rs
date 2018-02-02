extern crate botnanars;
use botnanars::botnana;
use std::{thread, time};
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(botnana::botnana::new().unwrap()));

    let btn = botnana.clone();

    botnana.lock().unwrap().once("ready", move |_| {
        let slave_count = btn.lock().unwrap().ethercat.get_slaves_count();
        for i in 1..slave_count {
            match btn.lock().unwrap().ethercat.slave(i) {
                Some(s) => s.get(),
                None => {
                    println!("get None");
                }
            }
        }

        loop {
            for i in 1..slave_count {
                match btn.lock().unwrap().ethercat.slave(i) {
                    Some(s) => s.get_diff(),
                    None => {
                        println!("get None");
                    }
                }
            }
            thread::sleep(time::Duration::from_millis(2000));
        }
    });

    botnana.lock().unwrap().start("ws://localhost:3012");

    loop {}
}
