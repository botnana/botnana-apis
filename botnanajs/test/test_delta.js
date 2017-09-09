"use strict";

// Setup 1 由台達電的模組構成。
//
var botnana = require("../index");

function test_delta() {
  // Show reponse data
  botnana.debug_level = 1;
  botnana.once("ready", function() {
    var script = "1 .slave 2 .slave 3 .slave 4 .slave 5 .slave 6 .slave";
    botnana.motion.evaluate(script);
    setInterval(
      function() {
        var script = "1 .slave-diff 2 .slave-diff 3 .slave-diff 4 .slave-diff 5 .slave-diff 6 .slave-diff";
        botnana.motion.evaluate(script);
      },
      2000
    );
  });

  botnana.start("ws://192.168.1.117:3012");
}

test_delta();
