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

pub fn start(connection: &str) -> mpsc::Sender<OwnedMessage> {
    println!("Connecting to {}", connection);

    let (usr_msg, stdin_ch) = mpsc::channel(0);
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
                    .filter_map(|message| {
                        println!("Received Message: {:?}", message);
                        match message {
                            OwnedMessage::Close(e) => Some(OwnedMessage::Close(e)),
                            OwnedMessage::Ping(d) => Some(OwnedMessage::Pong(d)),
                            _ => None,
                        }
                    })
                    .select(stdin_ch.map_err(|_| WebSocketError::NoDataAvailable))
                    .forward(sink)
            });
        core.run(runner).unwrap();
    });
    usr_msg
}

pub fn poll(sender: &mut mpsc::Sender<websocket::OwnedMessage>) {
    let mut input = String::new();
    let mut stdin_sink = sender.wait();
    loop {
        input.clear();
        stdin().read_line(&mut input).unwrap();
        let trimmed = input.trim();

        let (close, msg) = match trimmed {
            "/close" => (true, OwnedMessage::Close(None)),
            "/ping" => (false, OwnedMessage::Ping(b"PING".to_vec())),
            _ => (false, OwnedMessage::Text(trimmed.to_string())),
        };

        stdin_sink
            .send(msg)
            .expect("Sending message across stdin channel.");

        if close {
            break;
        }
    }
}

#[cfg(test)]
mod tests {
    #[test]
    fn it_works() {
        assert_eq!(2 + 2, 4);
    }
}
