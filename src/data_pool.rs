use botnana::Botnana;

/// Drive Data
#[derive(Debug)]
pub struct Drive {
    /// target position
    pub target_position: i32,
    /// Drive real position
    pub real_position: i32,
    /// Drive Digital inputs
    pub digital_inputs: u32,
    /// Control word
    pub control_word: u16,
    /// Status word
    pub status_word: u16,
}

impl Drive {
    /// New
    pub fn new() -> Drive {
        Drive {
            target_position: 0,
            real_position: 0,
            digital_inputs: 0,
            control_word: 0,
            status_word: 0,
        }
    }
}

/// Slave Data
#[derive(Debug)]
pub struct Slave {
    /// Vendor ID
    pub vendor_id: u32,
    /// Product Code
    pub product_code: u32,
    /// Slave description
    pub description: String,
    /// Slave Alias
    pub alias: u16,
    /// Slave state
    pub ec_state: u8,
    /// Drive data
    pub drives: Vec<Drive>,
}

impl Slave {
    /// New
    pub fn new() -> Slave {
        Slave {
            vendor_id: 0,
            product_code: 0,
            description: String::new(),
            alias: 0,
            ec_state: 0,
            drives: Vec::new(),
        }
    }

    /// 依據 channel index 配置所以需要的記憶體
    #[inline]
    #[inline(always)]
    pub fn reserve_drives(&mut self, channel_index: usize) {
        for _i in self.drives.len()..channel_index + 1 {
            self.drives.push(Drive::new());
        }
    }
}

/// Data Pool
pub struct DataPool {
    /// EtherCAT 從站數
    pub ec_slaves_len: u32,
    /// EtherCAT 從站狀態
    pub ec_slaves_state: u32,
    /// slaves 資料初始化旗標
    slaves_initing: bool,
    slaves_inited: bool,
    /// Slaves 的資料
    pub slaves: Vec<Slave>,

    enabled: bool,
}

impl DataPool {
    /// New
    pub fn new() -> DataPool {
        DataPool {
            ec_slaves_len: 0,
            ec_slaves_state: 0,
            slaves_initing: false,
            slaves_inited: false,
            slaves: Vec::new(),

            enabled: false,
        }
    }
}

fn ec_slaves_len_process(data_pool: &mut DataPool, _: usize, _: usize, msg: &str) {
    if let Ok(x) = msg.parse::<u32>() {
        data_pool.ec_slaves_len = x;
        if !data_pool.slaves_inited {
            data_pool.slaves_initing = true;
        }
    }
}

fn ec_slaves_state_process(data_pool: &mut DataPool, _: usize, _: usize, msg: &str) {
    if let Ok(x) = msg.parse::<u32>() {
        data_pool.ec_slaves_state = x;
    }
}

fn slave_vendor_id_process(data_pool: &mut DataPool, position: usize, _channel: usize, msg: &str) {
    if let Ok(x) = u32::from_str_radix(msg.trim_start_matches("0x"), 16) {
        data_pool.slaves[position].vendor_id = x;
    }
}

fn slave_product_code_process(
    data_pool: &mut DataPool,
    position: usize,
    _channel: usize,
    msg: &str,
) {
    if let Ok(x) = u32::from_str_radix(msg.trim_start_matches("0x"), 16) {
        data_pool.slaves[position].product_code = x;
    }
}

fn slave_description_process(
    data_pool: &mut DataPool,
    position: usize,
    _channel: usize,
    msg: &str,
) {
    data_pool.slaves[position].description = msg.to_string();
}

fn slave_alias_process(data_pool: &mut DataPool, position: usize, _channel: usize, msg: &str) {
    if let Ok(x) = msg.parse::<u16>() {
        data_pool.slaves[position].alias = x;
    }
}

fn slave_state_process(data_pool: &mut DataPool, position: usize, _channel: usize, msg: &str) {
    if let Ok(x) = u8::from_str_radix(msg.trim_start_matches("0x"), 16) {
        data_pool.slaves[position].ec_state = x;
    }
}

fn drive_target_position_process(
    data_pool: &mut DataPool,
    position: usize,
    channel: usize,
    msg: &str,
) {
    if let Ok(x) = msg.parse::<i32>() {
        data_pool.slaves[position].reserve_drives(channel);
        data_pool.slaves[position].drives[channel].target_position = x;
    }
}

fn drive_real_position_process(
    data_pool: &mut DataPool,
    position: usize,
    channel: usize,
    msg: &str,
) {
    if let Ok(x) = msg.parse::<i32>() {
        data_pool.slaves[position].reserve_drives(channel);
        data_pool.slaves[position].drives[channel].real_position = x;
    }
}

