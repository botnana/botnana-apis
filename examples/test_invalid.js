"use strict";
var botnana = require("../index");

function test_invalid() {
  botnana.debug_level = 1;
  botnana.once("ready", function() {
    // Fort valid slave from 1 to 8
    //   Test .slave and .slave-diff
    var script = "0 .slave";
    botnana.motion.evaluate(script);
    var script = "0 .slave-diff";
    botnana.motion.evaluate(script);
    var script = "9 .slave";
    botnana.motion.evaluate(script);
    var script = "9 .slave-diff";
    botnana.motion.evaluate(script);
    //   Test drive
    var script = "hm 0 op-mode!";
    botnana.motion.evaluate(script);
    var script = "hm 9 op-mode!";
    botnana.motion.evaluate(script);
    var script = "2 0 pds-goal!";
    botnana.motion.evaluate(script);
    var script = "2 9 pds-goal!";
    botnana.motion.evaluate(script);
    var script = "0 reset-fault";
    botnana.motion.evaluate(script);
    var script = "9 reset-fault";
    botnana.motion.evaluate(script);
    var script = "0 go";
    botnana.motion.evaluate(script);
    var script = "9 go";
    botnana.motion.evaluate(script);
    var script = "0 0 jog";
    botnana.motion.evaluate(script);
    var script = "0 9 jog";
    botnana.motion.evaluate(script);
    var script = "0 target-reached?";
    botnana.motion.evaluate(script);
    var script = "9 target-reached?";
    botnana.motion.evaluate(script);
    var script = "0 0 home-offset!";
    botnana.motion.evaluate(script);
    var script = "0 9 home-offset!";
    botnana.motion.evaluate(script);
    var script = "0 0 homing-a!";
    botnana.motion.evaluate(script);
    var script = "0 9 homing-a!";
    botnana.motion.evaluate(script);
    var script = "0 0 homing-method!";
    botnana.motion.evaluate(script);
    var script = "0 9 homing-method!";
    botnana.motion.evaluate(script);
    var script = "0 0 homing-v1!";
    botnana.motion.evaluate(script);
    var script = "0 9 homing-v1!";
    botnana.motion.evaluate(script);
    var script = "0 0 homing-v2!";
    botnana.motion.evaluate(script);
    var script = "0 9 homing-v2!";
    botnana.motion.evaluate(script);
    var script = "0 0 profile-a1!";
    botnana.motion.evaluate(script);
    var script = "0 9 profile-a1!";
    botnana.motion.evaluate(script);
    var script = "0 0 profile-a2!";
    botnana.motion.evaluate(script);
    var script = "0 9 profile-a2!";
    botnana.motion.evaluate(script);
    var script = "0 0 profile-v!";
    botnana.motion.evaluate(script);
    var script = "0 9 profile-v!";
    botnana.motion.evaluate(script);

    // For EC7062 at position 4
    //   Test ec-dout@
    //     Invalid slave
    var script = "0 1 0 ec-dout@";
    botnana.motion.evaluate(script);
    var script = "0 1 9 ec-dout@";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 0 4 ec-dout@";
    botnana.motion.evaluate(script);
    var script = "0 1 4 ec-dout@";
    botnana.motion.evaluate(script);
    var script = "0 4 4 ec-dout@";
    botnana.motion.evaluate(script);
    var script = "0 5 4 ec-dout@";
    botnana.motion.evaluate(script);
    //   Test ec-dout!
    //     Invalid slave
    var script = "0 1 0 ec-dout!";
    botnana.motion.evaluate(script);
    var script = "0 1 9 ec-dout!";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 0 4 ec-dout!";
    botnana.motion.evaluate(script);
    var script = "0 1 4 ec-dout!";
    botnana.motion.evaluate(script);
    var script = "0 4 4 ec-dout!";
    botnana.motion.evaluate(script);
    var script = "0 5 4 ec-dout!";
    botnana.motion.evaluate(script);

    // For EC6022 at position 5
    //   Test ec-din@
    //     Invalid slave
    var script = "0 1 0 ec-din@";
    botnana.motion.evaluate(script);
    var script = "0 1 9 ec-din@";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 0 5 ec-din@";
    botnana.motion.evaluate(script);
    var script = "0 1 5 ec-din@";
    botnana.motion.evaluate(script);
    var script = "0 4 5 ec-din@";
    botnana.motion.evaluate(script);
    var script = "0 5 5 ec-din@";
    botnana.motion.evaluate(script);

    // For EC8124 at position 6
    //   Test +ec-ain
    //     Invalid slave
    var script = "1 0 +ec-ain";
    botnana.motion.evaluate(script);
    var script = "1 9 +ec-ain";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 6 +ec-ain";
    botnana.motion.evaluate(script);
    var script = "1 6 +ec-ain";
    botnana.motion.evaluate(script);
    var script = "4 6 +ec-ain";
    botnana.motion.evaluate(script);
    var script = "5 6 +ec-ain";
    botnana.motion.evaluate(script);
    //   Test -ec-ain
    //     Invalid slave
    var script = "1 0 -ec-ain";
    botnana.motion.evaluate(script);
    var script = "1 9 -ec-ain";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 6 -ec-ain";
    botnana.motion.evaluate(script);
    var script = "1 6 -ec-ain";
    botnana.motion.evaluate(script);
    var script = "4 6 -ec-ain";
    botnana.motion.evaluate(script);
    var script = "5 6 -ec-ain";
    botnana.motion.evaluate(script);
    //   Test ec-ain@
    //     Invalid slave
    var script = "0 1 0 ec-ain@";
    botnana.motion.evaluate(script);
    var script = "0 1 9 ec-ain@";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 0 6 ec-ain@";
    botnana.motion.evaluate(script);
    var script = "0 1 6 ec-ain@";
    botnana.motion.evaluate(script);
    var script = "0 4 6 ec-ain@";
    botnana.motion.evaluate(script);
    var script = "0 5 6 ec-ain@";
    botnana.motion.evaluate(script);

    // For EC9144 at position 7
    //   Test +ec-aout
    //     Invalid slave
    var script = "1 0 +ec-aout";
    botnana.motion.evaluate(script);
    var script = "1 9 +ec-aout";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 7 +ec-aout";
    botnana.motion.evaluate(script);
    var script = "1 7 +ec-aout";
    botnana.motion.evaluate(script);
    var script = "4 7 +ec-aout";
    botnana.motion.evaluate(script);
    var script = "5 7 +ec-aout";
    botnana.motion.evaluate(script);
    //   Test -ec-aout
    //     Invalid slave
    var script = "1 0 -ec-aout";
    botnana.motion.evaluate(script);
    var script = "1 9 -ec-aout";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 7 -ec-aout";
    botnana.motion.evaluate(script);
    var script = "1 7 -ec-aout";
    botnana.motion.evaluate(script);
    var script = "4 7 -ec-aout";
    botnana.motion.evaluate(script);
    var script = "5 7 -ec-aout";
    botnana.motion.evaluate(script);
    //   Test ec-aout!
    //     Invalid slave
    var script = "0 1 0 ec-aout!";
    botnana.motion.evaluate(script);
    var script = "0 1 9 ec-aout!";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 0 7 ec-aout!";
    botnana.motion.evaluate(script);
    var script = "0 1 7 ec-aout!";
    botnana.motion.evaluate(script);
    var script = "0 4 7 ec-aout!";
    botnana.motion.evaluate(script);
    var script = "0 5 7 ec-aout!";
    botnana.motion.evaluate(script);
    //   Test ec-aout@
    //     Invalid slave
    var script = "0 1 0 ec-aout@";
    botnana.motion.evaluate(script);
    var script = "0 1 9 ec-aout@";
    botnana.motion.evaluate(script);
    //     Invalid channel
    var script = "0 0 7 ec-aout@";
    botnana.motion.evaluate(script);
    var script = "0 1 7 ec-aout@";
    botnana.motion.evaluate(script);
    var script = "0 4 7 ec-aout@";
    botnana.motion.evaluate(script);
    var script = "0 5 7 ec-aout@";
    botnana.motion.evaluate(script);
  });
  botnana.start("ws://192.168.7.2:3012");
}

test_invalid();
