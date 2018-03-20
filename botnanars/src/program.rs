use websocket::OwnedMessage;
use std::sync::{Arc, Mutex, mpsc};

const WAITING_NONE: i8 = 0;
const WAITING_REQUESTS: i8 = 1;
const WAITING_TARGET_REACHED: i8 = 2;

#[derive(Debug, Clone)]
// #[derive(Debug)]
pub struct ProgrammedEtherCATSlave {
    program: Program,
    position: usize,
    pub state: i8,
}

impl ProgrammedEtherCATSlave {
    pub fn new(program: Program, position: usize) -> ProgrammedEtherCATSlave {
        ProgrammedEtherCATSlave {
            program: program,
            position: position,
            state: WAITING_NONE,
        }
    }

    pub fn hm(&mut self) {
        if self.state == WAITING_TARGET_REACHED {
            self.until_target_reached();
        }
        let position = self.position.to_string();
        self.program.lines.lock().unwrap().push_str("hm ");
        self.program.lines.lock().unwrap().push_str(&position);
        self.program.lines.lock().unwrap().push_str(" op-mode!\\n");
        self.state = WAITING_REQUESTS;
    }

    pub fn pp(&mut self) {
        if self.state == WAITING_TARGET_REACHED {
            self.until_target_reached();
        }
        let position = self.position.to_string();
        self.program.lines.lock().unwrap().push_str("pp ");
        self.program.lines.lock().unwrap().push_str(&position);
        self.program.lines.lock().unwrap().push_str(" op-mode!\\n");
        self.state = WAITING_REQUESTS;
    }

    pub fn move_to(&mut self, target: usize) {
        let position = self.position.to_string();
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&target.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program.lines.lock().unwrap().push_str(&position);
        self.program.lines.lock().unwrap().push_str(" jog\\n");
    }

    pub fn go(&mut self) {
        if self.state == WAITING_REQUESTS {
            self.until_no_requests();
        }

        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" go\\n");
        self.state = WAITING_TARGET_REACHED;
    }
    pub fn until_target_reached(&mut self) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(" until-target-reached\\n");
        self.state = WAITING_NONE;
    }
    pub fn until_no_requests(&mut self) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str("until-no-requests\\n");
        self.state = WAITING_NONE;
    }
    pub fn disable_aout(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" -ec-aout\\n");
        self.state = WAITING_REQUESTS;
    }
    pub fn disable_ain(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" -ec-ain\\n");
        self.state = WAITING_REQUESTS;
    }
    pub fn enable_aout(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" +ec-aout\\n");
        self.state = WAITING_REQUESTS;
    }
    pub fn enable_ain(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" +ec-ain\\n");
        self.state = WAITING_REQUESTS;
    }
    pub fn set_aout(&mut self, channel: usize, value: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&value.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" ec-aout\\n");
        self.state = WAITING_NONE;
    }
    pub fn set_dout(&mut self, channel: usize, value: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&value.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" ec-aout\\n");
        self.state = WAITING_NONE;
    }
    pub fn aout(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" ec-aout@\\n");
        self.state = WAITING_NONE;
        //TODO return
    }
    pub fn dout(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" ec-dout@\\n");
        self.state = WAITING_NONE;
        //TODO return
    }
    pub fn ain(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" ec-ain@\\n");
        self.state = WAITING_NONE;
        //TODO return
    }
    pub fn din(&mut self, channel: usize) {
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&channel.to_string());
        self.program.lines.lock().unwrap().push_str(" ");
        self.program
            .lines
            .lock()
            .unwrap()
            .push_str(&self.position.to_string());
        self.program.lines.lock().unwrap().push_str(" ec-din@\\n");
        self.state = WAITING_NONE;
        //TODO return
    }
    // pub fn IF(){}
    // pub fn ELSE(){}
    // pub fn THEN(){}
    // pub fn BEGIN(){}
    // pub fn WHILE(){}
    // pub fn REPEAT(){}
    // pub fn GT(){}
    // pub fn GE(){}
    // pub fn LT(){}
    // pub fn LE(){}
    // pub fn EQ(){}
    // pub fn NE(){}
}

