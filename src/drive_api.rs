use crate::botnana::Botnana;

impl Botnana {
    /// Set drive operation mode
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @mode	  : operation mode (PP, PV, HM, TQ, CSP, CSV, CST)
    fn set_drive_mode(&mut self, alias: u16, position: u16, channel: u16, mode: u8) {
        self.send_script_to_buffer(&format!(
            "{} {} {} op-mode!",
            mode,
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Set drive operation mode to PP (位置模式)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_pp(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 1);
    }

    /// Set drive operation mode to PV (速度模式, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_pv(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 3);
    }

    /// Set drive operation mode to HM (回歸原點, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_hm(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 6);
    }

    /// Set drive operation mode to TQ (扭力模式, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_tq(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 4);
    }

    /// Set drive operation mode to CSP (時間同步位置模式, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_csp(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 8);
    }

    /// Set drive operation mode to CSV (時間同步速度模式, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_csv(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 9);
    }

    /// Set drive operation mode to CST (時間同步扭力模式, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn set_drive_mode_to_cst(&mut self, alias: u16, position: u16, channel: u16) {
        self.set_drive_mode(alias, position, channel, 10);
    }

    /// Reset drive fault (清除驅動器異警)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn reset_drive_fault(&mut self, alias: u16, position: u16, channel: u16) {
        self.send_script_to_buffer(&format!(
            "{} {} reset-fault",
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Drive On
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn drive_on(&mut self, alias: u16, position: u16, channel: u16) {
        self.send_script_to_buffer(&format!(
            "{} {} drive-on",
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Drive Off
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn drive_off(&mut self, alias: u16, position: u16, channel: u16) {
        self.send_script_to_buffer(&format!(
            "{} {} drive-off",
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Drive Stop
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn drive_stop(&mut self, alias: u16, position: u16, channel: u16) {
        self.send_script_to_buffer(&format!(
            "{} {} drive-stop",
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Drive halt
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @halt     : halt or not halt
    pub fn drive_halt(&mut self, alias: u16, position: u16, channel: u16, halt: bool) {
        let cmd = if halt { "+drive-halt" } else { "-drive-halt" };
        self.send_script_to_buffer(&format!(
            "{} {} {}",
            channel,
            slave_position!(alias, position),
            cmd
        ));
    }

    /// Set drive profile vecloity (For PP Mode, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @velocity : velocity （([pulse/s]，但會因驅動器而異)
    pub fn set_drive_profile_velocity(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        velocity: u32,
    ) {
        self.send_script_to_buffer(&format!(
            "{} {} {} profile-v!",
            velocity,
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Set drive profile acceleration (For PP, PV Mode, SDO)
    /// @alias        : slave alias
    /// @position     : slave position
    /// @channel      : channel
    /// @acceleration : acceleration （([pulse/s^2]，但會因驅動器而異)
    pub fn set_drive_profile_acceleration(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        acceleration: u32,
    ) {
        self.send_script_to_buffer(&format!(
            "{} {} {} profile-a1!",
            acceleration,
            channel,
            slave_position!(alias, position)
        ));
    }

    /// Set drive profile deceleration (For PP, PV Mode, SDO)
    /// @alias        : slave alias
    /// @position     : slave position
    /// @channel      : channel
    /// @deceleration : deceleration ([pulse/s^2]，但會因驅動器而異)
    pub fn set_drive_profile_deceleration(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        deceleration: u32,
    ) {
        self.send_script_to_buffer(&format!(
            "{} {} {} profile-a2!",
            deceleration,
            channel,
            slave_position!(alias, position)
        ));
    }

    /// PP 模式下進行運動
    /// @alias    : slave alias
    /// @position : slave position  
    /// @channel  : channel
    /// @relative : 相對位置或是絕對位置，有些驅動器不支援相對位置
    /// @target   : 目標位置 [pulse]
    pub fn drive_move_to_target_position(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        relative: bool,
        target: u32,
    ) {
        let rel_cmd = if relative { "+pp-rel" } else { "-pp-rel" };
        self.send_script_to_buffer(&format!(
            "{target} {channel} {slave} target-p! {channel} {slave} {rel_cmd} {channel} {slave} go",
            slave = slave_position!(alias, position),
            channel = channel,
            rel_cmd = rel_cmd,
            target = target,
        ));
    }

    /// Set homing profile (SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @method   : 回歸原點的方法 （支援的方法會因驅動器而異）
    /// @speed1   : 搜尋 switch (正負極限或是原點開開) 的速度 ([pulse/s]，但會因驅動器而異)
    /// @speed2   : 搜尋 index pulse 的速度 ([pulse/s]，但會因驅動器而異)
    /// @acceleration  : 加速度 ([pulse/s^2]，但會因驅動器而異)
    pub fn set_drive_homing_profile(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        method: i8,
        speed1: u32,
        speed2: u32,
        acceleration: u32,
    ) {
        self.send_script_to_buffer(&format!(
            "{method} {channel} {slave} homing-method! {speed1} {channel} {slave} homing-v1! {speed2} {channel} {slave} homing-v2! {acceleration} {channel} {slave} homing-a!",
            slave = slave_position!(alias, position),
            channel = channel,
            method = method,
            speed1 = speed1,
            speed2 = speed2,
            acceleration = acceleration,
        ));
    }

    /// HM 模式開始進行回歸原點
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    pub fn drive_homing_start(&mut self, alias: u16, position: u16, channel: u16) {
        self.send_script_to_buffer(&format!(
            "{} {} go",
            channel,
            slave_position!(alias, position),
        ));
    }

    /// Set Target Velocity (在 PV 模式下會開始運動，SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @velocity : velocity ([pulse/s]，但會因驅動器而異)
    pub fn set_drive_target_velocity(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        velocity: i32,
    ) {
        self.send_script_to_buffer(&format!(
            "{} {} {} target-v!",
            velocity,
            channel,
            slave_position!(alias, position),
        ));
    }

    /// Set Torque Slope (For TQ Mode, SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @slope : slope ([0.1%/s])
    pub fn set_drive_torque_slope(&mut self, alias: u16, position: u16, channel: u16, slope: u32) {
        self.send_script_to_buffer(&format!(
            "{} {} {} tq-slope!",
            slope,
            channel,
            slave_position!(alias, position),
        ));
    }

    /// Set Target Torque (在 TQ 模式下會開始運動，SDO)
    /// @alias    : slave alias
    /// @position : slave position
    /// @channel  : channel
    /// @torque : velocity ([0.1%])
    pub fn set_drive_target_torque(
        &mut self,
        alias: u16,
        position: u16,
        channel: u16,
        torque: i16,
    ) {
        self.send_script_to_buffer(&format!(
            "{} {} {} target-tq!",
            torque,
            channel,
            slave_position!(alias, position),
        ));
    }
}
