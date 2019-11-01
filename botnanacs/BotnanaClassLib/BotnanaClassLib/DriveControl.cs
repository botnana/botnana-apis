using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using BotnanaLib;

namespace BotnanaClassLib
{
    public partial class DriveControl : UserControl
    {
        private Botnana botnana;
        private HandleTagNameMessage onECAlias;
        private HandleTagNameMessage onPDSState;
        private HandleTagNameMessage onSupportedMode;
        private HandleTagNameMessage onRealPosition;
        private HandleTagNameMessage onRealTorque;
        private HandleTagNameMessage onDigitalInput;
        private HandleTagNameMessage onControlWord;
        private HandleTagNameMessage onStatusWord;
        private HandleTagNameMessage onOperationMode;
        private HandleTagNameMessage onHomingMethod;
        private HandleTagNameMessage onHomingSpeed1;
        private HandleTagNameMessage onHomingSpeed2;
        private HandleTagNameMessage onHomingAcceleration;
        private HandleTagNameMessage onTargetPosition;
        private HandleTagNameMessage onTargetVelocity;
        private HandleTagNameMessage onProfileVelocity;
        private HandleTagNameMessage onProfileAcceleration;
        private HandleTagNameMessage onProfileDeceleration;
        private HandleTagNameMessage onTargetTorque;
        private HandleTagNameMessage onTorqueSlope;
        private UInt32 alias = 0;
        private UInt32 slaveNumber = 1;
        private UInt32 channelNumber = 1;
        private Int32 targetPos = 10000;
        private Int32 realPos = 0;
        private bool supportedOPModeUpdated = false;
        private delegate void Deg();

        public DriveControl()
        {
            InitializeComponent();
        }

        public void InitializeBotnana(Botnana bot)
        {
            botnana = bot;

            onECAlias = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                UInt32 a = UInt32.Parse(str);
                if (a != 0 && a == alias)
                {
                    slaveNumber = slv;
                    BeginInvoke(new Deg(() => textBoxSlavePos.Text = slv.ToString()));
                }
            });
            botnana.SetTagNameCB(@"ec_alias", 0, IntPtr.Zero, onECAlias);

