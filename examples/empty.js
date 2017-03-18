"use strict";

var botnana = require("../index");

botnana.once("ready", function() {
  // Real-time script API
  var script = "empty  marker empty";
  botnana.motion.evaluate(script);
});

botnana.start("ws://192.168.7.2:3012");
