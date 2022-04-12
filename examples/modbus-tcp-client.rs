extern crate botnanars;
use botnanars::Botnana;
use log::info;
use std::time::Duration;

fn main() {
    let rt = tokio::runtime::Runtime::new().expect("Tokio runtime");
    let mut botnana = Botnana::new();
    botnana.mb_connect();
    rt.block_on(async {
        let mut interval = tokio::time::interval(Duration::from_millis(100));
        loop {
            interval.tick().await;
            {
                let mut inputs = botnana.mb_table().inputs.lock().expect("Mb Inputs");
                let buf = inputs.read();
                info!("inputs[0..7] = {:?}", buf.get(0..7));
            }
            info!("bit[10001]={:?}, bit[12000]={:?}, i16[30001]={:?}, i16[30384]={:?}, u16[30001]={:?}, u16[30384]={:?}, i32[30001]={:?}, i32[30384]={:?}, i32[30383]={:?}, u32[30001]={:?}, u32[30384]={:?}, u32[30383]={:?}",
                botnana.mb_bit(10001),
                botnana.mb_bit(12000),
                botnana.mb_i16(30001),
                botnana.mb_i16(30384),
                botnana.mb_u16(30001),
                botnana.mb_u16(30384),
                botnana.mb_i32(30001),
                botnana.mb_i32(30384),
                botnana.mb_i32(30383),
                botnana.mb_u32(30001),
                botnana.mb_u32(30384),
                botnana.mb_u32(30383),
            );
        }
    })
}
