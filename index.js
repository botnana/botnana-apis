"use strict";

var botnana = {
    sender: null,
    _: {}
};

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
    get: function() {
        var json = {
            jsonrpc: "2.0",
            method: "version.get",
        };
        botnana.sender.send(JSON.stringify(json));
    }
}

// Real-time script API
botnana.motion.evaluate = function(script) {
    var json = {
        jsonrpc: "2.0",
        method: "motion.evaluate",
        params: {
            script: script
        }
    };
    botnana.sender.send(JSON.stringify(json));
};

botnana._.get_slaves = function() {
    var json = {
        jsonrpc: "2.0",
        method: "_.get_slaves"
    };
    botnana.sender.send(JSON.stringify(json));
}

botnana._.get_slave = function(i) {
    var json = {
        jsonrpc: "2.0",
        method: "_.get_slave",
        params: {
            position: i
        }
    };
    botnana.sender.send(JSON.stringify(json));
}

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
    save: function() {
        var json = {
            jsonrpc: "2.0",
            method: "config.save"
        };
        botnana.sender.send(JSON.stringify(json));
    },
    set_slave: function(args) {
        var json = {
            jsonrpc: "2.0",
            method: "config.set_slave",
            params: args
        };
        botnana.sender.send(JSON.stringify(json));
    }
}

// Start API
botnana.start = function(ip) {
    var WebSocket = require('ws');
    var ws = new WebSocket(ip);
    ws.on('message', function(data, flags) {
        console.log(data);
        botnana.handle_response(data);
    });
    ws.on('open', function () {
        botnana._.get_slaves();
    });
    botnana.sender = ws;
    botnana.on("slaves", function(slaves) {
        let s = slaves.split(",");
        let slave_count = s.length/2;
        console.log("slave count: " + slave_count);
        for (var i = 0; i < slave_count; i = i + 1) {
            botnana._.get_slave(i+1);
        }
        let ready_handler = botnana.handlers["ready"];
        if (ready_handler) {
            ready_handler();
        };
    });
}

module.exports = botnana;
