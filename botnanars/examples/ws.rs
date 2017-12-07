extern crate botnanars;
use botnanars::{start, poll, slaves};


const CONNECTION: &'static str = "ws://127.0.0.1:3012";

fn main() {    
    let mut sender = start(CONNECTION);
    
    poll(&mut sender); 
    println!("list slaves");
    slaves(&mut sender);
     
}
