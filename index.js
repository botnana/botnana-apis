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
    let lines = resp.split("\n");
    for (var j=0; j<lines.length; j=j+1) {
        let r = lines[j].split("|");
        for (var i=0; i< r.length; i=i+2) {
            if(botnana.handlers[r[i]]) {
                botnana.handlers[r[i]](r[i+1]);
            }
        }
    }
};

botnana.poll = function() {
    var json = {
        jsonrpc: "2.0",
        method: "motion.poll",
    };
    botnana.sender.send(JSON.stringify(json));
}

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

// Real-time scripting API
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

botnana.programs = [];

botnana.empty = function() {
    botnana.motion.evaluate("empty");
}

const WAITING_NONE = 0;
const WAITING_REQUESTS = 1;
const WAITING_TARGET_REACHED = 2;

class ProgrammedEtherCATSlave {
    constructor(program, i) {
        this.program = program;
        this.position = i;
        this.state = WAITING_NONE;
    }

    hm() {
        if (this.state == WAITING_TARGET_REACHED) {
            this.until_target_reached();
        }
        this.program.lines.push("hm " + this.position + " op-mode!");
        this.state = WAITING_REQUESTS;
    }

    pp() {
        if (this.state == WAITING_TARGET_REACHED) {
            this.until_target_reached();
        }
        this.program.lines.push("pp " + this.position + " op-mode!");
        this.state = WAITING_REQUESTS;
    }

    move_to(target) {
        this.program.lines.push(target + " " + this.position + " jog");
    }

    go() {
        if (this.state == WAITING_REQUESTS) {
            this.until_no_requests();
        }
        this.program.lines.push(this.position + " go");
        this.state = WAITING_TARGET_REACHED;
    }

    until_target_reached() {
        this.program.lines.push(this.position + " until-target-reached");
        this.state = WAITING_NONE;
    }

    until_no_requests() {
        this.program.lines.push("until-no-requests");
        this.state = WAITING_NONE;
    }

}

class Program {
    constructor(name) {
        var that = this;
        this.name = name;
        botnana.programs.push(this);
        this.lines = [": " + "user$" + name];
        this.ethercat = {};
        this.ethercat._slaves = [];
        for (var i = 1; i <= botnana.ethercat.slave_count; i = i + 1) {
            this.ethercat._slaves[i] = new ProgrammedEtherCATSlave(this, i);
        }
        this.ethercat.slave = function(n) {
            return that.ethercat._slaves[n];
        }
    }

    deploy() {
        for (var i = 1; i <= botnana.ethercat.slave_count; i = i + 1) {
            let slave = this.ethercat._slaves[i];
            if (slave.state === WAITING_REQUESTS) {
                slave.until_no_requests();
            }
            if (slave.state === WAITING_TARGET_REACHED) {
                slave.until_target_reached();
            }
            slave.state = WAITING_NONE;
        }
        this.lines.push("end-of-program ;");
        var params = {
            script: this.lines.join("\n")
        }
        var json = {
            jsonrpc: "2.0",
            method: "script.deploy",
            params: params
        };
        if(botnana.debug_level > 0) {
            console.log("Generated program for " + this.name + ":");
            console.log(params.script);
        }
        botnana.sender.send(JSON.stringify(json));
    }

    run() { botnana.motion.evaluate("user$" + this.name); }

}

botnana.Program = Program;

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
botnana.ethercat._slaves = [];
botnana.ethercat.slave = function(n) {
    if (1 <= n && n <= botnana.ethercat._slaves.length) {
        return botnana.ethercat._slaves[n-1];
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
botnana.start = function(ip, period) {
    period = period | 100;
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
//        console.log("slave count: " + slave_count);
        botnana.ethercat.slave_count = slave_count;
        for (var i = 0; i < slave_count; i = i + 1) {
            botnana.ethercat._slaves[i] = new Slave(i+1);
        }
        let ready_handler = botnana.handlers["ready"];
        if (ready_handler) {
            ready_handler();
        };
    });
    setInterval(function() { botnana.poll(); }, period);
}

module.exports = botnana;
