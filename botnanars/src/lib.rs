extern crate futures;
extern crate tokio_core;
extern crate websocket;

use std::thread;
use std::io::stdin;
use tokio_core::reactor::Core;
use futures::future::Future;
use futures::sink::Sink;
use futures::stream::Stream;
use futures::sync::mpsc;
use websocket::result::WebSocketError;
use websocket::{ClientBuilder, OwnedMessage};

/// Starts a communication thread and returns a sender.
// pub fn start(connection: &str) -> mpsc::Sender<OwnedMessage> {
//     println!("Connecting to {}", connection);

//     let (sender, receiver) = mpsc::channel(0);
//     let connection = connection.to_owned();
//     thread::spawn(move || {
//         let mut core = Core::new().unwrap();
//         let runner = ClientBuilder::new(&connection)
//             .unwrap()
//             .add_protocol("rust-websocket")
//             .async_connect_insecure(&core.handle())
//             .and_then(|(duplex, _)| {
//                 let (sink, stream) = duplex.split();
//                 stream
//                     .filter_map(|message| {
//                         println!("Received Message: {:?}", message);
//                         match message {
//                             OwnedMessage::Close(e) => Some(OwnedMessage::Close(e)),
//                             OwnedMessage::Ping(d) => Some(OwnedMessage::Pong(d)),
//                             _ => None,
//                         }
//                     })
//                     .select(receiver.map_err(|_| WebSocketError::NoDataAvailable))
//                     .forward(sink)
//             });
//         core.run(runner).unwrap();
//     });

//     sender
// }

// pub fn poll(sender: &mut mpsc::Sender<websocket::OwnedMessage>) {
//     let mut input = String::new();
//     let mut sender = sender.wait();
//     loop {
//         input.clear();
//         stdin().read_line(&mut input).unwrap();
//         let trimmed = input.trim();

//         let (close, msg) = match trimmed {
//             "/close" => (true, OwnedMessage::Close(None)),
//             "/ping" => (false, OwnedMessage::Ping(b"PING".to_vec())),
//             _ => (false, OwnedMessage::Text(trimmed.to_string())),
//         };

//         sender
//             .send(msg)
//             .expect("Sending message across stdin channel.");

//         if close {
//             break;
//         }
//     }
// }

// fn evaluate(script: &str, sender: &mut mpsc::Sender<websocket::OwnedMessage>) {
//     let msg = "{\"jsonrpc\":\"2.0\",\"method\":\"motion.evaluate\",\"params\":{\"script\":\""
//         .to_owned() + script + "\"}}";
//     let msg = websocket::OwnedMessage::Text(msg.to_string());

//     let mut sender = sender.wait();

//     sender
//         .send(msg)
//         .expect("Sending message across stdin channel.");
// }

// pub fn slaves(sender: &mut mpsc::Sender<websocket::OwnedMessage>) {
//     let message = "list-slaves";
//     evaluate(message, sender);
// }

// pub mod tests {
//     #[test]
//     pub fn it_works() {
//         assert_eq!(2 + 2, 4);
//     }
//     fn test() {
//         println!("test");
//     }
// }


pub mod botnana;
pub mod programmed;
pub mod ethercat;