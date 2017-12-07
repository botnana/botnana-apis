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

use slave::Slave;

#[derive(Debug)]
pub enum BotNanaError {}

pub type Result<T> = result::Result<T, BotNanaError>;

// Real-time scripwting API
struct Motion {
    sender: mpsc::Sender<OwnedMessage>,
}

impl Motion {}

struct Config {}

impl Config {}


// #[derive(Clone, Debug)]
#[derive(Clone)]
#[warn(non_snake_case)]
pub struct botnana {
    // sender: mpsc::Sender<OwnedMessage>,
    sender: Option<mpsc::Sender<OwnedMessage>>,
    debug_level: i32,
    slave: Arc<Mutex<Vec<Slave>>>,
    handlers: Arc<Mutex<HashMap<&'static str, Vec<Box<Fn(&str) + Send>>>>>,
    handlers_counters: Arc<Mutex<HashMap<&'static str, Vec<i32>>>>,
}

impl botnana {
    /*
    pub fn new(connection: &str) -> Result<botnana> {
        println!("Connecting to {}", connection);
        let (sender, receiver) = mpsc::channel(0);
        let connection = connection.to_owned();

        let mut btn = botnana {
            sender: sender,        
            debug_level: 1,
            slave: Arc::new(Mutex::new(Vec::new())),
            handlers: Arc::new(Mutex::new(HashMap::new())),
            handlers_counters: Arc::new(Mutex::new(HashMap::new())),
        };        

        let mut tb = btn.clone();

        thread::spawn(move || {
            let mut core = Core::new().unwrap();
            let runner = ClientBuilder::new(&connection)
                .unwrap()
                .add_protocol("rust-websocket")
                .async_connect_insecure(&core.handle())
                .and_then(|(duplex, _)| {                   
                    let (sink, stream) = duplex.split();
                    stream
                        .filter_map(|message| {                            
                            match message {
                                OwnedMessage::Close(e) => Some(OwnedMessage::Close(e)),
                                OwnedMessage::Ping(d) => Some(OwnedMessage::Pong(d)),
                                OwnedMessage::Text(m) => {
                                    tb.handle_message(m);
                                    None
                                }
                                _ => None,
                            }
                        })
                        .select(receiver.map_err(|_| WebSocketError::NoDataAvailable))
                        .forward(sink)
                });

            core.run(runner).unwrap();
        });

        btn.poll();
        Ok(btn)
    }*/
    pub fn new(connection: &str) -> Result<botnana> {
        Ok(botnana {
            sender: None,
            debug_level: 1,
            slave: Arc::new(Mutex::new(Vec::new())),
            handlers: Arc::new(Mutex::new(HashMap::new())),
            handlers_counters: Arc::new(Mutex::new(HashMap::new())),
        })
    }

    fn setSlave(&self, p: usize) {
        let mut mutself = self.clone();
        let mut slave = mutself.slave.lock().unwrap();
        slave.push(Slave::new(p));
    }
    
    fn ok(&self) {
        let mut mutself = self.clone();
        mutself.handle_message("ready|ok".to_string());
    }

    pub fn start(&mut self, connection: &str) {
        println!("Connecting to {}", connection);

        let (sender, receiver) = mpsc::channel(0);
        self.sender = Some(sender);

        let mut botnana = self.clone();
        let t = self.clone();
        let slave = self.slave.lock().unwrap();

        botnana.once("slaves", move |slaves| {
            let s: Vec<&str> = slaves.split(",").collect();
            let length = s.len() / 2;

            for i in 0..length {
                t.setSlave(i + 1);
            }
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
    }

    fn start_handler(&self, event: &str, msg: &str) {
        let mut botnana = self.clone();  
    }

    fn handle_message(&mut self, message: String) {
        println!("receiver :: {:?}", message);
        let lines: Vec<&str> = message.split("\n").collect();
        let mut handlers = self.handlers.try_lock().unwrap();
        let mut handlers_counters = self.handlers_counters.try_lock().unwrap();

        for line in lines {
            if self.debug_level > 0 {
                println!("{:?}", line);
            }

            let mut r: Vec<&str> = line.split("|").collect();
            let mut index = 0;
            let mut removeList = Vec::new();
            let mut event = "";
            for e in r {
                if index % 2 == 0 {
                    event = e;
                } else {
                    match handlers.get(event) {
                        Some(handle) => {
                            let counter = handlers_counters.get_mut(event).unwrap();
                            let mut idx = 0;
                            for h in handle {
                                h(e);
                                if counter[idx] == 1 {
                                    removeList.push(idx);
                                }
                                idx += 1;
                            }
                        }
                        None => {}
                    };

                    if (removeList.len() > 0) {
                        let handlers = handlers.get_mut(event).unwrap();
                        let counter = handlers_counters.get_mut(event).unwrap();
                        for i in removeList.clone() {
                            handlers.remove(i);
                            counter.remove(i);
                        }
                    }
                }

                index += 1;
            }
        }
    }

    fn times<F>(&mut self, event: &'static str, count: i32, handler: F)
    where
        F: Fn(&str) + Send + 'static,
    {
        // fn times(&mut sel   f, event: &str, count:i8, handler:Box<Fn(&str) + Send>){
        let mut handlers = self.handlers.lock().unwrap();
        let mut handlers_counters = self.handlers_counters.lock().unwrap();

        let mut h = handlers.entry(event).or_insert(Vec::new());
        let mut hc = handlers_counters.entry(event).or_insert(Vec::new());

        h.push(Box::new(handler));
        hc.push(count);

        // let a = handlers.get_mut(event).unwrap();


        // a.push(Box::new(|s|{println!("hello")}));

        // h[0]("hello how are you");
        // println!("{:?}",f[0]);
        // let handler_counters = self.handler_counters.lock().unwrap();
        // let mut getFlag = false;

        // match handlers.get(event) {
        //     Some(h) => true,
        //     None => false,
        // };

        // if getFlag {

        // }
        // else {

        // }

        // handler("hello times");
    }

    fn send(&self, msg: &str, expect: &str) {
        let s = self.sender.clone();
        match s {
            Some(sender) => {
                let mut sender = sender.clone().wait();
                let msg = OwnedMessage::Text(msg.to_string());
                sender.send(msg).expect(expect);
            }
            None => {
                println!("Error Happen");
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
        // let mut sender = self.sender.clone().wait();
        let timer = 100;
        let ten_millis = time::Duration::from_millis(timer);
        let bt = self.clone();
        thread::spawn(move || {
            loop {
                // let msg = OwnedMessage::Text("".to_string());
                // sender.send(msg).expect("send Polling");

                thread::sleep(ten_millis);
            }
        });
    }

    fn evaluate(&self, script: &str) {
        let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"motion.evaluate\",\"params\":{\"script\":\""
            .to_owned() + script + "\"}}";

        self.send(&msg, "Sending message");
        // let msg = OwnedMessage::Text(msg.to_string());

        // let mut sender = self.sender.clone().wait();

        // sender
        //     .send(msg)
        //     .expect("Sending message across stdin channel.");
    }

    /* pub fn version(&self) {
        let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"version.get\"}";
        // let mut sender = self.sender.clone().wait();
        
        let msg = OwnedMessage::Text(msg.to_string());

        sender.send(msg).expect("Sending get version message");
    }

    pub fn empty(&self) {
        self.evaluate("empty");
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
*/
    pub fn slaves(&self) {
        let message = "list-slaves";
        self.evaluate(message);
    }
}
