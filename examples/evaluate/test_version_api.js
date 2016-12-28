"use strict";

var botnana = require('../../index');

function test_version_api() {
    // Show reponse data
    botnana.debug_level = 0;
    // Event API
    botnana.on("version", function(version) {
        console.log("version: " + version);
        process.exit();
    });
    botnana.on("ready", function() {
        // Version API
        botnana.version.get();
    });
    botnana.start("ws://192.168.7.2:3012");
 }

test_version_api();

