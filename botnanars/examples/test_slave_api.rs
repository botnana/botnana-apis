extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new()));

    // TODO botnana.lock().unwrap().set_debug(0);

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        btn.lock()
            .unwrap()
            .set_slave(r#"{"position":1,"tag":"homing_method","value":33}"#);
        btn.lock()
            .unwrap()
            .set_slave(r#"{"position":1,"tag":"homing_speed_1","value":18000}"#);
        btn.lock().unwrap().save();

        match btn.lock().unwrap().ethercat.slave(1) {
            Some(slave) => {
                slave.set("home_offset", 2800);
                slave.set("homing_method", 32);
                slave.set("homing_speed_1", 1900000);
                slave.set("homing_speed_2", 29000);
                slave.set("homing_acceleration", 39000);
                slave.set("profile_velocity", 18000000);
                slave.set("profile_acceleration", 280000);
                slave.set("profile_deceleration", 380000);
            }
            None => {}
        }

        let slave_count = btn.lock().unwrap().ethercat.get_slaves_count();

        for i in 1..slave_count {
            match btn.lock().unwrap().ethercat.slave(i) {
                Some(slave) => {
                    slave.get();
                }
                None => {}
            }
        }

        match btn.lock().unwrap().ethercat.slave(1) {
            Some(slave) => {
                slave.get_diff();
            }
            None => {}
        }
    });

    botnana
        .lock()
        .unwrap()
        .on("homing_speed_1.1", move |value| {
            println!("homing_speed_1.1 = {:?}", value);
        });

    botnana.lock().unwrap().start("ws://localhost:3012");

    loop {}
}
