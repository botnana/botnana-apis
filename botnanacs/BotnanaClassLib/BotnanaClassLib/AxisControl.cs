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

        public AxisControl()
        {
            InitializeComponent();
            SetConfigurationReadOnly();
        }

        private void SetConfigurationReadOnly()
        {
            foreach (TextBox field in new[]
            {
                textBoxAxisName, textBoxHomeOffset, textBoxDriveAlias, textBoxDriveSlavePos,
                textBoxDriveChannel, textBoxEncoderPPU, textBoxEncoderDirection,
                textBoxExtEncoderPPU, textBoxExtEncoderDirection, textBoxExtEncoderAlias,
                textBoxExtEncoderSlavePos, textBoxExtEncoderChannel, textBoxCloseLoopFilter,
                textBoxMaxPosDeviation, textBoxIgnorableDistance, textBoxVmax, textBoxAmax,
                textBoxVff, textBoxVfactor, textBoxAff, textBoxAfactor
            })
            {
                field.ReadOnly = true;
            }
            comboBoxEncoderUnit.Enabled = false;
            tabPageAxisConfig.Text = "Configuration (read only)";
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
            UpdateConfig();
        }

        public void Sleep()
        {
            timer1.Enabled = false;
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axisNumber != 0) botnana.EvaluateScript(axisNumber.ToString() + @" .axis");
        }

        private void textBoxAxisNumberSubmit(object sender)
        {
            TextBox tb = sender as TextBox;
            UInt32 n;
            if (ParseCheck.UIntTryParseNotZero(tb.Text, out n))
            {
                axisNumber = n;
                Reset();
                UpdateConfig();
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
            ParseCheck.TextBoxCheckByParserUInt(sender, ParseCheck.UIntTryParseNotZero);
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
            ParseCheck.TextBoxCheckByParserDouble(sender, Double.TryParse);
        }

        private void textBoxTargetPos_TextChanged(object sender, EventArgs e)
        {
            ParseCheck.TextBoxCheckByParserDouble(sender, Double.TryParse);
        }
    }
}
