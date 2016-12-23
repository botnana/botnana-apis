"use strict";

var botnana = require('../../index');

function test_botnana() {
    // Event API
    botnana.on("version", function(version) {
        console.log("version: " + version);
    });
    botnana.on("ready", function() {
        // Version API
        botnana.version.get();
        // Real-time script API
        var script = "words";
        botnana.motion.evaluate(script);
    	botnana.motion.evaluate("1 reset-fault");
        // Configuration API
        botnana.config.set_slave({
            position: 1,
            tag: "homing_method",
            value: 33
        });
        botnana.config.set_slave({
            position: 1,
            tag: "homing_speed_1",
            value: 18000
        });
        botnana.config.save();
        // Slave API
        botnana.ethercat.slave(1).set("home_offset",2800);
        botnana.ethercat.slave(1).set("homing_method",32);
        botnana.ethercat.slave(1).set("homing_speed_1",1900000);
        botnana.ethercat.slave(1).set("homing_speed_2",29000);
        botnana.ethercat.slave(1).set("homing_acceleration",39000);
        botnana.ethercat.slave(1).set("profile_velocity",18000000);
        botnana.ethercat.slave(1).set("profile_acceleration",280000);
        botnana.ethercat.slave(1).set("profile_deceleration",380000);
        botnana.on("homing_speed_1.1", function(value) {
            console.log("homing_speed_1.1 = " + value);
        });
        botnana.ethercat.slave(1).get();
        botnana.ethercat.slave(3).get();
        botnana.ethercat.slave(4).get();
        botnana.ethercat.slave(5).get();
        botnana.ethercat.slave(6).get();
        botnana.ethercat.slave(7).get();
        botnana.ethercat.slave(1).get_diff();
                
    });
    botnana.start("ws://192.168.7.2:3012");
 }

setTimeout(test_botnana, 500);

setTimeout(function() {
    console.log("set homing_speed_1.1 to 490000");
    botnana.ethercat.slave(1).set("homing_speed_1",4900000);
    botnana.ethercat.slave(1).get_diff();
}, 5000)
