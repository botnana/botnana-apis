extern crate botnanars;
use botnanars::Botnana;
use std::time::Duration;

fn main() {
    let rt = tokio::runtime::Runtime::new().expect("Tokio runtime");
    let mut botnana = Botnana::new();
    botnana.mb_connect();
    rt.block_on(async {
        let mut interval = tokio::time::interval(Duration::from_millis(100));
        loop {
            interval.tick().await;
            let mut inputs = botnana.mb_table().inputs.lock().expect("Mb Inputs");
            let buf = inputs.read();
            println!("inputs[0..7] = {:?}", buf.get(0..7));
        }
    })
}
