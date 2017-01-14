"use strict"

var botnana = require('../../index')

function test() {
    // Show reponse data
    botnana.debug_level = 1;
    botnana.on("ready", function() {
        // Programming API                
        var Program = botnana.Program;
        // Program p
        var p = new Program("p");
        var s1 = p.ethercat.slave(1);
        s1.move_to(50000);
        p.deploy();
        // Run Program p1
        botnana.on("deployed", function() { p.run(); });
    })
    botnana.start("ws://192.168.1.117:3012");
}

test();
