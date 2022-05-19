extern crate botnanars;
use botnanars::modbus::MB_BLOCK_SIZE;
use botnanars::Botnana;
use log::info;
use std::time::Duration;

fn main() {
    let rt = tokio::runtime::Builder::new_current_thread()
        .enable_time()
        .build()
        .expect("Tokio runtime");
    let mut botnana = Botnana::new();
    botnana.mb_connect();
    rt.block_on(async {
        let mut interval = tokio::time::interval(Duration::from_millis(3000));
        let mut counter = 0;
        info!("Connecting...");
        while !botnana.is_mb_connected() {
            interval.tick().await;
        }
        info!("Connected...");
        loop {
            botnana.mb_table().update();
            info!("bit[10001]={:?}, bit[10000+N*16]={:?}, bit[10000+N*16+1]={:?}, i16[30001]={:?}, i16[30000+N]={:?}, i16[30000+N+1]={:?}, u16[30001]={:?}, u16[30000+N]={:?}, u16[30000+N+1]={:?}, i32[30001]={:?}, i32[30000+N-1]={:?}, i32[30000+N]={:?}, u32[30001]={:?}, u32[30000+N-1]={:?}, u32[30000+N]={:?}",
                botnana.mb_bit(10001),
                botnana.mb_bit(10000+MB_BLOCK_SIZE*16),
                botnana.mb_bit(10000+MB_BLOCK_SIZE*16+1),
                botnana.mb_i16(30001),
                botnana.mb_i16(30000+MB_BLOCK_SIZE),
                botnana.mb_i16(30000+MB_BLOCK_SIZE+1),
                botnana.mb_u16(30001),
                botnana.mb_u16(30000+MB_BLOCK_SIZE),
                botnana.mb_u16(30000+MB_BLOCK_SIZE+1),
                botnana.mb_i32(30001),
                botnana.mb_i32(30000+MB_BLOCK_SIZE-1),
                botnana.mb_i32(30000+MB_BLOCK_SIZE),
                botnana.mb_u32(30001),
                botnana.mb_u32(30000+MB_BLOCK_SIZE-1),
                botnana.mb_u32(30000+MB_BLOCK_SIZE),
            );
            let _ = botnana.mb_set_bit(00001, true);
            let _ = botnana.mb_set_u16(40002, counter);
            counter += 1;
            let _ = botnana.mb_set_u16(40003, 0xffff);
            let _ = botnana.mb_set_i32(40004, -3);
            let _ = botnana.mb_set_u32(40006, 0x5a5a8a8a);
            botnana.mb_table().publish();
            interval.tick().await;
        }
    })
}
