"use strict";

var botnana = {
    sender: null,
    debug_level: 0,
    _: {}
};

// Event API
botnana.handlers = {}

botnana.on = function(tag, handler) {
    botnana.handlers[tag] = handler;
};

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
botnana.motion = {};

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

/// Hidden API
botnana._.get_slaves = function() {
    var json = {
        jsonrpc: "2.0",
        method: "_.get_slaves"
    };
    botnana.sender.send(JSON.stringify(json));
}

// Slave API
class Slave {
    constructor(i) {
        this.position = i;
    }

    set(tag, value) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.set",
            params: {
                position: this.position,
                tag: tag,
                value: value
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    get() {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.get",
            params: {
                position: this.position 
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    get_diff() {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.get_diff",
            params: {
                position: this.position 
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    reset_fault() {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.reset_fault",
            params: {
                position: this.position 
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    set_dout(channel, value) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.set_dout",
            params: {
                position: this.position,
                channel: channel,
                value: value
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    disable_aout(channel) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.disable_aout",
            params: {
                position: this.position,
                channel: channel
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    enable_aout(channel) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.enable_aout",
            params: {
                position: this.position,
                channel: channel
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    set_aout(channel, value) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.set_aout",
            params: {
                position: this.position,
                channel: channel,
                value: value
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    disable_ain(channel) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.disable_ain",
            params: {
                position: this.position,
                channel: channel
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }

    enable_ain(channel) {
        var json = {
            jsonrpc: "2.0",
            method: "ethercat.slave.enable_ain",
            params: {
                position: this.position,
                channel: channel
            }
        };
        botnana.sender.send(JSON.stringify(json));
    }
}

botnana.ethercat = {};
botnana.ethercat.slaves = [];
botnana.ethercat.slave = function(n) {
    if (1 <= n && n <= botnana.ethercat.slaves.length) {
        return botnana.ethercat.slaves[n-1];
    } else {
        console.log("Invalid slave index " + n + ".")
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
        if (data) {
            if (botnana.debug_level > 0) {
                console.log(data);
            }
            botnana.handle_response(data);
        }
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
            botnana.ethercat.slaves[i] = new Slave(i+1);
        }
        let ready_handler = botnana.handlers["ready"];
        if (ready_handler) {
            ready_handler();
        };
    });
}

module.exports = botnana;
