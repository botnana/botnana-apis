
"use strict"

var botnana = require('../../index')

function test() {
    // Show reponse data
    botnana.debug_level = 0;
    botnana.on("ready", function() {
        // Programming API                
        var Program = botnana.Program;
        // Program p3
        var p3 = new Program("p3");
        var s1 = p3.ethercat.slave(1);
        var s2 = p3.ethercat.slave(2);
        s1.hm();
        s2.hm();
        s1.go();
        s2.go();
        s1.pp();
        s2.pp();
        s1.move_to(30000);
        s2.move_to(40000);
        s1.go();
        s2.go();
        p3.deploy();
        // Run Program p3
        setTimeout(function() {
            p3.run();
        }, 1000);
    })
    botnana.start("ws://192.168.7.2:3012");
}

test();
