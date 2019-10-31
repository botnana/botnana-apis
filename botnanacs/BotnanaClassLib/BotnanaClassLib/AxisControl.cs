using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotnanaLib;

namespace BotnanaClassLib
{
    public partial class AxisControl : UserControl
    {
        private Botnana botnana;
        private HandleTagNameMessage onAxisName;
        private HandleTagNameMessage onAxisHomeOffset;
        private HandleTagNameMessage onEncoderLengthUnit;
        private HandleTagNameMessage onEncoderPPU;
        private HandleTagNameMessage onEncoderDirection;
        private HandleTagNameMessage onCloseLoopFilter;
        private HandleTagNameMessage onMaxPositionDeviation;
        private HandleTagNameMessage onDriveAlias;
        private HandleTagNameMessage onDriveSlavePosition;
        private HandleTagNameMessage onDriveChannel;
        private HandleTagNameMessage onExtEncoderPPU;
        private HandleTagNameMessage onExtEncoderDirection;
        private HandleTagNameMessage onExtEncoderAlias;
        private HandleTagNameMessage onExtEncoderSlavePosition;
        private HandleTagNameMessage onExtEncoderChannel;
        private HandleTagNameMessage onAxisAmax;
        private HandleTagNameMessage onAxisVmax;
        private HandleTagNameMessage onAxisIgnorableDistance;
        private HandleTagNameMessage onAxisVff;
        private HandleTagNameMessage onAxisVfactor;
        private HandleTagNameMessage onAxisAff;
        private HandleTagNameMessage onAxisAfactor;
        private HandleTagNameMessage onAxisDenamdPos;
        private HandleTagNameMessage onAxisEncoderPos;
        private HandleTagNameMessage onAxisFeedbackPos;
        private HandleTagNameMessage onAxisFollowingError;
        private UInt32 axisNumber = 1;
        private delegate void Deg();
        private delegate bool ParseFunc<T1, T2>(T1 a, out T2 b);

        private bool UIntTryParseNotZero(string str, out UInt32 n)
        {
            return (UInt32.TryParse(str, out n) && n != 0) ? true : false;
        }

        private bool IntTryParseNotZero(string str, out Int32 n)
        {
            return (Int32.TryParse(str, out n) && n != 0) ? true : false;
        }

        private bool DoubleTryParseNotNeg(string str, out double n)
        {
            return (Double.TryParse(str, out n) && n >= 0.0) ? true : false;
        }

        private bool DoubleTryParsePos(string str, out double n)
        {
            return (Double.TryParse(str, out n) && n > 0.0) ? true : false;
        }

        private void CheckTextBoxByParserUInt(object sender, ParseFunc<string, UInt32> parser)
        {
            TextBox tb = sender as TextBox;
            if (parser(tb.Text, out _)) { tb.ForeColor = Color.Black; } else { tb.ForeColor = Color.Red; }
        }

        private void CheckTextBoxByParserInt(object sender, ParseFunc<string, Int32> parser)
        {
            TextBox tb = sender as TextBox;
            if (parser(tb.Text, out _)) { tb.ForeColor = Color.Black; } else { tb.ForeColor = Color.Red; }
        }

        private void CheckTextBoxByParserDouble(object sender, ParseFunc<string, Double> parser)
        {
            TextBox tb = sender as TextBox;
            if (parser(tb.Text, out _)) { tb.ForeColor = Color.Black; } else { tb.ForeColor = Color.Red; }
        }
        
        private void StrSetParamByParserUInt(string value, string cmd, Action<uint, int> configset, ParseFunc<string, UInt32> parser)
        {
            UInt32 val;
            if (parser(value, out val))
            {
                botnana.EvaluateScript(value + @" " + axisNumber.ToString() + @" " + cmd);
                configset(axisNumber, (Int32)val);
            }
            botnana.EvaluateScript(axisNumber.ToString() + @" .axiscfg");
            botnana.ConfigAxisGet(axisNumber);
        }

