"use strict"

var botnana = require('../../index')

function test() {
    // Show reponse data
    botnana.debug_level = 0;
    botnana.once("ready", function() {
        // Programming API
        var Program = botnana.Program;
        // Program p5
        var p5 = new Program("p5");
        var douts = p5.ethercat.slave(1);
        var dins = p5.ethercat.slave(2);
        var aouts = p5.ethercat.slave(3);
        var ains = p5.ethercat.slave(4);
        aouts.disable_aout(1);
        ains.disable_ain(1);
        aouts.set_aout(1, 0);
        douts.set_dout(1, 0);
        p5.ms(1000);
        aouts.enable_aout(1);
        ains.enable_ain(1);
        p5.DO(0, 10);
            p5.ms(1000);
            p5.IF(dins.din(1));
                douts.set_dout(1, 0);
            p5.ELSE()
                douts.set_dout(1, 1);
            p5.THEN();
        p5.LOOP();
        p5.BEGN();
            p5.ms(1000);
            p5.IF(ains.ain(1).GT(1000));
                p5.aouts.set_aout(1, 0);
            p5.ELSE();
                p5.aouts.set_aout(1, 10000);
            p5.THEN();
        p5.AGAIN();
        p5.deploy();        
        // Run Program p1
        botnana.once("deployed", function() { p5.run(); });
    })
    botnana.start("ws://192.168.7.2:3012");
}

test();
