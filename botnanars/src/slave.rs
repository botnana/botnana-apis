use websocket::{ClientBuilder, OwnedMessage};
use futures::sync::mpsc;

#[derive(Clone, Debug)]
pub struct Slave{
    //    sender: mpsc::Sender<OwnedMessage>,
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
    pub fn new(p:usize) -> Slave {
        Slave {
            position: p
        }
    }

    fn set(&self, tag: i32, value: i32){    
        let mut msg = String::new();
        msg.push_str("{\"jsonrpc\":\"2.0\",
                        \"metohd:\"ethercat.slave.set\",
                        \"params:\"{\"position\"");        
        msg.push_str(&self.position.to_string());
        println!("{}",msg);

        OwnedMessage::Text("".to_string());       
    }

    fn get(&self){

    }

    fn get_diff(&self){

    }

   fn reset_fault(&self){

    }

    fn set_dout(&self){

    }
}