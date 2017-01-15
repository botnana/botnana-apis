"use strict"

var botnana = require('../../index')

function test() {
    // Show reponse data
    botnana.debug_level = 1;
    botnana.once("ready", function() {
        // Programming API                
        var Program = botnana.Program;
        // Program p2
        var p2 = new Program("p2");
        var s1 = p2.ethercat.slave(1);
        s1.hm();
        s1.go();
        s1.pp();
        s1.move_to(30000);
        s1.go();
        p2.deploy();
        // Run Program p1
        botnana.once("deployed", function() { p2.run(); });
    })
    botnana.start("ws://192.168.7.2:3012");
}

test();
