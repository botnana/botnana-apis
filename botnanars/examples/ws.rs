extern crate botnanars;
use botnanars::{start, poll};

const CONNECTION: &'static str = "ws://127.0.0.1:2794";

fn main() {
    let mut sender = start(CONNECTION);
    poll(&mut sender);
    println!("Hello");
}
