#[warn(unused_parens)]
#[warn(unused_imports)]

use std::result;
use std::thread;
use std::time;
use std::sync::{Arc, Mutex};
use std::collections::HashMap;

use tokio_core::reactor::Core;
use futures::future::Future;
use futures::sink::Sink;
use futures::stream::Stream;
use futures::sync::mpsc;
use websocket::result::WebSocketError;
use websocket::{ClientBuilder, OwnedMessage};

use ethercat::{Ethercat};
use programmed::Program;

#[derive(Debug)]
pub enum BotNanaError {}

pub type Result<T> = result::Result<T, BotNanaError>;


// Real-time scripwting API
/*
struct Motion {
    sender: mpsc::Sender<OwnedMessage>,
}

impl Motion {}

struct Config {}

impl Config {}
*/

#[derive(Clone)]
#[warn(non_snake_case)]
pub struct botnana {
    sender: Option<mpsc::Sender<OwnedMessage>>,
    debug_level: i32,
    pub ethercat: Ethercat,
    handlers: Arc<Mutex<HashMap<&'static str, Vec<Box<Fn(&str) + Send>>>>>,
    handlers_counters: Arc<Mutex<HashMap<&'static str, Vec<i32>>>>,    
}
/**
 * TODO 
 * 
 * botnana.programs = []
 * 
 */
impl botnana {
    pub fn new() -> Result<botnana> {
        Ok(botnana {
            sender: None,
            debug_level: 1,
            ethercat: Ethercat::new(),
            handlers: Arc::new(Mutex::new(HashMap::new())),
            handlers_counters: Arc::new(Mutex::new(HashMap::new())),
        })
    }

    fn ok(&self) {
        let mut mutself = self.clone();
        thread::spawn(move || {
            mutself.handle_message("ready|ok".to_string());
        });
    }

    fn set_ethercat_slave(&self, i: usize) {
        self.ethercat.set_slave(i, self.sender.clone().unwrap());
    }

    pub fn start(&mut self, connection: &str) {
        println!("Connecting to {}", connection);

        let (sender, receiver) = mpsc::channel(0);
        let se = sender.clone();
        self.sender = Some(sender);

        let mut botnana = self.clone();
        let mut btn = self.clone();

        botnana.once("slaves", move |slaves| {
            let s: Vec<&str> = slaves.split(",").collect();
            let length = s.len() / 2;

            for i in 0..length {
                btn.set_ethercat_slave(i + 1);
            }

            btn.ok();
        });

        let connection = connection.to_owned();

        thread::spawn(move || {
            let mut core = Core::new().unwrap();
            let runner = ClientBuilder::new(&connection)
                .unwrap()
                .add_protocol("rust-websocket")
                .async_connect_insecure(&core.handle())
                .and_then(|(duplex, _)| {
                    let (sink, stream) = duplex.split();
                    stream
                        .filter_map(|message| match message {
                            OwnedMessage::Close(e) => Some(OwnedMessage::Close(e)),
                            OwnedMessage::Ping(d) => Some(OwnedMessage::Pong(d)),
                            OwnedMessage::Text(m) => {
                                botnana.handle_message(m);
                                None
                            }
                            _ => None,
                        })
                        .select(receiver.map_err(|_| WebSocketError::NoDataAvailable))
                        .forward(sink)
                });

            core.run(runner).unwrap();
        });

        self.poll();
        self.slaves();
    }

    fn handle_message(&mut self, message: String) {        
        if message != "" {
            let lines: Vec<&str> = message.split("\n").collect();
            let mut handlers = self.handlers.try_lock().unwrap();
            let mut handlers_counters = self.handlers_counters.try_lock().unwrap();

            for line in lines {
                if self.debug_level > 0 {
                    println!("{:?}", line);
                }

                let mut r: Vec<&str> = line.split("|").collect();
                let mut index = 0;
                let mut event = "";
                for e in r {
                    if index % 2 == 0 {
                        event = e;
                    } else {
                        let mut removeList = Vec::new();

                        match handlers.get(event) {
                            Some(handle) => {
                                let counter = handlers_counters.get_mut(event).unwrap();

                                let mut idx = 0;
                                for h in handle {
                                    h(e);
                                    if counter[idx] == 1 {
                                        removeList.push(idx);
                                    }
                                    counter[idx] -= 1;
                                    idx += 1;
                                }
                            }
                            None => {}
                        };

                        let counter = handlers_counters.get_mut(event).unwrap();
                        let handler = handlers.get_mut(event).unwrap();
                        for i in &removeList {
                            handler.remove(*i);
                            counter.remove(*i);
                        }
                    }

                    index += 1;
                }
            }
        }
    }

    pub fn times<F>(&mut self, event: &'static str, count: i32, handler: F)
    where
        F: Fn(&str) + Send + 'static,
    {
        let mut handlers = self.handlers.lock().unwrap();
        let mut handlers_counters = self.handlers_counters.lock().unwrap();

        let mut h = handlers.entry(event).or_insert(Vec::new());
        let mut hc = handlers_counters.entry(event).or_insert(Vec::new());

        h.push(Box::new(handler));
        hc.push(count);
    }

    fn send(&self, msg: &str, expect: &str) {
        let s = &self.sender;
        match s {
            &Some(ref sender) => {
                let mut sender = sender.clone().wait();
                let msg = OwnedMessage::Text(msg.to_string());
                sender.send(msg).expect(expect);
            }
            &None => {
                println!("No sender can find");
            }
        }
    }

    pub fn on<F>(&mut self, event: &'static str, handler: F)
    where
        F: Fn(&str) + Send + 'static,
    {
        self.times(event, 0, handler);
    }

    pub fn once<F>(&mut self, event: &'static str, handler: F)
    where
        F: Fn(&str) + Send + 'static,
    {
        self.times(event, 1, handler);
    }

    fn poll(&self) {
        let timer = 100;
        let ten_millis = time::Duration::from_millis(timer);
        let btn = self.clone();
        thread::spawn(move || loop {
            btn.send("{\"jsonrpc\":\"2.0\",\"method\":\"motion.poll\"}", "");
            thread::sleep(ten_millis);
        });
    }

    pub fn evaluate(&self, script: &str) {
        let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"motion.evaluate\",\"params\":{\"script\":\""
            .to_owned() + script + "\"}}";

        self.send(&msg, "Sending message");
    }

    pub fn empty(&self) {
        self.evaluate("empty");
    }

    pub fn slaves(&self) {
        let message = "list-slaves";
        self.evaluate(message);
    }

    pub fn version(&self) {
        let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"version.get\"}";
        self.send(msg, "");
    }

    pub fn program(&self, name: &str) -> Program {
        let slave_len = self.ethercat.get_slaves_count();
        let sender = self.sender.clone().unwrap();

        // let program = Program::new(name);        

        let program = Program::new(name, slave_len, sender);

        /*
              push program to programms: Vec<Program>                   
        */

        program
    }

    pub fn get_self(&self) -> &Self  {
        self
    }

    pub fn get_mut_self(&mut self) -> &mut Self {
        self
    }

    // pub fn set_slave(&self, args: &str) {
    //     let mut sender = self.sender.clone().wait();

    //     let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"config.set_slave\",\"params\":{\"script\":\""
    //         .to_owned() + args + "\"}}";
    //     let msg = OwnedMessage::Text(msg.to_string());

    //     sender
    //         .send(msg)
    //         .expect("Sending message across stdin channel");
    // }
}
