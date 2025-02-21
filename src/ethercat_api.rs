use botnana::Botnana;

impl Botnana {
    /// Request EtherCAT link status
    pub fn request_ec_link_status(&mut self) {
        self.evaluate(".ec-links");
    }

    /// Request EtherCAT Slave Info.
    /// @alias    : slave alias
    /// @position : slave position
    pub fn request_ec_slave_info(&mut self, alias: u16, position: u16) {
        self.evaluate(&format!("{} .slave", slave_position!(alias, position)));
    }

    /// Request EtherCAT Slave Info. (只回傳與上次要求不同的狀態)
    /// @alias    : slave alias
    /// @position : slave position
    pub fn request_ec_slave_info_diff(&mut self, alias: u16, position: u16) {
        self.evaluate(&format!("{} .slave_diff", slave_position!(alias, position)));
    }
}
