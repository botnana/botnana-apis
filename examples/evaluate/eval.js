"use strict";

var botnana = require('../../index');

function test_botnana() {
    // Websocket
    var WebSocket = require('ws');
    var ws = new WebSocket('ws://192.168.7.2:3012');
    // ws = new WebSocket('ws://localhost:3012');

    ws.on('message', function(data, flags) {
        console.log(data);
        botnana.handle_response(data);
    });

    ws.on('open', function () {
        // Version API
        botnana.on("version", function(version) {
            console.log("version: " + version);
        });
        botnana.version.get(ws);
        // Real-time script API
        var script = "words";
        botnana.motion.evaluate(ws, script);
        // Configuration API
        botnana.config.set_slave(ws, {
            position: 1,
            tag: "homing_method",
            value: 33
        });
        botnana.config.set_slave(ws, {
            position: 1,
            tag: "homing_speed_1",
            value: 18000
        });
        botnana.config.save(ws);
    });
}

setTimeout(test_botnana, 500);
