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

        // Configuration changes are made through the Botnana Control HMI.

        // JSON-API: config.motion.get
        public void ConfigMotionGet()
        {
            configure_motion_get(innerBotnana);
        }

        // JSON-API: config.group.get
        public void ConfigGroupGet(UInt32 position)
        {
            configure_group_get(innerBotnana, position);
        }

        // JSON-API: config.axis.get
        public void ConfigAxisGet(UInt32 position)
        {
            configure_axis_get(innerBotnana, position);
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
        private static extern void configure_motion_get(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_group_get(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void configure_axis_get(IntPtr botnana, UInt32 position);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_reboot(IntPtr botnana);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_poweroff(IntPtr botnana);
    }
}