            onPDSState = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxPDSState.Text = str));
            });
            botnana.SetTagNameCB(@"pds_state", 0, IntPtr.Zero, onPDSState);

            onSupportedMode = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber && !supportedOPModeUpdated)
                {
                    Int32 supportedMode;
                    try { supportedMode = Convert.ToInt32(str, 16); } catch (Exception exc) { Console.WriteLine(exc); return; }
                    List<string> modes = new List<string>();
                    if ((supportedMode & 0x1) != 0) modes.Add("PP");
                    if ((supportedMode & 0x4) != 0) modes.Add("PV");
                    if ((supportedMode & 0x8) != 0) modes.Add("PT");
                    if ((supportedMode & 0x20) != 0) modes.Add("HM");
                    if ((supportedMode & 0x80) != 0) modes.Add("CSP");
                    if ((supportedMode & 0x100) != 0) modes.Add("CSV");
                    if ((supportedMode & 0x200) != 0) modes.Add("CST");
                    BeginInvoke(new Deg(() => comboBoxOPMode.Items.AddRange(modes.Cast<object>().ToArray())));
                    supportedOPModeUpdated = true;
                }
            });
            botnana.SetTagNameCB(@"supported_drive_mode", 0, IntPtr.Zero, onSupportedMode);

            onRealPosition = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber)
                {
                    BeginInvoke(new Deg(() => textBoxRealPos.Text = str));
                    realPos = Int32.Parse(str);
                }
            });
            botnana.SetTagNameCB(@"real_position", 0, IntPtr.Zero, onRealPosition);

            onRealTorque = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxRealTorque.Text = str));
            });
            botnana.SetTagNameCB(@"real_torque", 0, IntPtr.Zero, onRealTorque);

            onDigitalInput = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxDigitalInput.Text = str));
            });
            botnana.SetTagNameCB(@"digital_inputs", 0, IntPtr.Zero, onDigitalInput);

            onControlWord = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxControlWord.Text = str));
            });
            botnana.SetTagNameCB(@"control_word", 0, IntPtr.Zero, onControlWord);

            onStatusWord = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxStatusWord.Text = str));
            });
            botnana.SetTagNameCB(@"status_word", 0, IntPtr.Zero, onStatusWord);

            onOperationMode = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber)
                {
                    string mode;
                    switch (UInt32.Parse(str))
                    {
                        case 1:
                            mode = @"PP";
                            break;
                        case 3:
                            mode = @"PV";
                            break;
                        case 4:
                            mode = @"PT";
                            break;
                        case 6:
                            mode = @"HM";
                            break;
                        case 8:
                            mode = @"CSP";
                            break;
                        case 9:
                            mode = @"CSV";
                            break;
                        case 10:
                            mode = @"CST";
                            break;
                        default:
                            return;
                    }
                    BeginInvoke(new Deg(() =>
                    {
                        // 不知為何 comboBoxOPMode 顯示的文字會有閃爍的問題，這裡設定兩次問題就消失了。
                        comboBoxOPMode.Text = mode;
                        comboBoxOPMode.Text = mode;
                    }));
                }

            });
            botnana.SetTagNameCB(@"operation_mode", 0, IntPtr.Zero, onOperationMode);

            onHomingMethod = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxHMMethod.Text = str));
            });
            botnana.SetTagNameCB(@"homing_method", 0, IntPtr.Zero, onHomingMethod);

            onHomingSpeed1 = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxHMSpeed1.Text = str));
            });
            botnana.SetTagNameCB(@"homing_speed_1", 0, IntPtr.Zero, onHomingSpeed1);

            onHomingSpeed2 = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxHMSpeed2.Text = str));
            });
            botnana.SetTagNameCB(@"homing_speed_2", 0, IntPtr.Zero, onHomingSpeed2);

            onHomingAcceleration = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxHMAcceleration.Text = str));
            });
            botnana.SetTagNameCB(@"homing_acceleration", 0, IntPtr.Zero, onHomingAcceleration);

            onTargetPosition = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber)
                {
                    BeginInvoke(new Deg(() => textBoxTargetPosition.Text = str));
                    targetPos = Int32.Parse(str);
                }
            });
            botnana.SetTagNameCB(@"target_position", 0, IntPtr.Zero, onTargetPosition);

            onTargetVelocity = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxTargetVelocity.Text = str));
            });
            botnana.SetTagNameCB(@"target_velocity", 0, IntPtr.Zero, onTargetVelocity);

            onProfileVelocity = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxProfileVelocity.Text = str));
            });
            botnana.SetTagNameCB(@"profile_velocity", 0, IntPtr.Zero, onProfileVelocity);

            onProfileAcceleration = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxProfileAcceleration.Text = str));
            });
            botnana.SetTagNameCB(@"profile_acceleration", 0, IntPtr.Zero, onProfileAcceleration);

            onProfileDeceleration = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxProfileDeceleration.Text = str));
            });
            botnana.SetTagNameCB(@"profile_deceleration", 0, IntPtr.Zero, onProfileDeceleration);

            onTargetTorque = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxTargetTorque.Text = str));
            });
            botnana.SetTagNameCB(@"target_torque", 0, IntPtr.Zero, onTargetTorque);

            onTorqueSlope = new HandleTagNameMessage((IntPtr _, UInt32 slv, UInt32 ch, string str) =>
            {
                if (slv == slaveNumber && ch == channelNumber) BeginInvoke(new Deg(() => textBoxTorqueSlope.Text = str));
            });
            botnana.SetTagNameCB(@"torque_slope", 0, IntPtr.Zero, onTorqueSlope);

            timer1.Interval = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (slaveNumber != 0 && channelNumber != 0) botnana.EvaluateScript(slaveNumber.ToString() + @" .slave-diff");
        }

        public void Awake()
        {
            timer1.Enabled = true;
            Reset();
            UpdateData();
        }

        public void Sleep()
        {
            timer1.Enabled = false;
            Reset();
        }

        private void UpdateData()
        {
            if (slaveNumber != 0)
            {
                botnana.EvaluateScript(slaveNumber.ToString() + @" .slave");
            }
            else if (alias != 0)
            {
                botnana.EvaluateScript(alias.ToString() + @" ec-a>n .slave");
            }
        }

        private void Reset()
        {
            supportedOPModeUpdated = false;
            textBoxPDSState.Text = "";
            textBoxRealPos.Text = "";
            textBoxRealTorque.Text = "";
            textBoxDigitalInput.Text = "";
            textBoxControlWord.Text = "";
            textBoxStatusWord.Text = "";
            comboBoxOPMode.Items.Clear();
            comboBoxOPMode.Text = "";
            textBoxHMMethod.Text = "";
            textBoxHMSpeed1.Text = "";
            textBoxHMSpeed2.Text = "";
            textBoxHMAcceleration.Text = "";
            textBoxTargetPosition.Text = "";
            textBoxTargetVelocity.Text = "";
            textBoxProfileAcceleration.Text = "";
            textBoxProfileDeceleration.Text = "";
            textBoxProfileVelocity.Text = "";
            textBoxTargetTorque.Text = "";
            textBoxTorqueSlope.Text = "";
        }

        private void TextBoxParaStore(object sender, string cmd)
        {
            botnana.EvaluateScript((sender as TextBox).Text + @" " + channelNumber.ToString() + @" " + slaveNumber.ToString() + @" " + cmd);
            botnana.EvaluateScript(slaveNumber.ToString() + @" .slave");
        }

        private void comboBoxOPMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (supportedOPModeUpdated)
            {
                string selectedText = (sender as ComboBox).Text;
                (sender as ComboBox).Text = "";
                string mode;
                switch (selectedText)
                {
                    case "PP":
                        mode = @"1";
                        break;
                    case "PV":
                        mode = @"3";
                        break;
                    case "PT":
                        mode = @"4";
                        break;
                    case "HM":
                        mode = @"6";
                        break;
                    case "CSP":
                        mode = @"8";
                        break;
                    case "CSV":
                        mode = @"9";
                        break;
                    case "CST":
                        mode = @"10";
                        break;
                    default:
                        return;
                }
                botnana.EvaluateScript(mode + @" " + channelNumber.ToString() + @" " + slaveNumber.ToString() + @" op-mode!");
            }
        }

        private void textBoxAlias_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxAlias_Submit(object sender)
        {
            TextBox tb = sender as TextBox;
            UInt32 n;
            if (UInt32.TryParse(tb.Text, out n))
            {
                alias = n;
                if (alias != 0)
                {
                    textBoxSlavePos.Text = "0";
                    slaveNumber = 0;
                    Reset();
                    UpdateData();
                }
            } else
            {
                tb.Text = alias.ToString();
            }
        }

        private void textBoxAlias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBoxAlias_Submit(sender);
        }

        private void textBoxAlias_Leave(object sender, EventArgs e)
        {
            textBoxAlias_Submit(sender);
        }

        private void textBoxSlavePos_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxSlavePos_Submit(object sender)
        {
            TextBox tb = sender as TextBox;
            UInt32 n;
            if (ParseCheck.UIntTryParseNotZero(tb.Text, out n) && n != 0 && alias == 0)
            {
                slaveNumber = n;
                Reset();
                UpdateData();
            } else
            {
                tb.Text = slaveNumber.ToString();
            }
        }

        private void textBoxSlavePos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBoxSlavePos_Submit(sender);
        }

        private void textBoxSlavePos_Leave(object sender, EventArgs e)
        {
            textBoxSlavePos_Submit(sender);
        }

        private void textBoxChannel_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxChannel_Submit(object sender)
        {
            TextBox tb = sender as TextBox;
            UInt32 n;
            if (ParseCheck.UIntTryParseNotZero(tb.Text, out n) && n != 0)
            {
                channelNumber = n;
                Reset();
                UpdateData();
            } else
            {
                tb.Text = channelNumber.ToString();
            }
        }

        private void textBoxChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBoxChannel_Submit(sender);
        }

        private void textBoxChannel_Leave(object sender, EventArgs e)
        {
            textBoxChannel_Submit(sender);
        }

        private void textBoxTargetPosition_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserInt(sender, Int32.TryParse);
        }

        private void textBoxTargetPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"target-p!");
        }

        private void textBoxTargetPosition_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"target-p!");
        }

        private void textBoxTargetVelocity_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserInt(sender, Int32.TryParse);
        }

        private void textBoxTargetVelocity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"target-v!");
        }

        private void textBoxTargetVelocity_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"target-v!");
        }

        private void textBoxTargetTorque_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserInt(sender, Int32.TryParse);
        }

        private void textBoxTargetTorque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"target-tq!");
        }

        private void textBoxTargetTorque_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"target-tq!");
        }

        private void textBoxHMMethod_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxHMMethod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"homing-method!");
        }

        private void textBoxHMMethod_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"homing-method!");
        }

        private void textBoxHMSpeed1_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxHMSpeed1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"homing-v1!");
        }

        private void textBoxHMSpeed1_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"homing-v1!");
        }

        private void textBoxHMSpeed2_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxHMSpeed2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"homing-v2!");
        }

        private void textBoxHMSpeed2_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"homing-v2!");
        }

        private void textBoxHMAcceleration_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxHMAcceleration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"homing-a!");
        }

        private void textBoxHMAcceleration_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"homing-a!");
        }

        private void textBoxProfileVelocity_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxProfileVelocity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"profile-v!");
        }

        private void textBoxProfileVelocity_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"profile-v!");
        }

        private void textBoxProfileAcceleration_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxProfileAcceleration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"profile-a1!");
        }

        private void textBoxProfileAcceleration_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"profile-a1!");
        }

        private void textBoxProfileDeceleration_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxProfileDeceleration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"profile-a2!");
        }

        private void textBoxProfileDeceleration_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"profile-a2!");
        }

        private void textBoxTorqueSlope_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
        }

        private void textBoxTorqueSlope_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxParaStore(sender, @"tq-slope!");
        }

        private void textBoxTorqueSlope_Leave(object sender, EventArgs e)
        {
            TextBoxParaStore(sender, @"tq-slope!");
        }

        private void buttonServoOn_Click(object sender, EventArgs e)
        {
            if (supportedOPModeUpdated && (Math.Abs(realPos - targetPos) < 1000 || comboBoxOPMode.Text != "CSP"))
            {
                botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" drive-on");
            } else
            {
                new Thread(() => System.Windows.Forms.MessageBox.Show("Following Error Too Large")).Start();
            }
        }

        private void buttonServoOff_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" drive-off");
        }

        private void buttonDriveHaltOn_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" +drive-halt");
        }

        private void buttonDriveHaltOff_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" -drive-halt");
        }

        private void buttonResetFault_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" reset-fault");
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" go");
        }

        private void buttonClearFollowingErr_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(channelNumber.ToString() + @" " + slaveNumber.ToString() + @" 2dup real-p@ -rot target-p!");
        }
    }
}
