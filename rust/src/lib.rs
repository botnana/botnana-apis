extern crate libc;
extern crate serde_json;
extern crate url;
extern crate ws;

/// alias, EtherCAT Slave 的站號別名。
/// position, EtherCAT Slave 的站號，最靠近主站的為 1, 依序遞增排列。
/// 使用規則，當 alias > 0，就以 alias 選定從站，
/// 當 alias = 0, 就以 position 選定從站。  
macro_rules! slave_position {
    ( $alias:expr, $position:expr) => {{
        if $alias > 0 {
            format!("{} ec-a>n", $alias)
        } else {
            format!("{}", $position)
        }
    }};
}

pub mod botnana;
pub mod data_pool;
pub mod drive_api;
pub mod ethercat_api;
pub mod json_api;
pub mod modbus;
pub mod program;

pub use botnana::Botnana;
pub use program::Program;
