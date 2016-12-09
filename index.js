"use strict";

var botnana = {};

// Event API
botnana.handlers = {}

botnana.on = function(tag, handler) {
    botnana.handlers[tag] = handler;
};

botnana.motion = {};
botnana.motion.handlers = {};
botnana.motion.on = function(tag, handler) {
    botnana.motion.handlers[tag] = handler;
}

botnana.handle_response = function(resp) {
    let r = resp.split("|");
    for (var i=0; i< r.length; i=i+2) {
        if(botnana.handlers[r[i]]) {
            botnana.handlers[r[i]](r[i+1]);
        }
    }
};

// Version API
botnana.version = {
    get: function(sender) {
        var json = {
            spec_version: "0.0.1",
            target: "version",
            command: "get"
        };
        sender.send(JSON.stringify(json));
    }
}

// Real-time script API
botnana.motion.evaluate = function(sender, script) {
    var json = {
        spec_version: "0.0.1",
        target: "motion",
        command: "evaluate",
        arguments: {
            script: script
        }
    };
    sender.send(JSON.stringify(json));
};

// Slave API
class Slave {
    constructor() {
        this.on = (tag, handler) => {
            this.handlers[tag] = hanlder;
        };
        this.set = function(args) {};
        this.get = function(tag) {};
        this.set_homing_method = function(value) {};
        this.set_dout = function(index, value) {};
        this.get_dout = function(index) {};
        this.get_din = function(index) {};
        this.disable_aout(index);
        this.enable_aout(index);
        this.set_aout = function(index, value) {};
        this.get_aout = function(index) {};
        this.disable_ain = function(index) {};
        this.enable_ain = function(index) {};
        this.get_ain = function(index) {};
    }
}

botnana.motion.slaves = [];
botnana.motion.slave = function(n) {
    if (botnana.motion.slaves.len >= n) {
        return botnana.motion.slaves[n-1];
    }
}

// Configuration API
botnana.config = {
    save: function(sender) {
        var json = {
            spec_version: "0.0.1",
            target: "config",
            command: "save"
        };
        sender.send(JSON.stringify(json));
    },
    set_slave: function(sender, args) {
        var json = {
            spec_version: "0.0.1",
            target: "config",
            command: "set_slave",
            arguments: args
        };
        sender.send(JSON.stringify(json));
    }
}

module.exports = botnana;
