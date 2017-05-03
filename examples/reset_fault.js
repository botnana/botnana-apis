"use strict";

var botnana = require("../index");
botnana.debug_level=1;

botnana.once("ready", function() {
  // Real-time script API
  var script = "7 reset-fault  1 7 pds-goal!";
  botnana.motion.evaluate(script);
});

botnana.start("ws://192.168.7.2:3012");
