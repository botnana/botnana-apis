﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Globalization;
using BotnanaLib;

namespace DIO
{

    public partial class Form1 : Form
    {
        private Botnana bot;

        private int wsState = 0;
        private HandleMessage onWSError;
        public void OnWSErrorCallback(IntPtr ptr, string data)
        {
            wsState = 0;
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + data)).Start();
        }

        private HandleMessage onWSOpen;
        public void OnWSOpenCallback(IntPtr ptr, string data)
        {
            wsState = 2;
        }

        private int messageCount = 0;
        private HandleMessage onMessage;
        public void OnMessageCallback(IntPtr ptr, string data)
        {
            messageCount++;
            if (messageCount > 256)
            {
                messageCount = 0;
            }
        }

        private HandleMessage onDiWord;
        private UInt32 diWord;
        private void OnDiWordCallback(IntPtr ptr, string str)
        {
            diWord = UInt32.Parse(str);
        }

        private HandleMessage onDoWord;
        private UInt32 doWord;
        private void OnDoWordCallback(IntPtr ptr, string str)
        {
            doWord = UInt32.Parse(str);

        }

        private int slavesCount = 0;
        private HandleMessage onSlavesResponding;
        private void OnSlavesRespondingCallback(IntPtr ptr, string str)
        {
            slavesCount = int.Parse(str);
        }

        private int slavesState = 0;
        private HandleMessage onSlavesState;
        private void OnSlavesStateCallback(IntPtr ptr, string str)
        {
            slavesState = int.Parse(str);
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();
            thisProc.PriorityClass = ProcessPriorityClass.RealTime;

            bot = new Botnana("192.168.7.2");

            onWSOpen = new HandleMessage(OnWSOpenCallback);
            bot.SetOnOpenCB(IntPtr.Zero, onWSOpen);

            onWSError = new HandleMessage(OnWSErrorCallback);
            bot.SetOnErrorCB(IntPtr.Zero, onWSError);

            onMessage = new HandleMessage(OnMessageCallback);
            bot.SetOnMessageCB(IntPtr.Zero, onMessage);

            onDiWord = new HandleMessage(OnDiWordCallback);
            bot.SetTagCB("din_wd.1.3", 0, IntPtr.Zero, onDiWord);

            onDoWord = new HandleMessage(OnDoWordCallback);
            bot.SetTagCB("dout_wd.1.2", 0, IntPtr.Zero, onDoWord);

            onSlavesResponding = new HandleMessage(OnSlavesRespondingCallback);
            bot.SetTagCB(@"slaves_responding", 0, IntPtr.Zero, onSlavesResponding);

            onSlavesState = new HandleMessage(OnSlavesStateCallback);
            bot.SetTagCB(@"al_states", 0, IntPtr.Zero, onSlavesState);

            bot.Connect();

            timer1.Interval = 50;
            timer1.Enabled = true;

            timer2.Interval = 1000;
            timer2.Enabled = true;
        }

        private bool hasSlaveInfo = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slavesCount > 0)
            {
                if (hasSlaveInfo)
                {
                    bot.EvaluateScript("2 .slave-diff 3 .slave-diff");
                }
                else
                {
                    bot.EvaluateScript("2 .slave 3 .slave");
                    hasSlaveInfo = true;
                }
            }

            labMessageCount.Text = messageCount.ToString("X2");
            textSlavesCount.Text = slavesCount.ToString();
            textSlavesState.Text = slavesState.ToString();
            checkDo1.Checked = (doWord & 0x1) != 0;
            checkDo2.Checked = (doWord & 0x2) != 0;
            checkDo3.Checked = (doWord & 0x4) != 0;
            checkDo4.Checked = (doWord & 0x8) != 0;
            checkDo5.Checked = (doWord & 0x10) != 0;
            checkDo6.Checked = (doWord & 0x20) != 0;
            checkDo7.Checked = (doWord & 0x40) != 0;
            checkDo8.Checked = (doWord & 0x80) != 0;
            checkDo9.Checked = (doWord & 0x100) != 0;
            checkDo10.Checked = (doWord & 0x200) != 0;
            checkDo11.Checked = (doWord & 0x400) != 0;
            checkDo12.Checked = (doWord & 0x800) != 0;
            checkDo13.Checked = (doWord & 0x1000) != 0;
            checkDo14.Checked = (doWord & 0x2000) != 0;
            checkDo15.Checked = (doWord & 0x4000) != 0;
            checkDo16.Checked = (doWord & 0x8000) != 0;

            checkDi1.Checked = (diWord & 0x1) != 0;
            checkDi2.Checked = (diWord & 0x2) != 0;
            checkDi3.Checked = (diWord & 0x4) != 0;
            checkDi4.Checked = (diWord & 0x8) != 0;
            checkDi5.Checked = (diWord & 0x10) != 0;
            checkDi6.Checked = (diWord & 0x20) != 0;
            checkDi7.Checked = (diWord & 0x40) != 0;
            checkDi8.Checked = (diWord & 0x80) != 0;
            checkDi9.Checked = (diWord & 0x100) != 0;
            checkDi10.Checked = (diWord & 0x200) != 0;
            checkDi11.Checked = (diWord & 0x400) != 0;
            checkDi12.Checked = (diWord & 0x800) != 0;
            checkDi13.Checked = (diWord & 0x1000) != 0;
            checkDi14.Checked = (diWord & 0x2000) != 0;
            checkDi15.Checked = (diWord & 0x4000) != 0;
            checkDi16.Checked = (diWord & 0x8000) != 0;

        }

        private void checkDo1_Click(object sender, EventArgs e)
        {
            int value = checkDo1.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 1 2 ec-dout!");
        }

        private void checkDo2_Click(object sender, EventArgs e)
        {
            int value = checkDo2.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 2 2 ec-dout!");
        }

        private void checkDo3_Click(object sender, EventArgs e)
        {
            int value = checkDo3.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 3 2 ec-dout!");
        }

        private void checkDo4_Click(object sender, EventArgs e)
        {
            int value = checkDo4.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 4 2 ec-dout!");
        }

        private void checkDo5_Click(object sender, EventArgs e)
        {
            int value = checkDo5.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 5 2 ec-dout!");
        }

        private void checkDo6_Click(object sender, EventArgs e)
        {
            int value = checkDo6.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 6 2 ec-dout!");
        }

        private void checkDo7_Click(object sender, EventArgs e)
        {
            int value = checkDo7.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 7 2 ec-dout!");
        }

        private void checkDo8_Click(object sender, EventArgs e)
        {
            int value = checkDo8.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 8 2 ec-dout!");
        }

        private void checkDo9_Click(object sender, EventArgs e)
        {
            int value = checkDo9.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 9 2 ec-dout!");
        }

        private void checkDo10_Click(object sender, EventArgs e)
        {
            int value = checkDo10.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 10 2 ec-dout!");
        }

        private void checkDo11_Click(object sender, EventArgs e)
        {
            int value = checkDo11.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 11 2 ec-dout!");
        }

        private void checkDo12_Click(object sender, EventArgs e)
        {
            int value = checkDo12.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 12 2 ec-dout!");
        }

        private void checkDo13_Click(object sender, EventArgs e)
        {
            int value = checkDo13.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 13 2 ec-dout!");
        }

        private void checkDo14_Click(object sender, EventArgs e)
        {
            int value = checkDo14.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 14 2 ec-dout!");
        }
        private void checkDo15_Click(object sender, EventArgs e)
        {
            int value = checkDo15.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 15 2 ec-dout!");
        }
        private void checkDo16_Click(object sender, EventArgs e)
        {
            int value = checkDo16.Checked ? 1 : 0;
            bot.EvaluateScript(value.ToString() + " 16 2 ec-dout!");
        }

        private void set_dout_wd()
        {
            Int32 number;
            if (Int32.TryParse(textDoWord.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out number))
            {
                bot.EvaluateScript(number + @" 1 2 ec-wdout!");
            }
            textDoWord.Text = "";
        }

        private void textDoWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                set_dout_wd();
            }
        }

        private void textDoWord_Leave(object sender, EventArgs e)
        {
            set_dout_wd();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bot.EvaluateScript(".ec-links");
            // 設定 WsConnected 連線狀態的顏色 
            if (wsState == 2)
            {
                buttonWsConnected.BackColor = Color.SpringGreen;
            }
            else if (wsState == 1)
            {
                buttonWsConnected.BackColor = Color.Gold;
            }
            else
            {
                buttonWsConnected.BackColor = Color.IndianRed;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bot.Connect();
            if (wsState != 2)
            {
                wsState = 1;
                hasSlaveInfo = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.Reboot();
        }
    }
}