fn drive_control_word_process(
    data_pool: &mut DataPool,
    position: usize,
    channel: usize,
    msg: &str,
) {
    if let Ok(x) = u16::from_str_radix(msg.trim_start_matches("0x"), 16) {
        data_pool.slaves[position].reserve_drives(channel);
        data_pool.slaves[position].drives[channel].control_word = x;
    }
}

pub fn drive_status_word_process(
    data_pool: &mut DataPool,
    position: usize,
    channel: usize,
    msg: &str,
) {
    if let Ok(x) = u16::from_str_radix(msg.trim_start_matches("0x"), 16) {
        data_pool.slaves[position].reserve_drives(channel);
        data_pool.slaves[position].drives[channel].status_word = x;
    }
}

pub fn drive_digital_inputs_process(
    data_pool: &mut DataPool,
    position: usize,
    channel: usize,
    msg: &str,
) {
    if let Ok(x) = u32::from_str_radix(msg.trim_start_matches("0x"), 16) {
        data_pool.slaves[position].reserve_drives(channel);
        data_pool.slaves[position].drives[channel].digital_inputs = x;
    }
}

impl Botnana {
    /// 啟動自動取得資料的功能
    pub fn enable_auto_qurey(&mut self) {
        let enabled = self.data_pool.lock().unwrap().enabled;
        if !enabled {
            self.config_init_queries_and_hadlers();
            self.data_pool.lock().unwrap().enabled = true;
        }
    }

    /// 關閉自動取得資料的功能
    pub fn disable_auto_qurey(&mut self) {
        let mut data_pool = self.data_pool.lock().unwrap();
        if data_pool.enabled {
            self.internal_handlers.lock().unwrap().clear();
            self.init_queries.lock().unwrap().clear();
            self.cyclic_queries.lock().unwrap().clear();

            data_pool.slaves_inited = false;
            data_pool.slaves_initing = false;
            data_pool.slaves.clear();
            data_pool.enabled = false;
        }
    }

    fn config_init_queries_and_hadlers(&mut self) {
        let mut internal_handlers = self.internal_handlers.lock().unwrap();
        internal_handlers.insert(
            "slaves_responding".to_owned(),
            Box::new(ec_slaves_len_process),
        );

        let mut init_queries = self.init_queries.lock().unwrap();
        init_queries.push(".verbose \n".to_string());
        init_queries.push(".motion \n".to_string());
        init_queries.push(".ec-links \n".to_string());
    }

    /// Data Pool Forth
    /// 檢查是否有初始化的需求
    pub(crate) fn data_pool_forth(&mut self) {
        let mut data_pool = self.data_pool.lock().unwrap();
        if data_pool.slaves_initing {
            data_pool.slaves_initing = false;
            data_pool.slaves_inited = true;

            let mut init_queries = self.init_queries.lock().unwrap();
            let mut cyclic_queries = self.cyclic_queries.lock().unwrap();
            let mut internal_handlers = self.internal_handlers.lock().unwrap();

            cyclic_queries.push(".ec-links\n".to_string());
            internal_handlers.insert("al_states".to_owned(), Box::new(ec_slaves_state_process));

            // slaves 多擴充一個，使 index 從 1 開始
            data_pool.slaves.push(Slave::new());

            for i in 1..data_pool.ec_slaves_len + 1 {
                init_queries.push(format!("{} .slave\n", i));
                cyclic_queries.push(format!("{} .slave-diff\n", i));

                data_pool.slaves.push(Slave::new());
                internal_handlers.insert("vendor".to_owned(), Box::new(slave_vendor_id_process));

                internal_handlers
                    .insert("product".to_owned(), Box::new(slave_product_code_process));

                internal_handlers.insert(
                    "description".to_owned(),
                    Box::new(slave_description_process),
                );

                internal_handlers.insert("ec_alias".to_owned(), Box::new(slave_alias_process));

                internal_handlers.insert("slave_state".to_owned(), Box::new(slave_state_process));

                internal_handlers.insert(
                    "target_position".to_owned(),
                    Box::new(drive_target_position_process),
                );
                internal_handlers.insert(
                    "real_position".to_owned(),
                    Box::new(drive_real_position_process),
                );
                internal_handlers.insert(
                    "control_word".to_owned(),
                    Box::new(drive_control_word_process),
                );
                internal_handlers.insert(
                    "status_word".to_owned(),
                    Box::new(drive_status_word_process),
                );
                internal_handlers.insert(
                    "digital_inputs".to_owned(),
                    Box::new(drive_digital_inputs_process),
                );
            }
        }
    }
}
