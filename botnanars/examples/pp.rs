extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
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
    botnana.lock().unwrap().once("deployed", move |_| {
        let p = btn.lock().unwrap().program("pp");
        p.run();
    });

    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
