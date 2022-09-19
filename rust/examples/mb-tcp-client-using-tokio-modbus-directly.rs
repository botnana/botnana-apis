use log::info;
use std::{net::SocketAddr, time::Duration};

use tokio_modbus::prelude::*;

pub fn main() -> Result<(), Box<dyn std::error::Error>> {
    let mut builder = env_logger::Builder::from_default_env();
    builder.format_timestamp_millis().init();

    let socket_addr = "192.168.7.2:502".parse().unwrap();

    let rt = tokio::runtime::Builder::new_current_thread()
        .enable_time()
        .enable_io()
        .build()
        .expect("Runtime");
    rt.block_on(async {
        tokio::select! {
            _ = client_context(socket_addr) => info!("Exiting"),
        }
    });

    Ok(())
}

async fn client_context(socket_addr: SocketAddr) {
    tokio::join!(async {
        // Give the server some time for starting up
        tokio::time::sleep(Duration::from_secs(1)).await;

        info!("Connecting client...");
        let mut ctx = tcp::connect(socket_addr).await.unwrap();
        info!("Client connected.");
        info!("Reading input registers...");
        let response = ctx.read_input_registers(30001, 7).await.unwrap();
        info!("The result is '{:?}'", response);
        tokio::time::sleep(Duration::from_millis(15)).await;
        info!("Read write registers...");
        let response = ctx
            .read_write_multiple_registers(30001, 8, 40001, &[5, 6, 7, 8])
            .await
            .unwrap();
        info!("The result is '{:?}'", response);
    },);
}
