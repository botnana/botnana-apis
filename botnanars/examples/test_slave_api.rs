extern crate botnanars;
use botnanars::botnana;
use std::{thread, time};

fn main() {         
    let mut botnana =  botnana::botnana::new("ws://localhost:3012").unwrap();         
    botnana.start("ws://localhost:3012");
    // let mut botnana = botnana::start().unwrap();  // get botnana
    //botnana.connect(wsAddress);
    botnana.slaves();  
    // botnana.on("a",|msg|{
    //     println!("{}",msg);        
    // });
    loop{                
        thread::sleep(time::Duration::from_millis(2000));        
    }
}
