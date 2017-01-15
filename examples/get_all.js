"use strict";

var botnana = require('../index');

function get_all() {
    // Show reponse data
    botnana.debug_level = 1;
    botnana.once("ready", function() {
        for (var i = 1; i <= botnana.ethercat.slave_count; i = i + 1) {
            botnana.ethercat.slave(i).get();
        }
    });

    botnana.start("ws://192.168.7.2:3012");
 }

get_all();

setTimeout(function() {
    botnana.ethercat.slave(1).get_diff();
}, 2000)
