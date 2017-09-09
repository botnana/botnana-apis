"use strict";

var botnana = require("../../index");

function test() {
  // Show reponse data
  botnana.debug_level = 0;
  botnana.once("ready", function() {
    // Programming API
    var Program = botnana.Program;
    // Program p1
    // Program p4
    var p4 = new Program("p4");
    var s1 = p4.ethercat.slave(1);
    var s2 = p4.ethercat.slave(2);
    s1.hm();
    s2.hm();
    s1.go();
    s1.until_target_reached();
    s2.go();
    s1.pp();
    s2.pp();
    s1.move_to(30000);
    s1.go();
    s1.until_target_reached();
    s2.move_to(40000);
    s2.go();
    p4.deploy();
    // Run Program p4
    botnana.once("deployed", function() {
      p4.run();
    });
  });
  botnana.start("ws://192.168.7.2:3012");
}

test();
