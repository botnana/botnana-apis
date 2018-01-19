use websocket::{ClientBuilder, OwnedMessage};
use futures::sync::mpsc;
use std::sync::{Arc, Mutex};
use futures::sink::Sink;

#[derive(Clone, Debug)]
pub struct Slave {
    sender: mpsc::Sender<OwnedMessage>,
    position: usize,
    //    vendor_id: i32,
    //    product_code: u64,
    //    old_pds_state: u64
}

impl Slave {
    // pub fn new(p: i32,i: i32, c: u64, o:u64, sender:mpsc::Sender<OwnedMessage>) -> Slave{
    //     Slave{
    //         sender: sender,
    //         position: p,
    //         vendor_id: i,
    //         product_code: c,
    //         old_pds_state: o
    //     }
    // }

    // pub fn new(p: usize, sender:mpsc::Sender<OwnedMessage>) -> Slave{
    //     Slave {
    //         sender: sender,
    //         position: p,
    //     }
    // }

    pub fn new(p: usize, sender: mpsc::Sender<OwnedMessage>) -> Slave {
        Slave {
            position: p,
            sender: sender,
        }
    }

    fn get_json(
        &self,
        method: &str,
        tag: Option<&str>,
        channel: Option<usize>,
        value: Option<usize>,
    ) -> OwnedMessage {
        let mut msg = String::new();
        let p = &self.position.to_string();
        msg.push_str("{");
        msg.push_str("\"jsonrpc\":\"2.0\",");
        msg.push_str("\"method:\":\"ethercat.slave.");
        msg.push_str(method);
        msg.push_str("\",");
        msg.push_str("\"params\":{\"position\":");
        msg.push_str(p);
        match tag {
            Some(v) => {
                msg.push_str(",\"tag\":\"");
                msg.push_str(&v.to_string());
                msg.push_str("\"");
            }
            None => {}
        }

        match channel {
            Some(v) => {
                msg.push_str(",\"channel\":");
                msg.push_str(&v.to_string());
            }
            None => {}
        }

        match value {
            Some(v) => {
                msg.push_str(",\"value\":");
                msg.push_str(&v.to_string());
            }
            None => {}
        }

        msg.push_str("}}");

        
        OwnedMessage::Text(msg)
    }

    fn send_tag_json(&self, method: &str, tag: &str, value: usize) {
        let message = self.get_json(method, Some(tag), None, Some(value));
        self.sender.clone().wait().send(message).expect("");
    }

    fn send_json(&self, method: &str) {
        let message = self.get_json(method, None, None, None);
        self.sender.clone().wait().send(message).expect("");
    }

    fn send_channel_json(&self, method: &str, channel: usize) {
        let message = self.get_json(method, None, Some(channel), None);
        self.sender.clone().wait().send(message).expect("");
    }

    fn send_channel_value_json(&self, method: &str, channel: usize, value: usize) {
        let message = self.get_json(method, None, Some(channel), Some(value));
    }

    pub fn set(&self, tag: &str, value: usize) {
        self.send_tag_json("set", tag, value);
    }

    pub fn get(&self) {
        self.send_json("get");
    }

    pub fn get_diff(&self) {
        self.send_json("get_diff");
    }

    pub fn reset_fault(&self) {
        self.send_json("reset_fault");
    }

    pub fn set_dout(&self, channel: usize, value: usize) {
        self.send_channel_value_json("set_dout", channel, value);
    }

    pub fn disable_aout(&self, channel: usize) {
        self.send_channel_json("disable_aout", channel);
    }

    pub fn enable_aout(&self, channel: usize) {
        self.send_channel_json("enable_aout", channel);
    }

    pub fn set_aout(&self, channel: usize) {
        self.send_channel_json("set_aout", channel);
    }

    pub fn disable_ain(&self, channel: usize) {
        self.send_channel_json("disable_ain", channel);
    }

    pub fn enable_ain(&self, channel: usize) {
        self.send_channel_json("enable_ain", channel);
    }
}

#[derive(Clone)]
pub struct Ethercat {
    _slaves: Arc<Mutex<Vec<Slave>>>,
    slaves_count: Arc<Mutex<usize>>,
}

impl Ethercat {
    pub fn new() -> Ethercat {
        Ethercat {
            _slaves: Arc::new(Mutex::new(Vec::new())),
            slaves_count: Arc::new(Mutex::new(0)),
        }
    }

    pub fn slave(&self, p: usize) -> Option<Slave> {
        let slave = self._slaves.lock().unwrap();
        if 1 <= p && p <= slave.len() {
            Some(slave[p - 1].clone())
        } else {
            None
        }
    }

    pub fn set_slave(&self, p: usize, sender: mpsc::Sender<OwnedMessage>) {
        let mut slave = self._slaves.lock().unwrap();
        slave.push(Slave::new(p, sender));
        *self.slaves_count.lock().unwrap() += 1;
    }

    pub fn get_slaves_count(&self) -> usize {
        *self.slaves_count.lock().unwrap()
    }
}
