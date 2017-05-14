"use strict";

var botnana = {
  sender: null,
  debug_level: 0,
  _: {}
};

// Events API
botnana.do_nothing = function() {};
botnana.do_nothing.nothing = true;
botnana.handlers = {};
botnana.handler_counters = {};

botnana.times = function(event, handler, count) {
  let handlers = botnana.handlers;
  let handler_counters = botnana.handler_counters;
  if (!handlers[event]) {
    handlers[event] = [handler];
    handler_counters[event] = [count];
  } else {
    let found = handlers[event].length;
    for (let h in handlers[event]) {
      if (handlers[event][h] === botnana.do_nothing) {
        found = h;
        handlers[event][h] = handler;
        handler_counters[event][h] = count;
        break;
      }
    }
    if (found === handlers[event].length) {
      handlers[event].push(handler);
      handler_counters[event].push(count);
    }
  }
};

botnana.on = function(event, handler) {
  botnana.times(event, handler, 0);
};

botnana.once = function(event, handler) {
  botnana.times(event, handler, 1);
};

botnana.handle_response = function(resp) {
  let lines = resp.split("\n");
  for (let j = 0; j < lines.length; j = j + 1) {
    if (botnana.debug_level > 0 && lines[j].length) {
      console.log(lines[j]);
    }
    let r = lines[j].split("|");
    for (let i = 0; i < r.length; i = i + 2) {
      let event = r[i];
      let handlers = botnana.handlers[event];
      let handler_counters = botnana.handler_counters[event];
      if (handlers) {
        let data = r[i + 1];
        for (let h in handlers) {
          handlers[h](data);
          if (handler_counters[h] >= 1) {
            --handler_counters[h];
            if (handler_counters[h] === 0) {
              handlers[h] = botnana.do_nothing;
            }
          }
        }
      }
    }
  }
};

botnana.poll = function() {
  var json = {
    jsonrpc: "2.0",
    method: "motion.poll"
  };
  botnana.sender.send(JSON.stringify(json));
};

// Version API
botnana.version = {
  get: function() {
    var json = {
      jsonrpc: "2.0",
      method: "version.get"
    };
    botnana.sender.send(JSON.stringify(json));
  }
};

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
};

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

  disable_aout(channel) {
    this.program.lines.push(channel + " " + this.position + " -ec-aout");
    this.state = WAITING_REQUEST;
  }

  disable_ain(channel) {
    this.program.lines.push(channel + " " + this.position + " -ec-ain");
    this.state = WAITING_REQUEST;
  }

  enable_aout(channel) {
    this.program.lines.push(channel + " " + this.position + " +ec-aout");
    this.state = WAITING_REQUEST;
  }

  enable_ain(channel) {
    this.program.lines.push(channel + " " + this.position + " +ec-ain");
    this.state = WAITING_REQUEST;
  }
  set_aout(channel, value) {
    this.program.lines.push(
      value + " " + channel + " " + this.position + " ec-aout!"
    );
    this.state = WAITING_NONE;
  }

  set_dout(channel, t) {
    this.program.lines.push(
      t + " " + channel + " " + this.position + " ec-dout!"
    );
    this.state = WAITING_NONE;
  }

  aout(channel) {
    this.program.line.push(channel + " " + this.position + " ec-aout@");
    this.state = WAITING_NONE;
    return this;
  }

  dout(channel) {
    this.program.lines.push(channel + " " + this.position + " ec-dout@");
    this.state = WAITING_NONE;
    return this;
  }

  ain(channel) {
    this.program.lines.push(channel + " " + this.position + " ec-ain@");
    this.state = WAITING_NONE;
    return this;
  }

  din(channel) {
    this.program.lines.push(channel + " " + this.position + " ec-din@");
    this.state = WAITING_NONE;
    return this;
  }

  IF() {
    this.program.lines.push(" if");
    this.state = WAITING_NONE;
  }

  ELSE() {
    this.program.lines.push(" else");
    this.state = WAITING_NONE;
  }

  THEN() {
    this.program.lines.push(" then");
    this.state = WAITING_NONE;
  }

  BEGIN() {
    this.program.lines.push(" begin");
    this.state = WAITING_NONE;
  }

  WHILE() {
    this.program.lines.push(" while");
    this.state = WAITING_NONE;
  }

  REPEAT() {
    this.program.lines.push(" pause repeat");
    this.state = WAITING_NONE;
  }

  /*UNTIL() {
    this.program.lines.push(" until");
    this.state = WAITING_NONE;
  }

  DO() {
    this.program.lines.push(" do");
    this.state = WAITING_NONE;
  }

  LOOP() {
    this.program.lines.push(" loop");
    this.state = WAITING_NONE;
  }

  AGAIN() {
    this.program.lines.push(" again");
    this.state = WAITING_NONE;
  }*/

  GT(value) {
    this.program.lines.push(value + " >");
    this.state = WAITING_NONE;
  }

  GE(value) {
    this.program.lines.push(value + " 2dup > >R = R> or");
    this.state = WAITING_NONE;
  }

  LT(value) {
    this.program.lines.push(value + " <");
    this.state = WAITING_NONE;
  }

  LE(value) {
    this.program.lines.push(value + " 2dup < >R = R> or");
    this.state = WAITING_NONE;
  }

  EQ(value) {
    this.program.lines.push(value + " =");
    this.state = WAITING_NONE;
  }

  NE(value) {
    this.program.lines.push(value + " = not");
    this.state = WAITING_NONE;
  }

  ms(value) {
    this.program.lines.push(value + " ms");
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
    };
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
    };
    var json = {
      jsonrpc: "2.0",
      method: "script.deploy",
      params: params
    };
    if (botnana.debug_level > 0) {
      console.log("Generated program for " + this.name + ":");
      console.log(params.script);
    }
    botnana.sender.send(JSON.stringify(json));
  }

  run() {
    botnana.motion.evaluate("user$" + this.name);
  }
}

botnana.Program = Program;

/// Hidden API
botnana._.get_slaves = function() {
  var json = {
    jsonrpc: "2.0",
    method: "_.get_slaves"
  };
  botnana.sender.send(JSON.stringify(json));
};

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
    return botnana.ethercat._slaves[n - 1];
  } else {
    console.log("Invalid slave index " + n + ".");
  }
};

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
};

// Start API
botnana.start = function(ip, period) {
  period = period | 100;
  var WebSocket = require("ws");
  var ws = new WebSocket(ip);
  ws.on("message", function(data, flags) {
    if (data) {
      // if (botnana.debug_level > 0) {
      //     console.log(data);
      // }
      botnana.handle_response(data);
    }
  });
  ws.on("open", function() {
    botnana._.get_slaves();
  });
  botnana.sender = ws;
  botnana.once("slaves", function(slaves) {
    let s = slaves.split(",");
    let slave_count = s.length / 2;
    //        console.log("slave count: " + slave_count);
    botnana.ethercat.slave_count = slave_count;
    for (var i = 0; i < slave_count; i = i + 1) {
      botnana.ethercat._slaves[i] = new Slave(i + 1);
    }
    botnana.handle_response("ready|ok");
  });
  setInterval(botnana.poll, period);
};

module.exports = botnana;
