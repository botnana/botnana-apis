﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BotnanaLib
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HandleMessage(string value);

    public class Botnana
    {

        public Botnana()
        {
            innerBotnana = botnana_new_dll("192.168.7.2");
            innerProgrm = program_new_dll("program");
        }

        public Botnana(string ip)
        {
            innerBotnana = botnana_new_dll(ip);
            innerProgrm = program_new_dll("program");
        }

        public void Connect()
        {
            botnana_connect_dll(innerBotnana);
        }

        public void Disconnect()
        {
            botnana_disconnect_dll(innerBotnana);
        }

        public void EvaluateScript(string script)
        {
            script_evaluate_dll(innerBotnana, script);
        }

        public void SetOnOpenCB(HandleMessage hm)
        {
            botnana_set_on_open_cb_dll(innerBotnana, hm);
        }

        public void SetOnErrorCB(HandleMessage hm)
        {
            botnana_set_on_error_cb_dll(innerBotnana, hm);
        }
        
        public void SetOnMessageCB(HandleMessage hm)
        {
            botnana_set_on_message_cb_dll(innerBotnana, hm);
        }

        public void SetOnSendCB(HandleMessage hm)
        {
            botnana_set_on_send_cb_dll(innerBotnana, hm);
        }

        public void SetTagCB(string tag, int count, HandleMessage hm)
        {
            botnana_set_tag_cb_dll(innerBotnana, tag, count, hm);
        }
             
        public void DeployProgram()
        {
            program_deploy_dll(innerBotnana, innerProgrm);
        }

        public void AddProgramLine(string script)
        {
            program_line_dll(innerProgrm, script);
        }

        public void RunProgram()
        {
            program_run_dll(innerBotnana, innerProgrm);
        }

        public void ClearProgram()
        {
            program_clear_dll(innerProgrm);
        }

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
        public void ClearSFC(string path)
        {
            script_evaluate_dll(innerBotnana, $"0sfc");
        }
        
        private IntPtr innerBotnana;
        private IntPtr innerProgrm;

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr botnana_new_dll(string ip);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_connect_dll(IntPtr desc);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_disconnect_dll(IntPtr desc);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void script_evaluate_dll(IntPtr desc, string script);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr program_new_dll(string name);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_tag_cb_dll(IntPtr desc, string tag, int count, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_open_cb_dll(IntPtr desc, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_error_cb_dll(IntPtr desc, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_message_cb_dll(IntPtr desc, HandleMessage hm);

        [DllImport(@"BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_send_cb_dll(IntPtr desc, HandleMessage hm);

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
    } 
}
