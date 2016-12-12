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
        botnana.ethercat.slave(1).on("product", function(product) {
            console.log(product);
        });
        botnana.ethercat.slave(1).on("drive", function(drive) {
            console.log(drive);
        });
        botnana.ethercat.slave(1).get();
        botnana.ethercat.slave(1).set({
            tag: "homing_method",
            value: 33
        });
                
    });
    botnana.start("ws://192.168.7.2:3012");
 }

setTimeout(test_botnana, 500);
