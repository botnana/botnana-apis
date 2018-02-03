extern crate botnanars;
use botnanars::Botnana;
use std::sync::{Arc, Mutex};

fn main() {
    let botnana = Arc::new(Mutex::new(Botnana::new().unwrap()));

    let btn = botnana.clone();
    botnana.lock().unwrap().once("ready", move |_| {
        // Fort valid slave from 1 to 8
        //   Test .slave and .slave-diff
        let script = "0 .slave";
        btn.lock().unwrap().evaluate(script);
        let script = "0 .slave-diff";
        btn.lock().unwrap().evaluate(script);
        let script = "9 .slave";
        btn.lock().unwrap().evaluate(script);
        let script = "9 .slave-diff";
        btn.lock().unwrap().evaluate(script);
        //   Test drive
        let script = "hm 0 op-mode!";
        btn.lock().unwrap().evaluate(script);
        let script = "hm 9 op-mode!";
        btn.lock().unwrap().evaluate(script);
        let script = "2 0 pds-goal!";
        btn.lock().unwrap().evaluate(script);
        let script = "2 9 pds-goal!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 reset-fault";
        btn.lock().unwrap().evaluate(script);
        let script = "9 reset-fault";
        btn.lock().unwrap().evaluate(script);
        let script = "0 go";
        btn.lock().unwrap().evaluate(script);
        let script = "9 go";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 jog";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 jog";
        btn.lock().unwrap().evaluate(script);
        let script = "0 target-reached?";
        btn.lock().unwrap().evaluate(script);
        let script = "9 target-reached?";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 home-offset!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 home-offset!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 homing-a!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 homing-a!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 homing-method!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 homing-method!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 homing-v1!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 homing-v1!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 homing-v2!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 homing-v2!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 profile-a1!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 profile-a1!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 profile-a2!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 profile-a2!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 0 profile-v!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 9 profile-v!";
        btn.lock().unwrap().evaluate(script);

        // For EC7062 at position 4
        //   Test ec-dout@
        //     Invalid slave
        let script = "0 1 0 ec-dout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 9 ec-dout@";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 0 4 ec-dout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 4 ec-dout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 4 4 ec-dout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 5 4 ec-dout@";
        btn.lock().unwrap().evaluate(script);
        //   Test ec-dout!
        //     Invalid slave
        let script = "0 1 0 ec-dout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 9 ec-dout!";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 0 4 ec-dout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 4 ec-dout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 4 4 ec-dout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 5 4 ec-dout!";
        btn.lock().unwrap().evaluate(script);

        // For EC6022 at position 5
        //   Test ec-din@
        //     Invalid slave
        let script = "0 1 0 ec-din@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 9 ec-din@";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 0 5 ec-din@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 5 ec-din@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 4 5 ec-din@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 5 5 ec-din@";
        btn.lock().unwrap().evaluate(script);

        // For EC8124 at position 6
        //   Test +ec-ain
        //     Invalid slave
        let script = "1 0 +ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "1 9 +ec-ain";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 6 +ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "1 6 +ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "4 6 +ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "5 6 +ec-ain";
        btn.lock().unwrap().evaluate(script);
        //   Test -ec-ain
        //     Invalid slave
        let script = "1 0 -ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "1 9 -ec-ain";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 6 -ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "1 6 -ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "4 6 -ec-ain";
        btn.lock().unwrap().evaluate(script);
        let script = "5 6 -ec-ain";
        btn.lock().unwrap().evaluate(script);
        //   Test ec-ain@
        //     Invalid slave
        let script = "0 1 0 ec-ain@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 9 ec-ain@";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 0 6 ec-ain@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 6 ec-ain@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 4 6 ec-ain@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 5 6 ec-ain@";
        btn.lock().unwrap().evaluate(script);

        // For EC9144 at position 7
        //   Test +ec-aout
        //     Invalid slave
        let script = "1 0 +ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "1 9 +ec-aout";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 7 +ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "1 7 +ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "4 7 +ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "5 7 +ec-aout";
        btn.lock().unwrap().evaluate(script);
        //   Test -ec-aout
        //     Invalid slave
        let script = "1 0 -ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "1 9 -ec-aout";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 7 -ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "1 7 -ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "4 7 -ec-aout";
        btn.lock().unwrap().evaluate(script);
        let script = "5 7 -ec-aout";
        btn.lock().unwrap().evaluate(script);
        //   Test ec-aout!
        //     Invalid slave
        let script = "0 1 0 ec-aout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 9 ec-aout!";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 0 7 ec-aout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 7 ec-aout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 4 7 ec-aout!";
        btn.lock().unwrap().evaluate(script);
        let script = "0 5 7 ec-aout!";
        btn.lock().unwrap().evaluate(script);
        //   Test ec-aout@
        //     Invalid slave
        let script = "0 1 0 ec-aout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 9 ec-aout@";
        btn.lock().unwrap().evaluate(script);
        //     Invalid channel
        let script = "0 0 7 ec-aout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 1 7 ec-aout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 4 7 ec-aout@";
        btn.lock().unwrap().evaluate(script);
        let script = "0 5 7 ec-aout@";
        btn.lock().unwrap().evaluate(script);
    });

    botnana.lock().unwrap().start("ws://192.168.50.197:3012");

    loop {}
}
