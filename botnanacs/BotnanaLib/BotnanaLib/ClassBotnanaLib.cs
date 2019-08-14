using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BotnanaLib
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HandleMessage(IntPtr dataPtr, string value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HandleTagNameMessage(IntPtr dataPtr, UInt32 position, UInt32 channel, string value);

    public class Botnana
    {

        public Botnana()
        {
            innerBotnana = botnana_new_dll("192.168.7.2");
            innerProgram = program_new_dll("program");
        }

        public Botnana(string ip)
        {
            innerBotnana = botnana_new_dll(ip);
            innerProgram = program_new_dll("program");
        }

        // Library version
        public String LibraryVersion()
        {
            return library_version_dll();
        }

        // WebSocket Connect
        public void Connect()
        {
            botnana_connect_dll(innerBotnana);
        }

        // WebSocket Disconnect
        public void Disconnect()
        {
            botnana_disconnect_dll(innerBotnana);
        }

        // Set IP
        public string set_ip(string ip)
        {
            return botnana_set_ip_dll(innerBotnana, ip);
        }

        // Set Port
        public UInt16 set_port(UInt16 port)
        {
            return botnana_set_port_dll(innerBotnana, port);
        }

        // URL
        public string url()
        {
            return botnana_url_dll(innerBotnana);
        }

        // Send real time script (立即送出) 
        public void EvaluateScript(string script)
        {
            script_evaluate_dll(innerBotnana, script);
        }

        // Send real time script (送到緩衝區)  
        public void SendScript(string script)
        {
            send_script(innerBotnana, script);
        }

        // Poll 時間到達時，每次要從緩衝區中送出幾個指令
        public void SetScriptsPopCount(UInt32 count)
        {
            set_scripts_pop_count_dll(innerBotnana, count);
        }

        // 設定 Poll 時間間隔
        public void SetPollIntervalMs(UInt64 interval)
        {
            set_poll_interval_ms_dll(innerBotnana, interval);
        }

        // 送出自定義的訊息
        public void SendMessage(string message)
        {
            botnana_send_message_dll(innerBotnana, message);
        }

        // Set callback function of WS on_open event
        public void SetOnOpenCB(IntPtr dataPtr, HandleMessage hm)
        {
            botnana_set_on_open_cb_dll(innerBotnana, dataPtr, hm);
        }

        // Set callback function of WS on_error event
        public void SetOnErrorCB(IntPtr dataPtr, HandleMessage hm)
        {
            botnana_set_on_error_cb_dll(innerBotnana, dataPtr, hm);
        }

        // Set callback function of WS on_message event
        public void SetOnMessageCB(IntPtr dataPtr, HandleMessage hm)
        {
            botnana_set_on_message_cb_dll(innerBotnana, dataPtr, hm);
        }

        // Set callback function of WS on_send event
        public void SetOnSendCB(IntPtr dataPtr, HandleMessage hm)
        {
            botnana_set_on_send_cb_dll(innerBotnana, dataPtr, hm);
        }

        // Set callback function of tag
        public void SetTagCB(string tag, int count, IntPtr dataPtr, HandleMessage hm)
        {
            botnana_set_tag_cb_dll(innerBotnana, tag, count, dataPtr, hm);
        }

        // Set callback function of name of tag
        public void SetTagNameCB(string tag, int count, IntPtr dataPtr, HandleTagNameMessage hm)
        {
            botnana_set_tagname_cb_dll(innerBotnana, tag, count, dataPtr, hm);
        }

        // Depoly program to NC background task
        public void DeployProgram()
        {
            program_deploy_dll(innerBotnana, innerProgram);
        }

        // Add command to program 
        public void AddProgramLine(string script)
        {
            program_line_dll(innerProgram, script);
        }

        // Run Program
        public void RunProgram()
        {
            program_run_dll(innerBotnana, innerProgram);
        }

        // Clear program
        public void ClearProgram()
        {
            program_clear_dll(innerProgram);
        }

        // Abort program
        public void AbortProgram()
        {
            botnana_abort_program_dll(innerBotnana);
        }

        // 載入單一 SFC 檔案 
        public void LoadSFC(string path)
        {
            script_evaluate_dll(innerBotnana, System.IO.File.ReadAllText(path, Encoding.UTF8));
        }

        // 清除 SFC
        public void ClearSFC()
        {
            script_evaluate_dll(innerBotnana, @"0sfc");
        }

        // JSON-API: version.get
        public void VersionGet()
        {
            botnana_version_get(innerBotnana);
        }

        // JSON-API: config.slave.get
        public void ConfigSlaveGet(UInt32 alias, UInt32 position, UInt32 channel)
        {
            configure_slave_get(innerBotnana, alias, position, channel);
        }

        // JSON-API: homing_method of config.slave.set
        public void ConfigSlaveSetHomingMethod(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_homing_method(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: homing_speed_1 of config.slave.set
        public void ConfigSlaveSetHomingSpeed1(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_homing_speed_1(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: homing_speed_2 of config.slave.set
        public void ConfigSlaveSetHomingSpeed2(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_homing_speed_2(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: homing_acceleration of config.slave.set
        public void ConfigSlaveSetHomingAcceleration(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_homing_acceleration(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: profile_velocity of config.slave.set
        public void ConfigSlaveSetProfileVelocity(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_profile_velocity(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: profile_acceleration of config.slave.set
        public void ConfigSlaveSetProfileAcceleration(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_profile_acceleration(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: profile_deceleration of config.slave.set
        public void ConfigSlaveSetProfileDeceleration(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_profile_deceleration(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: pdo_digital_inputs of config.slave.set
        public void ConfigSlaveSetPdoDigitalInputs(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_pdo_digital_inputs(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: pdo_demand_position of config.slave.set
        public void ConfigSlaveSetPdoDemandPosition(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_pdo_demand_position(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: pdo_demand_velocity of config.slave.set
        public void ConfigSlaveSetPdoDemandVelocity(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_pdo_demand_velocity(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: pdo_demand_torque of config.slave.set
        public void ConfigSlaveSetPdoDemandTorque(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_pdo_demand_torque(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: pdo_real_velocity of config.slave.set
        public void ConfigSlaveSetPdoRealVelocity(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_pdo_real_velocity(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: pdo_real_torque of config.slave.set
        public void ConfigSlaveSetPdoRealTorque(UInt32 alias, UInt32 position, UInt32 channel, Int32 value)
        {
            configure_slave_set_pdo_real_torque(innerBotnana, alias, position, channel, value);
        }

        // JSON-API: config.motion.get
        public void ConfigMotionGet()
        {
            configure_motion_get(innerBotnana);
        }

        // JSON-API: period_us of config.motion.set
        public void ConfigMotionSetPeriodUs(UInt32 value)
        {
            configure_motion_set_period_us(innerBotnana, value);
        }

        // JSON-API: group_capacity of config.motion.set
        public void ConfigMotionSetGroupCapacity(UInt32 value)
        {
            configure_motion_set_group_capacity(innerBotnana, value);
        }

        // JSON-API: axis_capacity of config.motion.set
        public void ConfigMotionSetAxisCapacity(UInt32 value)
        {
            configure_motion_set_axis_capacity(innerBotnana, value);
        }

        // JSON-API: config.group.get
        public void ConfigGroupGet(UInt32 position)
        {
            configure_group_get(innerBotnana, position);
        }


        // JSON-API: name of config.group.set
        public Int32 ConfigGroupSetName(UInt32 position, string name)
        {
            return configure_group_set_name(innerBotnana, position, name);
        }

        // JSON-API: type as 1D of config.group.set
        public void ConfigGroupSetTypeAs1D(UInt32 position, UInt32 a1)
        {
            configure_group_set_type_as_1d(innerBotnana, position, a1);
        }

        // JSON-API: type as 2D of config.group.set
        public void ConfigGroupSetTypeAs2D(UInt32 position, UInt32 axis1, UInt32 axis2)
        {
            configure_group_set_type_as_2d(innerBotnana, position, axis1, axis2);
        }

        // JSON-API: type as 3D of config.group.set
        public void ConfigGroupSetTypeAs3D(UInt32 position, UInt32 axis1, UInt32 axis2, UInt32 axis3)
        {
            configure_group_set_type_as_3d(innerBotnana, position, axis1, axis2, axis3);
        }

        // JSON-API: type as SINE of config.group.set
        public void ConfigGroupSetTypeAsSine(UInt32 position, UInt32 a1)
        {
            configure_group_set_type_as_sine(innerBotnana, position, a1);
        }

        // JSON-API: vmax of config.group.set
        public void ConfigGroupSetVmax(UInt32 position, double value)
        {
            configure_group_set_vmax(innerBotnana, position, value);
        }

        // JSON-API: amax of config.group.set
        public void ConfigGroupSetAmax(UInt32 position, double value)
        {
            configure_group_set_amax(innerBotnana, position, value);
        }

        // JSON-API: jmax of config.group.set
        public void ConfigGroupSetJmax(UInt32 position, double value)
        {
            configure_group_set_jmax(innerBotnana, position, value);
        }

        // JSON-API: config.axis.get
        public void ConfigAxisGet(UInt32 position)
        {
            configure_axis_get(innerBotnana, position);
        }

        // JSON-API: name of config.axis.set
        public Int32 ConfigAxisSetName(UInt32 position, string name)
        {
            return configure_axis_set_name(innerBotnana, position, name);
        }

        // JSON-API: home_offset of config.axis.set
        public void ConfigAxisSetHomeOffset(UInt32 position, double value)
        {
            configure_axis_set_home_offset(innerBotnana, position, value);
        }

        // JSON-API: encoder_length_unit as Meter of config.axis.set
        public void ConfigAxisSetEncoderLengthUnitAsMeter(UInt32 position)
        {
            configure_axis_set_encoder_length_unit_as_meter(innerBotnana, position);
        }

        // JSON-API: encoder_length_unit as Revolution of config.axis.set
        public void ConfigAxisSetEncoderLengthUnitAsRevolution(UInt32 position)
        {
            configure_axis_set_encoder_length_unit_as_revolution(innerBotnana, position);
        }

        // JSON-API: encoder_length_unit as pulse of config.axis.set
        public void ConfigAxisSetEncoderLengthUnitAsPulse(UInt32 position)
        {
            configure_axis_set_encoder_length_unit_as_pulse(innerBotnana, position);
        }

        // JSON-API: encoder_ppu of config.axis.set
        public void ConfigAxisSetEncoderPPU(UInt32 position, double value)
        {
            configure_axis_set_encoder_ppu(innerBotnana, position, value);
        }

        // JSON-API: ext_encoder_ppu of config.axis.set
        public void ConfigAxisSetExtEncoderPPU(UInt32 position, double value)
        {
            configure_axis_set_ext_encoder_ppu(innerBotnana, position, value);
        }

        // JSON-API: encoder_direction of config.axis.set
        public void ConfigAxisSetEncoderDirection(UInt32 position, Int32 value)
        {
            configure_axis_set_encoder_direction(innerBotnana, position, value);
        }


        // JSON-API: ext_encoder_direction of config.axis.set
        public void ConfigAxisSetExtEncoderDirection(UInt32 position, Int32 value)
        {
            configure_axis_set_ext_encoder_direction(innerBotnana, position, value);
        }

        // JSON-API: closed_loop_filter of config.axis.set
        public void ConfigAxisSetClosedLoopFilter(UInt32 position, double value)
        {
            configure_axis_set_closed_loop_filter(innerBotnana, position, value);
        }

        // JSON-API: max_position_deviation of config.axis.set
        public void ConfigAxisSetMaxPositionDeviation(UInt32 position, double value)
        {
            configure_axis_set_max_position_deviation(innerBotnana, position, value);
        }

        // JSON-API: vmax of config.axis.set
        public void ConfigAxisSetVmax(UInt32 position, double value)
        {
            configure_axis_set_vmax(innerBotnana, position, value);
        }

        // JSON-API: amax of config.axis.set
        public void ConfigAxisSetAmax(UInt32 position, double value)
        {
            configure_axis_set_amax(innerBotnana, position, value);
        }

        // JSON-API: drive_alias of config.axis.set
        public void ConfigAxisSetDriveAlias(UInt32 position, Int32 value)
        {
            configure_axis_set_drive_alias(innerBotnana, position, value);
        }

        // JSON-API: drive_slave_position of config.axis.set
        public void ConfigAxisSetDriveSlavePosition(UInt32 position, Int32 value)
        {
            configure_axis_set_drive_slave_position(innerBotnana, position, value);
        }

        // JSON-API: drive_channel of config.axis.set
        public void ConfigAxisSetDriveChannel(UInt32 position, Int32 value)
        {
            configure_axis_set_drive_channel(innerBotnana, position, value);
        }

        // JSON-API: ext_encoder_alias of config.axis.set
        public void ConfigAxisSetExtEncoderAlias(UInt32 position, Int32 value)
        {
            configure_axis_set_ext_encoder_alias(innerBotnana, position, value);
        }

        // JSON-API: ext_encoder_slave_position of config.axis.set
        public void ConfigAxisSetExtEncoderSlavePosition(UInt32 position, Int32 value)
        {
            configure_axis_set_ext_encoder_slave_position(innerBotnana, position, value);
        }

        // JSON-API: ext_encoder_channel of config.axis.set
        public void ConfigAxisSetExtEncoderChannel(UInt32 position, Int32 value)
        {
            configure_axis_set_ext_encoder_channel(innerBotnana, position, value);
        }

        // JSON-API: config.save
        public void ConfigSave()
        {
            configure_save(innerBotnana);
        }

        // Reboot
        public void Reboot()
        {
            botnana_reboot(innerBotnana);
        }

        // Power-off
        public void Poweroff()
        {
            botnana_poweroff(innerBotnana);
        }

        private IntPtr innerBotnana;
        private IntPtr innerProgram;

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern string library_version_dll();

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr botnana_new_dll(string ip);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_connect_dll(IntPtr desc);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_disconnect_dll(IntPtr desc);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern string botnana_set_ip_dll(IntPtr desc, string ip);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt16 botnana_set_port_dll(IntPtr desc, UInt16 port);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern string botnana_url_dll(IntPtr desc);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void script_evaluate_dll(IntPtr desc, string script);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void send_script(IntPtr desc, string script);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void set_scripts_pop_count_dll(IntPtr desc, UInt32 count);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void set_poll_interval_ms_dll(IntPtr desc, UInt64 interval);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr program_new_dll(string name);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_send_message_dll(IntPtr desc, string msg);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_tag_cb_dll(IntPtr desc, string tag, int count, IntPtr dataPtr, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_tagname_cb_dll(IntPtr desc, string tagName, int count, IntPtr dataPtr, HandleTagNameMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_open_cb_dll(IntPtr desc, IntPtr dataPt, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_error_cb_dll(IntPtr desc, IntPtr dataPt, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_message_cb_dll(IntPtr desc, IntPtr dataPt, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_send_cb_dll(IntPtr desc, IntPtr dataPtr, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_line_dll(IntPtr pm, string cmd);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_clear_dll(IntPtr pm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_deploy_dll(IntPtr botnana, IntPtr pm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void program_run_dll(IntPtr botnana, IntPtr pm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_abort_program_dll(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_version_get(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_get(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_homing_method(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_homing_speed_1(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_homing_speed_2(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_homing_acceleration(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_profile_velocity(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_profile_acceleration(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_profile_deceleration(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_pdo_digital_inputs(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_pdo_demand_position(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_pdo_demand_velocity(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_pdo_demand_torque(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_pdo_real_velocity(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_slave_set_pdo_real_torque(IntPtr botnana, UInt32 alias, UInt32 position, UInt32 channel, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_motion_get(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_motion_set_period_us(IntPtr botnana, UInt32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_motion_set_group_capacity(IntPtr botnana, UInt32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_motion_set_axis_capacity(IntPtr botnana, UInt32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_get(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 configure_group_set_name(IntPtr botnana, UInt32 position, string value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_type_as_1d(IntPtr botnana, UInt32 position, UInt32 axis1);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_type_as_2d(IntPtr botnana, UInt32 position, UInt32 axis1, UInt32 axis2);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_type_as_3d(IntPtr botnana, UInt32 position, UInt32 axis1, UInt32 axis2, UInt32 axis3);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_type_as_sine(IntPtr botnana, UInt32 position, UInt32 axis1);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_vmax(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_amax(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_set_jmax(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_get(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 configure_axis_set_name(IntPtr botnana, UInt32 position, string name);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_home_offset(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_encoder_length_unit_as_meter(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_encoder_length_unit_as_revolution(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_encoder_length_unit_as_pulse(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_encoder_ppu(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_ext_encoder_ppu(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_encoder_direction(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_ext_encoder_direction(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_closed_loop_filter(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_max_position_deviation(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_vmax(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_amax(IntPtr botnana, UInt32 position, double value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_drive_alias(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_drive_slave_position(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_drive_channel(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_ext_encoder_alias(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_ext_encoder_slave_position(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_set_ext_encoder_channel(IntPtr botnana, UInt32 position, Int32 value);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_save(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_reboot(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_poweroff(IntPtr botnana);
    }
}
