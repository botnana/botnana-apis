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
using BotnanaLib;

namespace TouchProbe
{
    public partial class Form1 : Form
    {

        private Botnana bot;

        private HandleMessage onWsErrorCallback;
        private void OnWsErrorCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("WS error : " + str)).Start();
        }

        private HandleMessage sdoIndexCallback;
        private int sdoIndex = 0;
        private void SdoIndexCB(IntPtr dataPtr, string str)
        {
            sdoIndex = Convert.ToInt32(str, 16);
        }

        private HandleMessage sdoSubindexCallback;
        private int sdoSubindex = 0;
        private void SdoSubindexCB(IntPtr dataPtr, string str)
        {
            sdoSubindex = Convert.ToInt32(str, 16);
        }

        private HandleMessage sdoErrorCallback;
        private string sdoErrorStr = "true";
        private void SdoErrorCB(IntPtr dataPtr, string str)
        {
            sdoErrorStr = str;
        }

        private HandleMessage sdoBusyCallback;
        private string sdoBusyStr = "true";
        private void SdoBusyCB(IntPtr dataPtr, string str)
        {
            sdoBusyStr = str;
        }

        private HandleMessage sdoDataCallback;
        private int sdoData = 0;
        private void SdoDataCB(IntPtr dataPtr, string str)
        {
            sdoData = Convert.ToInt32(str, 10);
        }

        private HandleMessage realPositionCallback;
        private string realPositionStr = "0";
        private void RealPositionCB(IntPtr dataPtr, string str)
        {
            realPositionStr = str;
        }

        private HandleMessage digitalInputsCallback;
        private string digitalInputsStr = "0";
        private void DigitalInputsCB(IntPtr dataPtr, string str)
        {
            digitalInputsStr = str;
        }

        private HandleMessage statusWordCallback;
        private string statusWordStr = "0";
        private void StatusWordCB(IntPtr dataPtr, string str)
        {
            statusWordStr = str;
        }

        private HandleMessage opModeCallback;
        private string opModeStr = "0";
        private void OpModeCB(IntPtr dataPtr, string str)
        {
            opModeStr = str;
        }

        private HandleMessage errorCallback;
        private void ErrorCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show("error|" + str)).Start();
        }

        private HandleMessage homingCallback;
        private void HomingCB(IntPtr dataPtr, string str)
        {
            new Thread(() => System.Windows.Forms.MessageBox.Show(str)).Start();
        }

        private HandleMessage slavesRespondingCallback;
        private int slaveLen = 0;
        private void SlavesRespondingCB(IntPtr dataPtr, string str)
        {
            slaveLen = Int32.Parse(str);
            if (slaveLen == 0)
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("No Slave is connected!!")).Start();
            }
        }

        private Boolean hasNewSetting = false;
        private int newSetting = 0;
        private Boolean isInited = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bot = new Botnana("192.168.7.2");

            onWsErrorCallback = new HandleMessage(OnWsErrorCB);
            bot.SetOnErrorCB(IntPtr.Zero, onWsErrorCallback);

            sdoIndexCallback = new HandleMessage(SdoIndexCB);
            bot.SetTagCB("sdo_index.1", 0, IntPtr.Zero, sdoIndexCallback);

            sdoSubindexCallback = new HandleMessage(SdoSubindexCB);
            bot.SetTagCB("sdo_subindex.1", 0, IntPtr.Zero, sdoSubindexCallback);

            sdoErrorCallback = new HandleMessage(SdoErrorCB);
            bot.SetTagCB("sdo_error.1", 0, IntPtr.Zero, sdoErrorCallback);

            sdoBusyCallback = new HandleMessage(SdoBusyCB);
            bot.SetTagCB("sdo_busy.1", 0, IntPtr.Zero, sdoBusyCallback);

            sdoDataCallback = new HandleMessage(SdoDataCB);
            bot.SetTagCB("sdo_data.1", 0, IntPtr.Zero, sdoDataCallback);

            realPositionCallback = new HandleMessage(RealPositionCB);
            bot.SetTagCB("real_position.1.1", 0, IntPtr.Zero, realPositionCallback);

            digitalInputsCallback = new HandleMessage(DigitalInputsCB);
            bot.SetTagCB("digital_inputs.1.1", 0, IntPtr.Zero, digitalInputsCallback);

            opModeCallback = new HandleMessage(OpModeCB);
            bot.SetTagCB("operation_mode.1.1", 0, IntPtr.Zero, opModeCallback);

            statusWordCallback = new HandleMessage(StatusWordCB);
            bot.SetTagCB("status_word.1.1", 0, IntPtr.Zero, statusWordCallback);

            errorCallback = new HandleMessage(ErrorCB);
            bot.SetTagCB("error", 0, IntPtr.Zero, errorCallback);

            homingCallback = new HandleMessage(HomingCB);
            bot.SetTagCB("homing", 0, IntPtr.Zero, homingCallback);

            slavesRespondingCallback = new HandleMessage(SlavesRespondingCB);
            bot.SetTagCB("slaves_responding", 1, IntPtr.Zero, slavesRespondingCallback);
            bot.Connect();
            Thread.Sleep(1000);
            bot.EvaluateScript(".ec-links 0 $60B8 1 sdo-upload-i16 1 .slave");

            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slaveLen > 0)
            {
                bot.EvaluateScript("1 .sdo 1 .slave-diff");
                textSDOAddress.Text = sdoIndex.ToString("X4") + ":" + sdoSubindex.ToString("X2");

                textSDOError.Text = sdoErrorStr;
                textSDOBusy.Text = sdoBusyStr;
                textSDOData.Text = sdoData.ToString();

                textRealPosition.Text = realPositionStr;
                textDigitalInputs.Text = digitalInputsStr;
                textOPMode.Text = opModeStr;
                textStatusWord.Text = statusWordStr;

                // 如果SDO 已經設定完成
                if (sdoBusyStr == "false")
                {
                    if (hasNewSetting)
                    {
                        // 如果有新的設定值,就設定 0x60B8 暫存器
                        bot.EvaluateScript(newSetting.ToString() + " 0 $60B8 1 sdo-download-i16");
                        hasNewSetting = false;
                    }
                    else if (sdoIndex == 0x60B8)
                    {
                        // 依 60B8 的讀值設定畫面上的 radio 元件
                        if (!isInited)
                        {
                            newSetting = sdoData;
                            radioTp1Enable.Checked = (sdoData & 0x1) != 0;
                            radioTp1Cont.Checked = (sdoData & 0x2) != 0;
                            radioTp1Rising.Checked = (sdoData & 0x10) != 0;
                            radioTp1Falling.Checked = (sdoData & 0x20) != 0;
                            radioTp2Enable.Checked = (sdoData & 0x100) != 0;
                            radioTp2Cont.Checked = (sdoData & 0x200) != 0;
                            radioTp2Rising.Checked = (sdoData & 0x1000) != 0;
                            radioTp2Falling.Checked = (sdoData & 0x2000) != 0;
                            isInited = true;
                        }
                        // 使用 SDO 取回 0x60B9 (Touch porbe status) 暫存器內容
                        bot.EvaluateScript("0 $60B9 1 sdo-upload-i16");
                    }
                    else if (sdoIndex == 0x60B9)
                    {
                        // 依 60B9 的讀值設定畫面上的 radio 元件
                        radioTp1Enabled.Checked = (sdoData & 0x1) != 0;
                        radioTp1HasRising.Checked = (sdoData & 0x2) != 0;
                        radioTp1HasFalling.Checked = (sdoData & 0x4) != 0;
                        radioTp2Enabled.Checked = (sdoData & 0x100) != 0;
                        radioTp2HasRising.Checked = (sdoData & 0x200) != 0;
                        radioTp2HasFalling.Checked = (sdoData & 0x400) != 0;

                        // 使用 SDO 取回 0x60BA (Touch probe pos1 pos value) 暫存器內容
                        bot.EvaluateScript("0 $60BA 1 sdo-upload-i32");
                    }
                    else if (sdoIndex == 0x60BA)
                    {
                        // 更新 Touch probe pos1 pos value 的 text 元件
                        textTp1Pos1.Text = sdoData.ToString();
                        // 使用 SDO 取回 0x60BB (Touch probe pos1 neg value) 暫存器內容
                        bot.EvaluateScript("0 $60BB 1 sdo-upload-i32");
                    }
                    else if (sdoIndex == 0x60BB)
                    {
                        // 更新 Touch probe pos1 neg value 的 text 元件
                        textTp1Pos2.Text = sdoData.ToString();
                        // 使用 SDO 取回 0x60BC (Touch probe pos2 pos value) 暫存器內容
                        bot.EvaluateScript("0 $60BC 1 sdo-upload-i32");
                    }
                    else if (sdoIndex == 0x60BC)
                    {
                        // 更新 Touch probe pos2 pos value 的 text 元件
                        textTp2Pos1.Text = sdoData.ToString();
                        // 使用 SDO 取回 0x60BD (Touch probe pos2 neg value) 暫存器內容
                        bot.EvaluateScript("0 $60BD 1 sdo-upload-i32");
                    }
                    else if (sdoIndex == 0x60BD)
                    {
                        // 更新 Touch probe pos2 neg value 的 text 元件
                        textTp2Pos2.Text = sdoData.ToString();
                        // 使用 SDO 取回 0x60B9 (Touch porbe status) 暫存器內容
                        bot.EvaluateScript("0 $60B9 1 sdo-upload-i16");
                    }

                }
            }
        }

        private void radioTp1Enable_Click(object sender, EventArgs e)
        {
            radioTp1Enable.Checked = !radioTp1Enable.Checked;

            if (radioTp1Enable.Checked)
            {
                newSetting = newSetting | 0x1;
            }
            else
            {
                newSetting = newSetting & 0xFFFE;
            }

            hasNewSetting = true;
        }

        private void radioTp2Enable_Click(object sender, EventArgs e)
        {
            radioTp2Enable.Checked = !radioTp2Enable.Checked;

            if (radioTp2Enable.Checked)
            {
                newSetting = newSetting | 0x100;
            }
            else
            {
                newSetting = newSetting & 0xFEFF;
            }

            hasNewSetting = true;
        }

        private void radioTp1Cont_Click(object sender, EventArgs e)
        {
            radioTp1Cont.Checked = !radioTp1Cont.Checked;

            if (radioTp1Cont.Checked)
            {
                newSetting = newSetting | 0x2;
            }
            else
            {
                newSetting = newSetting & 0xFFFD;
            }

            hasNewSetting = true;
        }

        private void radioTp2Cont_Click(object sender, EventArgs e)
        {
            radioTp2Cont.Checked = !radioTp2Cont.Checked;

            if (radioTp1Cont.Checked)
            {
                newSetting = newSetting | 0x200;
            }
            else
            {
                newSetting = newSetting & 0xFDFF;
            }

            hasNewSetting = true;
        }

        private void radioTp1Rising_Click(object sender, EventArgs e)
        {
            radioTp1Rising.Checked = !radioTp1Rising.Checked;

            if (radioTp1Rising.Checked)
            {
                radioTp1Falling.Checked = false;
                newSetting = newSetting & 0xFFDF;
                newSetting = newSetting | 0x10;
            }
            else
            {
                newSetting = newSetting & 0xFFEF;
            }

            hasNewSetting = true;
        }

        private void radioTp2Rising_Click(object sender, EventArgs e)
        {
            radioTp2Rising.Checked = !radioTp2Rising.Checked;

            if (radioTp2Rising.Checked)
            {
                radioTp2Falling.Checked = false;
                newSetting = newSetting & 0xDFFF;
                newSetting = newSetting | 0x1000;
            }
            else
            {
                newSetting = newSetting & 0xEFFF;
            }

            hasNewSetting = true;
        }

        private void radioTp1Falling_Click(object sender, EventArgs e)
        {
            radioTp1Falling.Checked = !radioTp1Falling.Checked;

            if (radioTp1Falling.Checked)
            {
                radioTp1Rising.Checked = false;
                newSetting = newSetting & 0xFFEF;
                newSetting = newSetting | 0x20;

            }
            else
            {
                newSetting = newSetting & 0xFFDF;
            }

            hasNewSetting = true;
        }

        private void radioTp2Falling_Click(object sender, EventArgs e)
        {
            radioTp2Falling.Checked = !radioTp2Falling.Checked;

            if (radioTp2Falling.Checked)
            {
                radioTp2Rising.Checked = false;
                newSetting = newSetting & 0xEFFF;
                newSetting = newSetting | 0x2000;
            }
            else
            {
                newSetting = newSetting & 0xDFFF;
            }

            hasNewSetting = true;
        }

        private void buttonStartHoming_Click(object sender, EventArgs e)
        {
            // 回歸原點後才可以使用 touch probe function
            bot.EvaluateScript("abort-program");
            bot.EvaluateScript("deploy 1 1 reset-fault 1 1 until-no-fault" +
               " 33 1 1 homing-method! hm 1 1 op-mode! until-no-requests 100 ms 1 1  drive-on 1 1 until-drive-on" +
              " 1 1 go 1 1 until-target-reached pp 1 1 op-mode! 1 1 until-no-requests 100 ms drive-off .( homing|Homing is Ok and change to PP mode)" +
             " ;deploy");
        }

        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            bot.EvaluateScript(textEvalute.Text);
            textEvalute.ResetText();
        }
    }
}
