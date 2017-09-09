"use strict";

var botnana = require("../../index");

function test() {
  // Show reponse data
  botnana.debug_level = 1;
  botnana.once("ready", function() {
    // Programming API
    var Program = botnana.Program;
    // Program p1
    var p1 = new Program("p1");
    p1.deploy();
    // Run Program p1
    botnana.once("deployed", function() {
      p1.run();
    });
  });
  botnana.start("ws://192.168.7.2:3012");
}

test();
