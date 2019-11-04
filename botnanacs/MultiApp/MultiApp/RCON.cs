using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotnanaLib;

namespace MultiApp
{
    public partial class FormRCON : Form
    {
        private delegate void RCONdeg();
        
        private int currAxis = 5;

        private HandleMessage onRCONReady;
        private HandleMessage onPLCEnabled;
        private HandleMessage onGWControl;
        private HandleMessage onGWStatus;
        private HandleMessage onAXControl;
        private HandleMessage onAXPSD;
        private HandleMessage onAXPW;
        private HandleMessage onAXSPD;
        private HandleMessage onAXACDEC;
        private HandleMessage onAXPCLV;
        private HandleMessage onAXStatus;
        private HandleMessage onAXPPD;
        private HandleMessage onAXMCCV;
        private HandleMessage onAXPSPD;
        private HandleMessage onAXALMC;

        public void Reset()
        {
            BeginInvoke(new RCONdeg(() =>
            {
                buttonRCONReady.Text = "RCON not ready";
                buttonRCONReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
            }));
        }

        public void Initialize()
        {
            // 取得裝置資訊
            FormApp.BotEvaluateScript(@".rcon-infos");
        }

        public void Awake()
        {
            timer1.Enabled = true;
        }

        public void Sleep()
        {
            timer1.Enabled = false;
        }

        public void EmergencyStop()
        {
            // 緊急停止不檢查連線狀態，嘗試送出命令
            FormApp.bot.EvaluateScript(@"ems-rcon");
        }

