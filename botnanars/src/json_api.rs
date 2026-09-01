use botnana::Botnana;

impl Botnana {
    /// motion.poll
    pub fn motion_poll(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"motion.poll"}"#;
        self.send_message(msg);
    }

    /// profiler.restart
    pub fn profiler_restart(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"profiler.restart"}"#;
        self.send_message(msg);
    }

    /// profiler.output
    pub fn profiler_output(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"profiler.output"}"#;
        self.send_message(msg);
    }

    /// version.get
    pub fn version_get(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"version.get"}"#;
        self.send_message(msg);
    }

    /// Reads one slave configuration.
    pub fn config_slave_get(&mut self, alias: u32, position: u32, channel: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.slave.get","params":{"alias":"#.to_owned()
            + alias.to_string().as_str()
            + r#","position":"#
            + position.to_string().as_str()
            + r#","channel":"#
            + channel.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// Reads the motion configuration.
    pub fn config_motion_get(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.motion.get"}"#;
        self.send_message(msg);
    }

    /// Reads one group configuration.
    pub fn config_group_get(&mut self, position: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.get","params":{"position":"#
            .to_owned()
            + position.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// Reads one axis configuration.
    pub fn config_axis_get(&mut self, position: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.get","params":{"position":"#.to_owned()
            + position.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// Powers off the system.
    pub fn poweroff(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"system.poweroff"}"#;
        self.send_message(msg);
    }

    /// Reboots the system.
    pub fn reboot(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"system.reboot"}"#;
        self.send_message(msg);
    }
}
