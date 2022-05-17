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
        let mut interval = tokio::time::interval(Duration::from_millis(100));
        loop {
            interval.tick().await;
            if botnana.is_mb_connected() {
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
            }
        }
    })
}