#[derive(Debug, Clone)]
pub struct ProgrammedEtherCAT {
    _slaves: Arc<Mutex<Vec<ProgrammedEtherCATSlave>>>,
    slaves_count: usize,
}

impl ProgrammedEtherCAT {
    pub fn new() -> ProgrammedEtherCAT {
        ProgrammedEtherCAT {
            _slaves: Arc::new(Mutex::new(Vec::new())),
            slaves_count: 0,
        }
    }

    pub fn slave<'a>(&self, p: usize) -> Option<ProgrammedEtherCATSlave> {
        let slave = self._slaves.lock().unwrap();
        if 1 <= p && p <= slave.len() {
            Some(slave[p - 1].clone()) // ProgramEtherCATSlave clone
        } else {
            None
        }
    }

    pub fn set_slave(&mut self, slave: ProgrammedEtherCATSlave) {
        let mut slaves = self._slaves.lock().unwrap();
        slaves.push(slave);
        self.slaves_count += 1;
    }

    pub fn get_slave_count(&self) -> usize {
        self.slaves_count
    }
}

#[derive(Debug, Clone)]
pub struct Program {
    name: String,
    line: String,
    lines: Arc<Mutex<String>>,
    sender: mpsc::Sender<OwnedMessage>,
    pub ethercat: ProgrammedEtherCAT,
}

impl Program {
    pub fn new(name: &str, slave: usize, sender: mpsc::Sender<OwnedMessage>) -> Program {
        let mut line = String::new();
        line.push_str(": user$");
        line.push_str(name);
        line.push_str("\\n");

        let ethercat = ProgrammedEtherCAT::new();
        let mut eth = ethercat.clone();

        let program = Program {
            name: name.to_string(),
            line: line.clone(),
            lines: Arc::new(Mutex::new(line.clone())),
            sender: sender,
            ethercat: ethercat,
        };

        for i in 0..slave {
            let pg = program.clone();
            eth.set_slave(ProgrammedEtherCATSlave::new(pg, i + 1));
        }

        program.clone()
    }

    fn deploy_json(&self) -> String {
        let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"script.deploy\",\"params\":{\"script\":\""
            .to_owned() + &self.lines.lock().unwrap() + "\"}}";
        msg.to_string()
    }

    pub fn deploy(&mut self) {
        let slave_count = self.ethercat.get_slave_count();

        for i in 1..slave_count {
            match self.ethercat.slave(i) {
                Some(mut slave) => {
                    if slave.state == WAITING_REQUESTS {
                        slave.until_no_requests();
                    }
                    if slave.state == WAITING_TARGET_REACHED {
                        slave.until_target_reached();
                    }
                    slave.state = WAITING_NONE;
                }
                None => {}
            }
        }

        self.lines.lock().unwrap().push_str("end-of-program ;");
        let message = self.deploy_json();
        self.sender
            .send(OwnedMessage::Text(message))
            .expect("error");
    }
    pub fn run(&self) {
        let msg = "deploy user$".to_owned() + &self.name + " ;deploy";
        self.evaluate(&msg);
    }
    pub fn ms(&mut self, value: usize) {
        self.lines.lock().unwrap().push_str(&value.to_string());
        self.lines.lock().unwrap().push_str(" ms");
        self.lines.lock().unwrap().push_str("\\n");
    }
    pub fn abort_program(&self) {
        self.evaluate("kill-task0");
    }
    pub fn println(&mut self, str: &str) {
        self.lines.lock().unwrap().push_str(str);
        self.lines.lock().unwrap().push_str("\\n");
    }
    fn evaluate(&self, script: &str) {
        let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"motion.evaluate\",\"params\":{\"script\":\""
            .to_owned() + script + "\"}}";

        self.sender
            .send(OwnedMessage::Text(msg))
            .expect("error");
    }
}
