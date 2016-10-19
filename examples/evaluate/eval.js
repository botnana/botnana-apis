"use strict";

var WebSocket = require('ws');
var botnana = {};

botnana.ws = new WebSocket('ws://192.168.7.2:3012');

botnana.ws.on('open', function open() {
    botnana.is_connected = true;
});

botnana.ws.on('message', function(data, flags) {
    console.log(data);
});

botnana.motion = {
    evaluate: function(script) {
        var json = "{\"spec_version\":\"0.0.1\",\"target\":\"motion\",\"command\":\"evaluate\",\"arguments\":{\"script\":\""
        + script + "\"}}";    
        botnana.ws.send(json);
    }
}

function test_botnana() {
    var script = "words";
    botnana.motion.evaluate(script);
}

setTimeout(test_botnana, 500);
