extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        let mut p = btn.lock().unwrap().program("p4");
        let mut s1 = p.ethercat.slave(1).unwrap();
        let mut s2 = p.ethercat.slave(2).unwrap();
        s1.hm();
        s2.hm();
        s1.go();
        s1.until_target_reached();
        s2.go();
        s1.pp();
        s2.pp();
        s1.move_to(30000);
        s1.go();
        s1.until_target_reached();
        s2.move_to(40000);
        s2.go();

        p.deploy();
    });

    let btn = botnana.clone();
    botnana.lock().unwrap().once("deployed", move |_| {
        let p = btn.lock().unwrap().program("p4");
        p.run();
    });

    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
