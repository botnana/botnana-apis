extern crate libc;
use crate::botnana::Botnana;
use std::str;

impl Botnana {
    /// motion.pool
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

    /// config.slave.set
    fn config_slave_set(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        param: &str,
        value: i32,
    ) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.slave.set","params":{"alias":"#.to_owned()
            + alias.to_string().as_str()
            + r#","position":"#
            + position.to_string().as_str()
            + r#","channel":"#
            + channel.to_string().as_str()
            + r#",""#
            + param
            + r#"":"#
            + value.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// config.slave.set (homing_method)
    pub fn config_slave_set_homing_method(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "homing_method", value);
    }

    /// config.slave.set (homing_speed_1)
    pub fn config_slave_set_homing_speed_1(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "homing_speed_1", value);
    }

    /// config.slave.set (homing_speed_2)
    pub fn config_slave_set_homing_speed_2(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "homing_speed_2", value);
    }

    /// config.slave.set (homing_acceleration)
    pub fn config_slave_set_homing_acceleration(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "homing_acceleration", value);
    }

    /// config.slave.set (profile_velocity)
    pub fn config_slave_set_profile_velocity(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "profile_velocity", value);
    }

    /// config.slave.set (profile_acceleration)
    pub fn config_slave_set_profile_acceleration(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "profile_acceleration", value);
    }

    /// config.slave.set (profile_deceleration)
    pub fn config_slave_set_profile_deceleration(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "profile_deceleration", value);
    }

    /// config.slave.set (pdo_velocity_offset)
    pub fn config_slave_set_pdo_velocity_offset(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_velocity_offset", value);
    }

    /// config.slave.set (pdo_torque_offset)
    pub fn config_slave_set_pdo_torque_offset(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_torque_offset", value);
    }

    /// config.slave.set (pdo_digital_inputs)
    pub fn config_slave_set_pdo_digital_inputs(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_digital_inputs", value);
    }

    /// config.slave.set (pdo_demand_position)
    pub fn config_slave_set_pdo_demand_position(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_demand_position", value);
    }

    /// config.slave.set (pdo_demand_velocity)
    pub fn config_slave_set_pdo_demand_velocity(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_demand_velocity", value);
    }

    /// config.slave.set (pdo_real_velocity)
    pub fn config_slave_set_pdo_real_velocity(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_real_velocity", value);
    }

    /// config.slave.set (pdo_real_torque)
    pub fn config_slave_set_pdo_real_torque(
        &mut self,
        alias: u32,
        position: u32,
        channel: u32,
        value: i32,
    ) {
        self.config_slave_set(alias, position, channel, "pdo_real_torque", value);
    }

    /// config.slave.get
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

    /// config.motion.set
    fn config_motion_set(&mut self, param: &str, value: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.motion.set","params":{""#.to_owned()
            + param
            + r#"":"#
            + value.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// config.motion.set (period_us)
    pub fn config_motion_set_period_us(&mut self, value: u32) {
        self.config_motion_set("period_us", value);
    }

    /// config.motion.set (group_capacity)
    pub fn config_motion_set_group_capacity(&mut self, value: u32) {
        self.config_motion_set("group_capacity", value);
    }

    /// config.motion.set (axis_capacity)
    pub fn config_motion_set_axis_capacity(&mut self, value: u32) {
        self.config_motion_set("axis_capacity", value);
    }

    /// config.motion.get
    pub fn config_motion_get(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.motion.get"}"#;
        self.send_message(msg);
    }

    /// config.group.set for string data type
    fn config_group_set_string(&mut self, position: u32, param: &str, value: &str) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#",""#
            + param
            + r#"":""#
            + value
            + r#""}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (name)
    pub fn config_group_set_name(&mut self, position: u32, name: &str) {
        self.config_group_set_string(position, "name", name);
    }

    /// config.group.set (gtype as 1D)
    pub fn config_group_set_type_as_1d(&mut self, position: u32, a1: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#","gtype":"1D", "mapping":["#
            + a1.to_string().as_str()
            + r#"]}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (gtype as 2D)
    pub fn config_group_set_type_as_2d(&mut self, position: u32, a1: u32, a2: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#","gtype":"2D", "mapping":["#
            + a1.to_string().as_str()
            + r#","#
            + a2.to_string().as_str()
            + r#"]}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (gtype as 3D)
    pub fn config_group_set_type_as_3d(&mut self, position: u32, a1: u32, a2: u32, a3: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#","gtype":"3D", "mapping":["#
            + a1.to_string().as_str()
            + r#","#
            + a2.to_string().as_str()
            + r#","#
            + a3.to_string().as_str()
            + r#"]}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (gtype as SINE)
    pub fn config_group_set_type_as_sine(&mut self, position: u32, a1: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#","gtype":"SINE", "mapping":["#
            + a1.to_string().as_str()
            + r#"]}}"#;
        self.send_message(&msg);
    }

    /// config.group.set for double data type
    fn config_group_set_double(&mut self, position: u32, param: &str, value: f64) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#",""#
            + param
            + r#"":"#
            + value.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (vmax)
    pub fn config_group_set_vmax(&mut self, position: u32, vmax: f64) {
        self.config_group_set_double(position, "vmax", vmax);
    }

    /// config.group.set (amax)
    pub fn config_group_set_amax(&mut self, position: u32, amax: f64) {
        self.config_group_set_double(position, "amax", amax);
    }

    /// config.group.set (jmax)
    pub fn config_group_set_jmax(&mut self, position: u32, jmax: f64) {
        self.config_group_set_double(position, "jmax", jmax);
    }

    /// config.group.get
    pub fn config_group_get(&mut self, position: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.group.get","params":{"position":"#
            .to_owned()
            + position.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// config.axis.set for string data type
    fn config_axis_set_string(&mut self, position: u32, param: &str, value: &str) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#",""#
            + param
            + r#"":""#
            + value
            + r#""}}"#;
        self.send_message(&msg.to_owned());
    }

    /// config.axis.set (name)
    pub fn config_axis_set_name(&mut self, position: u32, name: &str) {
        self.config_axis_set_string(position, "name", name);
    }

    /// config.axis.set (encoder_length_unit)
    pub extern "C" fn config_axis_set_encoder_length_unit_as_meter(&mut self, position: u32) {
        self.config_axis_set_string(position, "encoder_length_unit", "Meter");
    }

    /// config.axis.set (encoder_length_unit)
    pub extern "C" fn config_axis_set_encoder_length_unit_as_revolution(&mut self, position: u32) {
        self.config_axis_set_string(position, "encoder_length_unit", "Revolution");
    }

    /// config.axis.set (encoder_length_unit)
    pub extern "C" fn config_axis_set_encoder_length_unit_as_pulse(&mut self, position: u32) {
        self.config_axis_set_string(position, "encoder_length_unit", "Pulse");
    }

    /// config.axis.set for double data type
    fn config_axis_set_double(&mut self, position: u32, param: &str, value: f64) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#",""#
            + param
            + r#"":"#
            + value.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (home_offset)
    pub fn config_axis_set_home_offset(&mut self, position: u32, offset: f64) {
        self.config_axis_set_double(position, "home_offset", offset);
    }

    /// config.group.set (encoder_ppu)
    pub fn config_axis_set_encoder_ppu(&mut self, position: u32, value: f64) {
        self.config_axis_set_double(position, "encoder_ppu", value);
    }

    /// config.group.set (ext_encoder_ppu)
    pub fn config_axis_set_ext_encoder_ppu(&mut self, position: u32, value: f64) {
        self.config_axis_set_double(position, "ext_encoder_ppu", value);
    }

    /// config.group.set (closed_loop_filter)
    pub fn config_axis_set_closed_loop_filter(&mut self, position: u32, value: f64) {
        self.config_axis_set_double(position, "closed_loop_filter", value);
    }

    /// config.group.set (max_position_deviation)
    pub fn config_axis_set_max_position_deviation(&mut self, position: u32, value: f64) {
        self.config_axis_set_double(position, "max_position_deviation", value);
    }

    /// config.group.set (amax)
    pub fn config_axis_set_amax(&mut self, position: u32, value: f64) {
        self.config_axis_set_double(position, "amax", value);
    }

    /// config.group.set (vmax)
    pub fn config_axis_set_vmax(&mut self, position: u32, value: f64) {
        self.config_axis_set_double(position, "vmax", value);
    }

    /// config.axis.set for integer data type
    fn config_axis_set_integer(&mut self, position: u32, param: &str, value: i32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.set","params":{"#.to_owned()
            + r#""position":"#
            + position.to_string().as_str()
            + r#",""#
            + param
            + r#"":"#
            + value.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// config.group.set (encoder_direction)
    pub fn config_axis_set_encoder_direction(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "encoder_direction", value);
    }

    /// config.group.set (ext_encoder_direction)
    pub fn config_axis_set_ext_encoder_direction(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "ext_encoder_direction", value);
    }

    /// config.group.set (drive_alias)
    pub fn config_axis_set_drive_alias(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "drive_alias", value);
    }

    /// config.group.set (drive_slave_position)
    pub fn config_axis_set_drive_slave_position(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "drive_slave_position", value);
    }

    /// config.group.set (drive_channel)
    pub fn config_axis_set_drive_channel(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "drive_channel", value);
    }

    /// config.group.set (ext_encoder_alias)
    pub fn config_axis_set_ext_encoder_alias(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "ext_encoder_alias", value);
    }

    /// config.group.set (ext_encoder_slave_position)
    pub fn config_axis_set_ext_encoder_slave_position(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "ext_encoder_slave_position", value);
    }

    /// config.gr#[no_mangle]
    pub fn config_axis_set_ext_encoder_channel(&mut self, position: u32, value: i32) {
        self.config_axis_set_integer(position, "ext_encoder_channel", value);
    }

    /// config.axis.get
    pub fn config_axis_get(&mut self, position: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.axis.get","params":{"position":"#.to_owned()
            + position.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// save config
    pub fn config_save(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"config.save"}"#;
        self.send_message(msg);
    }

    /// System poweroff
    pub fn poweroff(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"system.poweroff"}"#;
        self.send_message(msg);
    }

    /// System reboot
    pub fn reboot(&mut self) {
        let msg = r#"{"jsonrpc":"2.0","method":"system.reboot"}"#;
        self.send_message(msg);
    }

    /// Subscribe EtherCAT slave
    pub fn subscribe_ec_slave(&mut self, alias: u32, position: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"ec_slave.subscribe","params":{"alias":"#.to_owned()
            + alias.to_string().as_str()
            + r#","position":"#
            + position.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }

    /// Unsubscribe EtherCAT slave
    pub fn unsubscribe_ec_slave(&mut self, alias: u32, position: u32) {
        let msg = r#"{"jsonrpc":"2.0","method":"ec_slave.unsubscribe","params":{"alias":"#
            .to_owned()
            + alias.to_string().as_str()
            + r#","position":"#
            + position.to_string().as_str()
            + r#"}}"#;
        self.send_message(&msg);
    }
}
