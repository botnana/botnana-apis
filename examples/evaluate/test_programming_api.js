
"use strict"

var botnana = require('../../index')

function test_programming_api() {
    // Show reponse data
    botnana.debug_level = 0;
    botnana.on("ready", function() {
        // Programming API                
        var p1 = new botnana.Program("p1");
//        var s1 = p1.ethercat.slave(1);
//        s1.hm();
        p1.deploy();
        setTimeout(function() {
            p1.run();
        }, 1000);
    })
    botnana.start("ws://192.168.7.2:3012");
}

test_programming_api();