        public FormRCON()
        {
            InitializeComponent();

            TopLevel = false;
            Visible = true;
            FormBorderStyle = FormBorderStyle.None;

            // On rcon_ready tag callback.
            onRCONReady = new HandleMessage((IntPtr ptr, string str) =>
            {
                if (str == "-1")
                {
                    BeginInvoke(new RCONdeg(() =>
                    {
                        buttonRCONReady.Text = "RCON ready";
                        buttonRCONReady.BackColor = Color.FromArgb(0x53, 0xFF, 0x53);
                    }));
                }
                else
                {
                    BeginInvoke(new RCONdeg(() =>
                    {
                        buttonRCONReady.Text = "RCON not ready";
                        buttonRCONReady.BackColor = Color.FromArgb(0xFF, 0x2D, 0x2D);
                    }));
                }
            });
            FormApp.bot.SetTagCB("rcon_ready", 0, IntPtr.Zero, onRCONReady);

            // On PLC_enabled tag callback.
            onPLCEnabled = new HandleMessage((IntPtr ptr, string str) =>
            {
                if (str == "-1")
                {
                    BeginInvoke(new RCONdeg(() => buttonPLCEnable.Text = "Disable PLC"));
                } else
                {
                    BeginInvoke(new RCONdeg(() => buttonPLCEnable.Text = "Enable PLC"));
                }
            });
            FormApp.bot.SetTagCB("PLC_enabled", 0, IntPtr.Zero, onPLCEnabled);

            // On GW_control tag callback.
            onGWControl = new HandleMessage((IntPtr ptr, string str) =>
            {
                int control = int.Parse(str);
                bool gwMON = (control & 0x8000) != 0;
                BeginInvoke(new RCONdeg(() => radioButtonGWMON.Checked = gwMON));

            });
            FormApp.bot.SetTagCB("GW_control", 0, IntPtr.Zero, onGWControl);

            // On GW_status tag callback.
            onGWStatus = new HandleMessage((IntPtr ptr, string str) =>
            {
                int status = int.Parse(str);
                Byte gwALMC = (Byte)(status & 0xff);
                bool gwSEMG = (status & 0x100) != 0;
                bool gwALML = (status & 0x400) != 0;
                bool gwALMH = (status & 0x800) != 0;
                bool gwMOD = (status & 0x1000) != 0;
                bool gwERRT = (status & 0x2000) != 0;
                bool gwLERC = (status & 0x4000) != 0;
                bool gwRUN = (status & 0x8000) != 0;
                UInt16 gwLNK = (UInt16)((status & 0xffff0000) >> 16);
                BeginInvoke(new RCONdeg(() =>
                {
                    textBoxGWALMC.Text = "0x" + gwALMC.ToString("X2");
                    radioButtonGWSEMG.Checked = gwSEMG;
                    radioButtonGWALML.Checked = gwALML;
                    radioButtonGWALMH.Checked = gwALMH;
                    radioButtonGWMOD.Checked = gwMOD;
                    radioButtonGWERRT.Checked = gwERRT;
                    radioButtonGWLERC.Checked = gwLERC;
                    radioButtonGWRUN.Checked = gwRUN;
                    textBoxGWLNK.Text = "0x" + gwLNK.ToString("X4");
                }));
            });
            FormApp.bot.SetTagCB("GW_status", 0, IntPtr.Zero, onGWStatus);

            // On AX_control tag callback.
            onAXControl = new HandleMessage((IntPtr ptr, string str) =>
            {
                int control = int.Parse(str);
                bool axCSTR = (control & 0x1) != 0;
                bool axHOME = (control & 0x2) != 0;
                bool axSTP = (control & 0x4) != 0;
                bool axRES = (control & 0x8) != 0;
                bool axSON = (control & 0x10) != 0;
                bool axJISL = (control & 0x20) != 0;
                bool axJVEL = (control & 0x40) != 0;
                bool axJOGN = (control & 0x80) != 0;
                bool axJOGP = (control & 0x100) != 0;
                bool axPUSH = (control & 0x1000) != 0;
                bool axDIR = (control & 0x2000) != 0;
                bool axINC = (control & 0x4000) != 0;
                bool axBKRL = (control & 0x8000) != 0;
                BeginInvoke(new RCONdeg(() =>
                {
                    radioButtonAXCSTR.Checked = axCSTR;
                    radioButtonAXHOME.Checked = axHOME;
                    radioButtonAXSTP.Checked = axSTP;
                    radioButtonAXRES.Checked = axRES;
                    radioButtonAXJISL.Checked = axJISL;
                    radioButtonAXJVEL.Checked = axJVEL;
                    radioButtonAXJOGN.Checked = axJOGN;
                    radioButtonAXJOGP.Checked = axJOGP;
                    radioButtonAXPUSH.Checked = axPUSH;
                    radioButtonAXDIR.Checked = axDIR;
                    radioButtonAXINC.Checked = axINC;
                    radioButtonAXBKRL.Checked = axBKRL;
                }));
            });
            FormApp.bot.SetTagCB("AX_control", 0, IntPtr.Zero, onAXControl);

            // On AX_PSD tag callback.
            onAXPSD = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => { if (!axPSDFocus) textBoxAXPSD.Text = str; }));
            });
            FormApp.bot.SetTagCB("AX_PSD", 0, IntPtr.Zero, onAXPSD);

            // On AX_PW tag callback.
            onAXPW = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => { if (!axPWFocus) textBoxAXPW.Text = str; }));
            });
            FormApp.bot.SetTagCB("AX_PW", 0, IntPtr.Zero, onAXPW);

            // On AX_SPD tag callback.
            onAXSPD = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => { if (!axSPDFocus) textBoxAXSPD.Text = str; }));
            });
            FormApp.bot.SetTagCB("AX_SPD", 0, IntPtr.Zero, onAXSPD);

            // On AX_ACDEC tag callback.
            onAXACDEC = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => { if (!axACDECFocus) textBoxAXACDEC.Text = str; }));
            });
            FormApp.bot.SetTagCB("AX_ACDEC", 0, IntPtr.Zero, onAXACDEC);

            // On AX_PCLV tag callback.
            onAXPCLV = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => { if (!axPCLVFocus) textBoxAXPCLV.Text = str; }));
            });
            FormApp.bot.SetTagCB("AX_PCLV", 0, IntPtr.Zero, onAXPCLV);

            // On AX_status tag callback.
            onAXStatus = new HandleMessage((IntPtr ptr, string str) =>
            {
                int status = int.Parse(str);
                bool axPEND = (status & 0x1) != 0;
                bool axHEND = (status & 0x2) != 0;
                bool axMOVE = (status & 0x4) != 0;
                bool axALM = (status & 0x8) != 0;
                bool axSV = (status & 0x10) != 0;
                bool axPSFL = (status & 0x20) != 0;
                bool axLOAD = (status & 0x40) != 0;
                bool axALML = (status & 0x80) != 0;
                bool axMEND = (status & 0x100) != 0;
                bool axWEND = (status & 0x200) != 0;
                bool axMODES = (status & 0x400) != 0;
                bool axPZONE = (status & 0x800) != 0;
                bool axZONE1 = (status & 0x1000) != 0;
                bool axZONE2 = (status & 0x2000) != 0;
                bool axCRDY = (status & 0x4000) != 0;
                bool axEMGS = (status & 0x8000) != 0;
                BeginInvoke(new RCONdeg(() =>
                {
                    radioButtonAXPEND.Checked = axPEND;
                    radioButtonAXHEND.Checked = axHEND;
                    radioButtonAXMOVE.Checked = axMOVE;
                    radioButtonAXALM.Checked = axALM;
                    radioButtonAXSV.Checked = axSV;
                    radioButtonAXPSFL.Checked = axPSFL;
                    radioButtonAXLOAD.Checked = axLOAD;
                    radioButtonAXALML.Checked = axALML;
                    radioButtonAXMEND.Checked = axMEND;
                    radioButtonAXWEND.Checked = axWEND;
                    radioButtonAXMODES.Checked = axMODES;
                    radioButtonAXPZONE.Checked = axPZONE;
                    radioButtonAXZONE1.Checked = axZONE1;
                    radioButtonAXZONE2.Checked = axZONE2;
                    radioButtonAXCRDY.Checked = axCRDY;
                    radioButtonAXEMGS.Checked = axEMGS;
                }));
            });
            FormApp.bot.SetTagCB("AX_status", 0, IntPtr.Zero, onAXStatus);

            // On AX_PPD tag callback.
            onAXPPD = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => textBoxAXPPD.Text = str));
            });
            FormApp.bot.SetTagCB("AX_PPD", 0, IntPtr.Zero, onAXPPD);

            // On AX_MCCV tag callback.
            onAXMCCV = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => textBoxAXMCCV.Text = str));
            });
            FormApp.bot.SetTagCB("AX_MCCV", 0, IntPtr.Zero, onAXMCCV);

            // On AX_PSPD tag callback.
            onAXPSPD = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => textBoxAXPSPD.Text = str));
            });
            FormApp.bot.SetTagCB("AX_PSPD", 0, IntPtr.Zero, onAXPSPD);

            // On AX_ALMC tag callback.
            onAXALMC = new HandleMessage((IntPtr ptr, string str) =>
            {
                BeginInvoke(new RCONdeg(() => textBoxAXALMC.Text = "0x" + UInt16.Parse(str).ToString("X2")));
            });
            FormApp.bot.SetTagCB("AX_ALMC", 0, IntPtr.Zero, onAXALMC);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 50;
        }

        private void buttonPLCEnable_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Enable PLC")
            {
                FormApp.BotEvaluateScript(@"plc-enabled on");
            } else
            {
                FormApp.BotEvaluateScript(@"plc-enabled off");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!FormApp.stopPolling) FormApp.BotEvaluateScript(currAxis.ToString() + @" .rcon-system");
        }

        private void comboBoxSelectedAX_SelectedIndexChanged(object sender, EventArgs e)
        {
            currAxis = ((ComboBox)sender).SelectedIndex;
        }

        private void radioButtonGWMON_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                FormApp.BotEvaluateScript(@"-gw-mon");
            } else
            {
                FormApp.BotEvaluateScript(@"+gw-mon");
            }
        }

        private Boolean axPSDFocus = false;
        private void textBoxAXPSD_MouseDown(object sender, MouseEventArgs e)
        {
            axPSDFocus = true;
        }

        private void textBoxAXPSD_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-spec!");
            axPSDFocus = false;
        }

        private void textBoxAXPSD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-spec!");
                axPSDFocus = false;
            }
        }

        private Boolean axPWFocus = false;
        private void textBoxAXPW_MouseDown(object sender, MouseEventArgs e)
        {
            axPWFocus = true;
        }

        private void textBoxAXPW_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-width!");
            axPWFocus = false;
        }

        private void textBoxAXPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-pos-width!");
                axPWFocus = false;
            }
        }

        private Boolean axSPDFocus = false;
        private void textBoxAXSPD_MouseDown(object sender, MouseEventArgs e)
        {
            axSPDFocus = true;
        }

        private void textBoxAXSPD_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-spd!");
            axSPDFocus = false;
        }

        private void textBoxAXSPD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-spd!");
                axSPDFocus = false;
            }
        }

        private Boolean axACDECFocus = false;
        private void textBoxAXACDEC_MouseDown(object sender, MouseEventArgs e)
        {
            axACDECFocus = true;
        }

        private void textBoxAXACDEC_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-ac/dec!");
            axACDECFocus = false;
        }

        private void textBoxAXACDEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-ac/dec!");
                axACDECFocus = false;
            }
        }

        private Boolean axPCLVFocus = false;
        private void textBoxAXPCLV_MouseDown(object sender, MouseEventArgs e)
        {
            axPCLVFocus = true;
        }

        private void textBoxAXPCLV_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-curr-limit!");
            axPCLVFocus = false;
        }

        private void textBoxAXPCLV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox tb = (TextBox)sender;
                FormApp.BotEvaluateScript(tb.Text + " " + currAxis.ToString() + " ax-curr-limit!");
                axPCLVFocus = false;
            }
        }

        private void radioButtonAXCSTR_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-cstr"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-cstr"); }
        }

        private void radioButtonAXHOME_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-home"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-home"); }
        }

        private void radioButtonAXSTP_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-stp"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-stp"); }
        }

        private void radioButtonAXRES_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-res"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-res"); }
        }

        private void radioButtonAXSON_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-son"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-son"); }
        }

        private void radioButtonAXJISL_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-jisl"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-jisl"); }
        }

        private void radioButtonAXJVEL_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-jvel"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-jvel"); }
        }

        private void radioButtonAXJOGN_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-jog-"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-jog-"); }
        }

        private void radioButtonAXJOGP_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-jog+"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-jog+"); }
        }

        private void radioButtonAXPUSH_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-push"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-push"); }
        }

        private void radioButtonAXDIR_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-dir"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-dir"); }
        }

        private void radioButtonAXINC_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-inc"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-inc"); }
        }

        private void radioButtonAXBKRL_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) { FormApp.BotEvaluateScript(currAxis.ToString() + @" -ax-bkrl"); } else { FormApp.BotEvaluateScript(currAxis.ToString() + @" +ax-bkrl"); }
        }
    }
}
