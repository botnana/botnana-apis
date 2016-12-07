"use strict";

var botnana = require('../../index');

function test_botnana() {
    var script = "words";
    botnana.version.info();
    botnana.motion.evaluate(script);
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
}

setTimeout(test_botnana, 500);
