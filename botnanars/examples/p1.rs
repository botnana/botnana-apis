extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        let mut p = btn.lock().unwrap().program("p1");
        p.deploy();
    });

    let btn = botnana.clone();
    botnana.lock().unwrap().once("deployed", move |_| {
        let p = btn.lock().unwrap().program("p1");
        p.run();
    });

    botnana.lock().unwrap().start("ws://192.168.50.197:3012");
    loop {}
}
