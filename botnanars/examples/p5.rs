extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        let mut p5 = btn.lock().unwrap().program("p5");
        let mut douts = p5.ethercat.slave(1).unwrap();
        let mut dins = p5.ethercat.slave(2).unwrap();
        let mut aouts = p5.ethercat.slave(3).unwrap();
        let mut ains = p5.ethercat.slave(4).unwrap();

        aouts.disable_aout(1);
        ains.disable_ain(1);
        aouts.set_aout(1, 0);
        douts.set_dout(1, 0);
        p5.ms(1000);
        aouts.enable_aout(1);
        ains.enable_ain(1);
        p5.ms(1000);
        douts.set_dout(1, 0);
        douts.set_dout(1, 1);
        p5.ms(1000);
        aouts.set_aout(1, 0);
        aouts.set_aout(1, 10000);

        p5.deploy();
    });

    let btn = botnana.clone();
    botnana.lock().unwrap().once("deployed", move |_| {
        let p5 = btn.lock().unwrap().program("p5");
        p5.run();
    });

    botnana.lock().unwrap().start("ws://localhost:3012");
    loop {}
}
