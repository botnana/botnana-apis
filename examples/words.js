"use strict";

var botnana = require('../index');

function words() {
    botnana.debug_level = 1;
    botnana.once("ready", function() {
        // Real-time script API
        var script = "words";
        botnana.motion.evaluate(script);
    });
    botnana.start("ws://192.168.7.2:3012");
 }

words();