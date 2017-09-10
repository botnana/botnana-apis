extern crate botnanars;
use botnanars::start;

const CONNECTION: &'static str = "ws://127.0.0.1:2794";

fn main() {
    start(CONNECTION);
    println!("Hello");
}
