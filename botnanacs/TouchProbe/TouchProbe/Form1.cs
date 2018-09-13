using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TouchProbe
{
    public partial class Form1 : Form
    {

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void HandleMessage(string str);

        [DllImport(@"..\..\..\..\BotnanaApi\Debug\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr botnana_connect_dll(string address, HandleMessage on_error_cb);

        [DllImport(@"..\..\..\..\BotnanaApi\Debug\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void script_evaluate_dll(IntPtr desc, string script);

        [DllImport(@"..\..\..\..\BotnanaApi\Debug\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_tag_cb_dll(IntPtr desc, string tag, int count, HandleMessage hm);

        [DllImport(@"..\..\..\..\BotnanaApi\Debug\BotnanaApi.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void botnana_set_on_message_cb_dll(IntPtr desc, HandleMessage hm);


        private static IntPtr botnana;

        private static HandleMessage on_ws_error_callback = new HandleMessage(on_ws_error_cb);
        private static void on_ws_error_cb(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + str)).Start();
        }


        private static HandleMessage sdo_index_callback = new HandleMessage(sdo_index_cb);
        private static int sdo_index = 0;
        private static void sdo_index_cb(string str)
        {
            sdo_index  = Convert.ToInt32(str, 16);
        }

        private static HandleMessage sdo_subindex_callback = new HandleMessage(sdo_subindex_cb);
        private static int sdo_subindex = 0;
        private static void sdo_subindex_cb(string str)
        {
            sdo_subindex = Convert.ToInt32(str, 16);
        }

        private static HandleMessage sdo_error_callback = new HandleMessage(sdo_error_cb);
        private static string sdo_error_str = "true";
        private static void sdo_error_cb(string str)
        {
            sdo_error_str = str;
        }

        private static HandleMessage sdo_busy_callback = new HandleMessage(sdo_busy_cb);
        private static string sdo_busy_str = "true";
        private static void sdo_busy_cb(string str)
        {
            sdo_busy_str = str;
        }

        private static HandleMessage sdo_data_callback = new HandleMessage(sdo_data_cb);
        private static int sdo_data = 0;
        private static void sdo_data_cb(string str)
        {
            sdo_data = Convert.ToInt32(str, 10);
        }

        private static HandleMessage realPositionCallback = new HandleMessage(realPositionCB);
        private static string realPositionStr = "0";
        private static void realPositionCB(string str)
        {
            realPositionStr = str;
        }

        private static HandleMessage digitalInputsCallback = new HandleMessage(digitalInputsCB);
        private static string digitalInputsStr = "0";
        private static void digitalInputsCB(string str)
        {
            digitalInputsStr = str;
        }

        private static HandleMessage statusWordCallback = new HandleMessage(statusWordCB);
        private static string statusWordStr = "0";
        private static void statusWordCB(string str)
        {
            statusWordStr = str;
        }

        private static HandleMessage opModeCallback = new HandleMessage(opModeCB);
        private static string opModeStr = "0";
        private static void opModeCB(string str)
        {
            opModeStr = str;
        }

        private static HandleMessage errorCallback = new HandleMessage(errorCB);
        private static void errorCB(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start();
        }

        private static HandleMessage homingCallback = new HandleMessage(homingCB);
        private static void homingCB(string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show(str)).Start();
        }

        private static HandleMessage slavesRespondingCallback = new HandleMessage(slavesRespondingCB);
        static int slaveLen = 0;
        private static void slavesRespondingCB(string str)
        {
            slaveLen = Int32.Parse(str);
            if (slaveLen == 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("No Slave is connected!!")).Start();
            }
        }


        private static Boolean has_new_setting = false;
        private static int new_setting = 0;
        private static Boolean is_inited = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            botnana = botnana_connect_dll("192.168.7.2", on_ws_error_callback);

            botnana_set_tag_cb_dll(botnana, "sdo_index.1", 0, sdo_index_callback);
            botnana_set_tag_cb_dll(botnana, "sdo_subindex.1", 0, sdo_subindex_callback);
            botnana_set_tag_cb_dll(botnana, "sdo_error.1", 0, sdo_error_callback);
            botnana_set_tag_cb_dll(botnana, "sdo_busy.1", 0, sdo_busy_callback);
            botnana_set_tag_cb_dll(botnana, "sdo_data.1", 0, sdo_data_callback);
            botnana_set_tag_cb_dll(botnana, "real_position.1.1", 0, realPositionCallback);
            botnana_set_tag_cb_dll(botnana, "digital_inputs.1.1", 0, digitalInputsCallback);
            botnana_set_tag_cb_dll(botnana, "operation_mode.1.1", 0, opModeCallback);
            botnana_set_tag_cb_dll(botnana, "status_word.1.1", 0, statusWordCallback);
            botnana_set_tag_cb_dll(botnana, "error", 0, errorCallback);
            botnana_set_tag_cb_dll(botnana, "homing", 0, homingCallback);
            botnana_set_tag_cb_dll(botnana, "slaves_responding", 1, slavesRespondingCallback);

            script_evaluate_dll(botnana, ".ec-links 0 $60B8 1 sdo-upload-i16 1 .slave");

            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slaveLen > 0)
            {
                script_evaluate_dll(botnana, "1 .sdo 1 .slave-diff");
                textSDOAddress.Text = sdo_index.ToString("X4") + ":" + sdo_subindex.ToString("X2");

                textSDOError.Text = sdo_error_str;
                textSDOBusy.Text = sdo_busy_str;
                textSDOData.Text = sdo_data.ToString();
                
                textRealPosition.Text = realPositionStr;
                textDigitalInputs.Text = digitalInputsStr;
                textOPMode.Text = opModeStr;
                textStatusWord.Text = statusWordStr;

                if (sdo_busy_str == "false")
                {
                    if (has_new_setting)
                    {
                        script_evaluate_dll(botnana, new_setting.ToString() + " 0 $60B8 1 sdo-download-i16");
                        has_new_setting = false;
                    }
                    else if (sdo_index == 0x60B8)
                    {
                        if (!is_inited)
                        {
                            new_setting = sdo_data;
                            radioTp1Enable.Checked = (sdo_data & 0x1) != 0;
                            radioTp1Cont.Checked = (sdo_data & 0x2) != 0;
                            radioTp1Rising.Checked = (sdo_data & 0x10) != 0;
                            radioTp1Falling.Checked = (sdo_data & 0x20) != 0;
                            radioTp2Enable.Checked = (sdo_data & 0x100) != 0;
                            radioTp2Cont.Checked = (sdo_data & 0x200) != 0;
                            radioTp2Rising.Checked = (sdo_data & 0x1000) != 0;
                            radioTp2Falling.Checked = (sdo_data & 0x2000) != 0;
                            is_inited = true;
                        }
                        script_evaluate_dll(botnana, "0 $60B9 1 sdo-upload-i16");
                    }
                    else if (sdo_index == 0x60B9)
                    {
                        radioTp1Enabled.Checked = (sdo_data & 0x1) != 0;
                        radioTp1HasRising.Checked = (sdo_data & 0x2) != 0;
                        radioTp1HasFalling.Checked = (sdo_data & 0x4) != 0;
                        radioTp2Enabled.Checked = (sdo_data & 0x100) != 0;
                        radioTp2HasRising.Checked = (sdo_data & 0x200) != 0;
                        radioTp2HasFalling.Checked = (sdo_data & 0x400) != 0;
                        script_evaluate_dll(botnana, "0 $60BA 1 sdo-upload-i32");
                    }
                    else if (sdo_index == 0x60BA)
                    {
                        textTp1Pos1.Text = sdo_data.ToString();
                        script_evaluate_dll(botnana, "0 $60BB 1 sdo-upload-i32");
                    }
                    else if (sdo_index == 0x60BB)
                    {
                        textTp1Pos2.Text = sdo_data.ToString();
                        script_evaluate_dll(botnana, "0 $60BC 1 sdo-upload-i32");
                    }
                    else if (sdo_index == 0x60BC)
                    {
                        textTp2Pos1.Text = sdo_data.ToString();
                        script_evaluate_dll(botnana, "0 $60BD 1 sdo-upload-i32");
                    }
                    else if (sdo_index == 0x60BD)
                    {
                        textTp2Pos2.Text = sdo_data.ToString();
                        script_evaluate_dll(botnana, "0 $60B9 1 sdo-upload-i16");
                    }

                }
            }

        }

        
        private void radioTp1Enable_Click(object sender, EventArgs e)
        {
            radioTp1Enable.Checked = !radioTp1Enable.Checked;

            if (radioTp1Enable.Checked)
            {
                new_setting = new_setting | 0x1;
            } else
            {
                new_setting = new_setting & 0xFFFE;
            }

            has_new_setting = true;
        }

        private void radioTp2Enable_Click(object sender, EventArgs e)
        {
            radioTp2Enable.Checked = !radioTp2Enable.Checked;

            if (radioTp2Enable.Checked)
            {
                new_setting = new_setting | 0x100;
            }
            else
            {
                new_setting = new_setting & 0xFEFF;
            }

            has_new_setting = true;
        }

        private void radioTp1Cont_Click(object sender, EventArgs e)
        {
            radioTp1Cont.Checked = !radioTp1Cont.Checked;

            if (radioTp1Cont.Checked)
            {
                new_setting = new_setting | 0x2;
            }
            else
            {
                new_setting = new_setting & 0xFFFD;
            }

            has_new_setting = true;
        }

        private void radioTp2Cont_Click(object sender, EventArgs e)
        {
            radioTp2Cont.Checked = !radioTp2Cont.Checked;

            if (radioTp1Cont.Checked)
            {
                new_setting = new_setting | 0x200;
            }
            else
            {
                new_setting = new_setting & 0xFDFF;
            }

            has_new_setting = true;
        }

        private void radioTp1Rising_Click(object sender, EventArgs e)
        {
            radioTp1Rising.Checked = !radioTp1Rising.Checked;

            if (radioTp1Rising.Checked)
            {
                radioTp1Falling.Checked = false;
                new_setting = new_setting | 0xFFDF;
                new_setting = new_setting | 0x10;
            }
            else
            {
                new_setting = new_setting & 0xFFEF;
            }

            has_new_setting = true;
        }

        private void radioTp2Rising_Click(object sender, EventArgs e)
        {
            radioTp2Rising.Checked = !radioTp2Rising.Checked;

            if (radioTp2Rising.Checked)
            {
                radioTp2Falling.Checked = false;
                new_setting = new_setting & 0xDFFF;
                new_setting = new_setting | 0x1000;
            }
            else
            {
                new_setting = new_setting & 0xEFFF;
            }

            has_new_setting = true;
        }

        private void radioTp1Falling_Click(object sender, EventArgs e)
        {
            radioTp1Falling.Checked = !radioTp1Falling.Checked;
            
            if (radioTp1Falling.Checked)
            {
                radioTp1Rising.Checked = false;
                new_setting = new_setting & 0xFFEF;
                new_setting = new_setting | 0x20;
               
            }
            else
            {
                new_setting = new_setting & 0xFFDF;
            }

            has_new_setting = true;
        }

        private void radioTp2Falling_Click(object sender, EventArgs e)
        {
            radioTp2Falling.Checked = !radioTp2Falling.Checked;

            if (radioTp2Falling.Checked)
            {
               radioTp2Rising.Checked = false;
               new_setting = new_setting & 0xEFFF;
               new_setting = new_setting | 0x2000;
            }
            else
            {
                new_setting = new_setting & 0xDFFF;
            }

            has_new_setting = true;
        }

        private void buttonStartHoming_Click(object sender, EventArgs e)
        {
            
            script_evaluate_dll(botnana, "abort-program");
           
            script_evaluate_dll(botnana, "deploy 1 1 reset-fault 1 1 until-no-fault" +
               " 33 1 1 homing-method! hm 1 1 op-mode! until-no-requests 100 ms 1 1  drive-on 1 1 until-drive-on" +
              " 1 1 go 1 1 until-target-reached pp 1 1 op-mode! 1 1 until-no-requests 100 ms drive-off .( homing|Homing is Ok and change to PP mode)" +
             " ;deploy");
        }

        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            script_evaluate_dll(botnana, textEvalute.Text);
            textEvalute.ResetText();
        }
    }
}
