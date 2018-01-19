"use strict";

var botnana = require("../../index");

function test() {
  // Show reponse data
  botnana.debug_level = 1;
  botnana.once("ready", function() {
    // Programming API
    var Program = botnana.Program;
    // Program p
    var p = new Program("hm");
    var s1 = p.ethercat.slave(1);
    s1.hm();
    p.deploy();
    // Run Program p1    
    botnana.once("deployed", function() {
      p.run();
    });
  });
  //botnana.start("ws://192.168.7.2:3012");
botnana.start("ws://192.168.50.222:3012");
}

test();