        private void StrSetParamByParserInt(string value, string cmd, Action<uint, int> configset, ParseFunc<string, Int32> parser)
        {
            Int32 val;
            if (parser(value, out val))
            {
                botnana.EvaluateScript(value + @" " + axisNumber.ToString() + @" " + cmd);
                configset(axisNumber, val);
            }
            botnana.EvaluateScript(axisNumber.ToString() + @" .axiscfg");
            botnana.ConfigAxisGet(axisNumber);
        }

        private void StrSetParamByParserDouble(string value, string cmd, Action<uint, double> configset, ParseFunc<string, Double> parser)
        {
            Double val;
            if (parser(value, out val))
            {
                botnana.EvaluateScript(value + @"e " + axisNumber.ToString() + @" " + cmd);
                configset(axisNumber, val);
            }
            botnana.EvaluateScript(axisNumber.ToString() + @" .axiscfg");
            botnana.ConfigAxisGet(axisNumber);
        }
        
        public AxisControl()
        {
            InitializeComponent();
        }

        public void InitializeBotnana(Botnana bot)
        {
            botnana = bot;

            onAxisName = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxAxisName.Text = str; }));
            });
            botnana.SetTagNameCB(@"config_axis_name", 0, IntPtr.Zero, onAxisName);

            onAxisHomeOffset = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxHomeOffset.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_home_offset", 0, IntPtr.Zero, onAxisHomeOffset);

            onEncoderLengthUnit = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) comboBoxEncoderUnit.Text = str; }));
            });
            botnana.SetTagNameCB(@"encoder_length_unit", 0, IntPtr.Zero, onEncoderLengthUnit);

            onEncoderPPU = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxEncoderPPU.Text = str; }));
            });
            botnana.SetTagNameCB(@"encoder_ppu", 0, IntPtr.Zero, onEncoderPPU);

            onEncoderDirection = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxEncoderDirection.Text = str; }));
            });
            botnana.SetTagNameCB(@"encoder_direction", 0, IntPtr.Zero, onEncoderDirection);

            onCloseLoopFilter = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxCloseLoopFilter.Text = str; }));
            });
            botnana.SetTagNameCB(@"closed_loop_filter", 0, IntPtr.Zero, onCloseLoopFilter);

            onMaxPositionDeviation = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxMaxPosDeviation.Text = str; }));
            });
            botnana.SetTagNameCB(@"max_position_deviation", 0, IntPtr.Zero, onMaxPositionDeviation);

            onDriveAlias = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxDriveAlias.Text = str; }));
            });
            botnana.SetTagNameCB(@"drive_alias", 0, IntPtr.Zero, onDriveAlias);

            onDriveSlavePosition = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxDriveSlavePos.Text = str; }));
            });
            botnana.SetTagNameCB(@"drive_slave_position", 0, IntPtr.Zero, onDriveSlavePosition);

            onDriveChannel = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxDriveChannel.Text = str; }));
            });
            botnana.SetTagNameCB(@"drive_channel", 0, IntPtr.Zero, onDriveChannel);

            onExtEncoderPPU = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxExtEncoderPPU.Text = str; }));
            });
            botnana.SetTagNameCB(@"ext_encoder_ppu", 0, IntPtr.Zero, onExtEncoderPPU);

            onExtEncoderDirection = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxExtEncoderDirection.Text = str; }));
            });
            botnana.SetTagNameCB(@"ext_encoder_direction", 0, IntPtr.Zero, onExtEncoderDirection);

            onExtEncoderAlias = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxExtEncoderAlias.Text = str; }));
            });
            botnana.SetTagNameCB(@"ext_encoder_alias", 0, IntPtr.Zero, onExtEncoderAlias);

            onExtEncoderSlavePosition = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxExtEncoderSlavePos.Text = str; }));
            });
            botnana.SetTagNameCB(@"ext_encoder_slave_position", 0, IntPtr.Zero, onExtEncoderSlavePosition);

            onExtEncoderChannel = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxExtEncoderChannel.Text = str; }));
            });
            botnana.SetTagNameCB(@"ext_encoder_channel", 0, IntPtr.Zero, onExtEncoderChannel);

            onAxisAmax = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxAmax.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_amax", 0, IntPtr.Zero, onAxisAmax);

            onAxisVmax = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxVmax.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_vmax", 0, IntPtr.Zero, onAxisVmax);

            onAxisIgnorableDistance = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxIgnorableDistance.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_ignorable_distance", 0, IntPtr.Zero, onAxisIgnorableDistance);

            onAxisVff = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxVff.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_vff", 0, IntPtr.Zero, onAxisVff);

            onAxisVfactor = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxVfactor.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_vfactor", 0, IntPtr.Zero, onAxisVfactor);

            onAxisAff = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxAff.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_aff", 0, IntPtr.Zero, onAxisAff);

            onAxisAfactor = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxAfactor.Text = str; }));
            });
            botnana.SetTagNameCB(@"axis_afactor", 0, IntPtr.Zero, onAxisAfactor);

            onAxisDenamdPos = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                double ppu;
                double pos;
                if (!double.TryParse(textBoxEncoderPPU.Text, out ppu)) ppu = 0.0;
                if (!double.TryParse(str, out pos)) pos = 0.0;
                BeginInvoke(new Deg(() =>
                {
                    if (ax == axisNumber)
                    {
                        textBoxDemandPos.Text = str;
                        textBoxOutputPulse.Text = (pos * ppu).ToString();
                    }
                }));
            });
            botnana.SetTagNameCB(@"axis_demand_position", 0, IntPtr.Zero, onAxisDenamdPos);

            onAxisEncoderPos = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxEncoderPos.Text = str; }));
            });
            botnana.SetTagNameCB(@"encoder_position", 0, IntPtr.Zero, onAxisEncoderPos);

            onAxisFeedbackPos = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxFeedbackPos.Text = str; }));
            });
            botnana.SetTagNameCB(@"feedback_position", 0, IntPtr.Zero, onAxisFeedbackPos);

            onAxisFollowingError = new HandleTagNameMessage((IntPtr _, UInt32 ax, UInt32 __, string str) =>
            {
                BeginInvoke(new Deg(() => { if (ax == axisNumber) textBoxFollowingError.Text = str; }));
            });
            botnana.SetTagNameCB(@"following_error", 0, IntPtr.Zero, onAxisFollowingError);

            timer1.Interval = 100;
        }

        public void Awake()
        {
            timer1.Enabled = true;
            Reset();
        }

        private void UpdateConfig()
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" .axiscfg");
            botnana.ConfigAxisGet(axisNumber);
        }

        private void Reset()
        {
            textBoxAxisName.Text = "";
            textBoxDriveAlias.Text = "";
            textBoxDriveSlavePos.Text = "";
            textBoxDriveChannel.Text = "";
            textBoxHomeOffset.Text = "";
            comboBoxEncoderUnit.Text = "";
            textBoxEncoderPPU.Text = "";
            textBoxEncoderDirection.Text = "";
            textBoxExtEncoderPPU.Text = "";
            textBoxExtEncoderDirection.Text = "";
            textBoxExtEncoderAlias.Text = "";
            textBoxExtEncoderSlavePos.Text = "";
            textBoxExtEncoderChannel.Text = "";
            textBoxCloseLoopFilter.Text = "";
            textBoxMaxPosDeviation.Text = "";
            textBoxIgnorableDistance.Text = "";
            textBoxVmax.Text = "";
            textBoxAmax.Text = "";
            textBoxVff.Text = "";
            textBoxVfactor.Text = "";
            textBoxAff.Text = "";
            textBoxAfactor.Text = "";
            textBoxDemandPos.Text = "";
            textBoxEncoderPos.Text = "";
            textBoxFeedbackPos.Text = "";
            textBoxFollowingError.Text = "";
            textBoxOutputPulse.Text = "";
            UpdateConfig();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axisNumber != 0) botnana.EvaluateScript(axisNumber.ToString() + @" .axis");
        }

        private void textBoxAxisNumberSubmit(object sender)
        {
            TextBox tb = sender as TextBox;
            UInt32 n;
            if (UIntTryParseNotZero(tb.Text, out n))
            {
                axisNumber = n;
                Reset();
            }
            else
            {
                tb.Text = axisNumber.ToString();
            }
        }

        private void textBoxAxisNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBoxAxisNumberSubmit(sender);
        }

        private void textBoxAxisNumber_Leave(object sender, EventArgs e)
        {
            textBoxAxisNumberSubmit(sender);
        }

        private void textBoxAxisNumber_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UIntTryParseNotZero);
        }

        private void textBoxAxisName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                botnana.ConfigAxisSetName(axisNumber, (sender as TextBox).Text);
                UpdateConfig();
            }
        }

        private void textBoxAxisName_Leave(object sender, EventArgs e)
        {
            botnana.ConfigAxisSetName(axisNumber, (sender as TextBox).Text);
            UpdateConfig();
        }

        private void textBoxDriveAlias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserUInt((sender as TextBox).Text, @"drv-alias!", botnana.ConfigAxisSetDriveAlias, UInt32.TryParse);
        }

        private void textBoxDriveAlias_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserUInt((sender as TextBox).Text, @"drv-alias!", botnana.ConfigAxisSetDriveAlias, UInt32.TryParse);
        }

        private void textBoxDriveAlias_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxDriveSlavePos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserUInt((sender as TextBox).Text, @"drv-slave!", botnana.ConfigAxisSetDriveSlavePosition, UInt32.TryParse);
        }

        private void textBoxDriveSlavePos_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserUInt((sender as TextBox).Text, @"drv-slave!", botnana.ConfigAxisSetDriveSlavePosition, UInt32.TryParse);
        }

        private void textBoxDriveSlavePos_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxDriveChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserUInt((sender as TextBox).Text, @"drv-channel!", botnana.ConfigAxisSetDriveChannel, UInt32.TryParse);
        }

        private void textBoxDriveChannel_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserUInt((sender as TextBox).Text, @"drv-channel!", botnana.ConfigAxisSetDriveChannel, UInt32.TryParse);
        }

        private void textBoxDriveChannel_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxHomeOffset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"hmofs!", botnana.ConfigAxisSetHomeOffset, Double.TryParse);
        }

        private void textBoxHomeOffset_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"hmofs!", botnana.ConfigAxisSetHomeOffset, Double.TryParse);
        }

        private void textBoxHomeOffset_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }

        private void comboBoxEncoderUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unit;
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    unit = @"0";
                    botnana.ConfigAxisSetEncoderLengthUnitAsMeter(axisNumber);
                    break;
                case 1:
                    unit = @"1";
                    botnana.ConfigAxisSetEncoderLengthUnitAsRevolution(axisNumber);
                    break;
                case 2:
                    unit = @"2";
                    botnana.ConfigAxisSetEncoderLengthUnitAsPulse(axisNumber);
                    break;
                case 3:
                    unit = @"3";
                    // TODO: botnana.ConfigAxisSetEncoderLengthUnitAsUserDefine(axisNumber);
                    break;
                default:
                    return;
            }
            botnana.EvaluateScript(unit + @" " + axisNumber.ToString() + @" enc-u!");
            UpdateConfig();
        }

        private void textBoxEncoderPPU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"enc-ppu!", botnana.ConfigAxisSetEncoderPPU, DoubleTryParsePos);
        }

        private void textBoxEncoderPPU_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"enc-ppu!", botnana.ConfigAxisSetEncoderPPU, DoubleTryParsePos);
        }

        private void textBoxEncoderPPU_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParsePos);
        }

        private void textBoxEncoderDirection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserInt((sender as TextBox).Text, @"enc-dir!", botnana.ConfigAxisSetEncoderDirection, IntTryParseNotZero);
        }

        private void textBoxEncoderDirection_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserInt((sender as TextBox).Text, @"enc-dir!", botnana.ConfigAxisSetEncoderDirection, IntTryParseNotZero);
        }

        private void textBoxEncoderDirection_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserInt(sender, IntTryParseNotZero);
        }

        private void textBoxExtEncoderPPU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"ext-enc-ppu!", botnana.ConfigAxisSetExtEncoderPPU, DoubleTryParsePos);
        }

        private void textBoxExtEncoderPPU_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"ext-enc-ppu!", botnana.ConfigAxisSetExtEncoderPPU, DoubleTryParsePos);
        }

        private void textBoxExtEncoderPPU_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParsePos);
        }

        private void textBoxExtEncoderDirection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserInt((sender as TextBox).Text, @"ext-enc-dir!", botnana.ConfigAxisSetExtEncoderDirection, IntTryParseNotZero);
        }

        private void textBoxExtEncoderDirection_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserInt((sender as TextBox).Text, @"ext-enc-dir!", botnana.ConfigAxisSetExtEncoderDirection, IntTryParseNotZero);
        }

        private void textBoxExtEncoderDirection_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserInt(sender, IntTryParseNotZero);
        }

        private void textBoxExtEncoderAlias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserUInt((sender as TextBox).Text, @"ext-enc-alias!", botnana.ConfigAxisSetExtEncoderAlias, UInt32.TryParse);
        }

        private void textBoxExtEncoderAlias_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserUInt((sender as TextBox).Text, @"ext-enc-alias!", botnana.ConfigAxisSetExtEncoderAlias, UInt32.TryParse);
        }

        private void textBoxExtEncoderAlias_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxExtEncoderSlavePos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserUInt((sender as TextBox).Text, @"ext-enc-slave!", botnana.ConfigAxisSetExtEncoderSlavePosition, UInt32.TryParse);
        }

        private void textBoxExtEncoderSlavePos_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserUInt((sender as TextBox).Text, @"ext-enc-slave!", botnana.ConfigAxisSetExtEncoderSlavePosition, UInt32.TryParse);
        }

        private void textBoxExtEncoderSlavePos_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxExtEncoderChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserUInt((sender as TextBox).Text, @"ext-enc-channel!", botnana.ConfigAxisSetExtEncoderChannel, UInt32.TryParse);
        }

        private void textBoxExtEncoderChannel_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserUInt((sender as TextBox).Text, @"ext-enc-channel!", botnana.ConfigAxisSetExtEncoderChannel, UInt32.TryParse);
        }

        private void textBoxExtEncoderChannel_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UInt32.TryParse);
        }

        private void textBoxCloseLoopFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"cl-cutoff!", botnana.ConfigAxisSetClosedLoopFilter, DoubleTryParseNotNeg);
        }

        private void textBoxCloseLoopFilter_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"cl-cutoff!", botnana.ConfigAxisSetClosedLoopFilter, DoubleTryParseNotNeg);
        }

        private void textBoxCloseLoopFilter_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParseNotNeg);
        }

        private void textBoxMaxPosDeviation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"max-pos-dev!", botnana.ConfigAxisSetMaxPositionDeviation, DoubleTryParseNotNeg);
        }

        private void textBoxMaxPosDeviation_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"max-pos-dev!", botnana.ConfigAxisSetMaxPositionDeviation, DoubleTryParseNotNeg);
        }

        private void textBoxMaxPosDeviation_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParseNotNeg);
        }

        private void textBoxIgnorableDistance_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: botnana.ConfigAxisSetIgnorableDistance;
            if (e.KeyCode == Keys.Enter)
            {
                botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-ignore-dist!");
                UpdateConfig();
            }
        }

        private void textBoxIgnorableDistance_Leave(object sender, EventArgs e)
        {
            // TODO: botnana.ConfigAxisSetIgnorableDistance;
            botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-ignore-dist!");
            UpdateConfig();
        }

        private void textBoxIgnorableDistance_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParsePos);
        }

        private void textBoxVmax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"axis-vmax!", botnana.ConfigAxisSetVmax, DoubleTryParsePos);
        }

        private void textBoxVmax_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"axis-vmax!", botnana.ConfigAxisSetVmax, DoubleTryParsePos);
        }

        private void textBoxVmax_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParsePos);
        }

        private void textBoxAmax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) StrSetParamByParserDouble((sender as TextBox).Text, @"axis-amax!", botnana.ConfigAxisSetAmax, DoubleTryParsePos);
        }

        private void textBoxAmax_Leave(object sender, EventArgs e)
        {
            StrSetParamByParserDouble((sender as TextBox).Text, @"axis-amax!", botnana.ConfigAxisSetAmax, DoubleTryParsePos);
        }

        private void textBoxAmax_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, DoubleTryParsePos);
        }

        private void textBoxVff_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: botnana.ConfigAxisSetVff;
            if (e.KeyCode == Keys.Enter)
            {
                botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-vff!");
                UpdateConfig();
            }
        }

        private void textBoxVff_Leave(object sender, EventArgs e)
        {
            // TODO: botnana.ConfigAxisSetVff;
            botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-vff!");
            UpdateConfig();
        }

        private void textBoxVff_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }

        private void textBoxVfactor_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: botnana.ConfigAxisSetVfactor;
            if (e.KeyCode == Keys.Enter)
            {
                botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-vfactor!");
                UpdateConfig();
            }
        }

        private void textBoxVfactor_Leave(object sender, EventArgs e)
        {
            // TODO: botnana.ConfigAxisSetVfactor;
            botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-vfactor!");
            UpdateConfig();
        }

        private void textBoxVfactor_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }

        private void textBoxAff_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: botnana.ConfigAxisSetAff;
            if (e.KeyCode == Keys.Enter)
            {
                botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-aff!");
                UpdateConfig();
            }
        }

        private void textBoxAff_Leave(object sender, EventArgs e)
        {
            // TODO: botnana.ConfigAxisSetAff;
            botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-aff!");
            UpdateConfig();
        }

        private void textBoxAff_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }

        private void textBoxAfactor_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: botnana.ConfigAxisSetAfactor;
            if (e.KeyCode == Keys.Enter)
            {
                botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-afactor!");
                UpdateConfig();
            }
        }

        private void textBoxAfactor_Leave(object sender, EventArgs e)
        {
            // TODO: botnana.ConfigAxisSetAfactor;
            botnana.EvaluateScript((sender as TextBox).Text + @"e " + axisNumber.ToString() + @" axis-afactor!");
            UpdateConfig();
        }

        private void textBoxAfactor_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            botnana.ConfigSave();
        }

        private void buttonJogP_MouseDown(object sender, MouseEventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" 2147481647e " + textBoxVelocityLimit.Text + @"e dup interpolator-v! dup +interpolator axis-cmd-p!");
        }

        private void buttonJogP_MouseUp(object sender, MouseEventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" -interpolator");
        }

        private void buttonJogN_MouseDown(object sender, MouseEventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" -2147481647e " + textBoxVelocityLimit.Text + @"e dup interpolator-v! dup +interpolator axis-cmd-p!");
        }

        private void buttonJogN_MouseUp(object sender, MouseEventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" -interpolator");
        }

        private void buttonJog_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" " + textBoxTargetPos.Text + @"e " + textBoxVelocityLimit.Text + @"e dup interpolator-v! dup +interpolator axis-cmd-p!");
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" -interpolator");
        }

        private void buttonClearFollowingErr_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(axisNumber.ToString() + @" 0axis-ferr");
        }

        private void textBoxVelocityLimit_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }

        private void textBoxTargetPos_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserDouble(sender, Double.TryParse);
        }
    }
}
