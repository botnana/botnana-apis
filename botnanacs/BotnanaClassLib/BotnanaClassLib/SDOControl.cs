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
    public partial class SDOControl : UserControl
    {
        private Botnana botnana;
        private HandleTagNameMessage onSDOIndex;
        private HandleTagNameMessage onSDOSubIndex;
        private HandleTagNameMessage onSDOError;
        private HandleTagNameMessage onSDOBusy;
        private HandleTagNameMessage onSDOData;
        private UInt32 slaveNumber = 1;
        private delegate void Deg();
        private delegate bool ParseFunc<T1, T2>(T1 a, out T2 b);

        private bool UIntTryParseNotZero(string str, out UInt32 n)
        {
            return (UInt32.TryParse(str, out n) && n != 0) ? true : false;
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

        private void CheckTextBoxUIntHex(object sender)
        {
            TextBox tb = sender as TextBox;
            try
            {
                Int32 _ = Convert.ToInt32(tb.Text, 16);
                tb.ForeColor = Color.Black;
            }
            catch
            {
                tb.ForeColor = Color.Red;
            }
        }

        public SDOControl()
        {
            InitializeComponent();
        }

        public void InitializeBotnana(Botnana bot)
        {
            botnana = bot;
            onSDOIndex = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                BeginInvoke(new Deg(() => { if (slv == slaveNumber) textBoxSDOIndexS.Text = str; }));
            });
            botnana.SetTagNameCB(@"sdo_index", 0, IntPtr.Zero, onSDOIndex);

            onSDOSubIndex = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                BeginInvoke(new Deg(() => { if (slv == slaveNumber) textBoxSDOSubIndexS.Text = str; }));
            });
            botnana.SetTagNameCB(@"sdo_subindex", 0, IntPtr.Zero, onSDOSubIndex);

            onSDOError = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                BeginInvoke(new Deg(() => { if (slv == slaveNumber) textBoxSDOErrorS.Text = str; }));
            });
            botnana.SetTagNameCB(@"sdo_error", 0, IntPtr.Zero, onSDOError);

            onSDOBusy = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                BeginInvoke(new Deg(() => { if (slv == slaveNumber) textBoxSDOBusyS.Text = str; }));
                if (slv == slaveNumber && str == "true") UpdateData();
            });
            botnana.SetTagNameCB(@"sdo_busy", 0, IntPtr.Zero, onSDOBusy);

            onSDOData = new HandleTagNameMessage((IntPtr dataPtr, UInt32 slv, UInt32 ch, string str) =>
            {
                BeginInvoke(new Deg(() => { if (slv == slaveNumber) textBoxSDOValueS.Text = str; }));
            });
            botnana.SetTagNameCB(@"sdo_data", 0, IntPtr.Zero, onSDOData);
        }

        public void UpdateData()
        {
            botnana.EvaluateScript(slaveNumber.ToString() + @" .sdo");
        }

        private void Reset()
        {
            textBoxSDOIndexS.Text = "--";
            textBoxSDOSubIndexS.Text = "--";
            textBoxSDOBusyS.Text = "--";
            textBoxSDOErrorS.Text = "--";
            textBoxSDOValueS.Text = "--";
        }

        private void buttonSDOExecute_Click(object sender, EventArgs e)
        {
            string cmd;
            bool upload;
            switch (comboBoxSDOOperation.SelectedIndex)
            {
                case 0:
                    cmd = "sdo-upload-u8";
                    upload = true;
                    break;
                case 1:
                    cmd = "sdo-upload-u16";
                    upload = true;
                    break;
                case 2:
                    cmd = "sdo-upload-u32";
                    upload = true;
                    break;
                case 3:
                    cmd = "sdo-upload-i8";
                    upload = true;
                    break;
                case 4:
                    cmd = "sdo-upload-i16";
                    upload = true;
                    break;
                case 5:
                    cmd = "sdo-upload-i32";
                    upload = true;
                    break;
                case 6:
                    cmd = "sdo-download-u8";
                    upload = false;
                    break;
                case 7:
                    cmd = "sdo-download-u16";
                    upload = false;
                    break;
                case 8:
                    cmd = "sdo-download-u32";
                    upload = false;
                    break;
                case 9:
                    cmd = "sdo-download-i8";
                    upload = false;
                    break;
                case 10:
                    cmd = "sdo-download-i16";
                    upload = false;
                    break;
                case 11:
                    cmd = "sdo-download-i32";
                    upload = false;
                    break;
                default:
                    return;
            }
            try
            {
                if (upload)
                {
                    botnana.EvaluateScript(Convert.ToInt32(textBoxSDOSubIndexC.Text, 16).ToString() + @" " + Convert.ToInt32(textBoxSDOIndexC.Text, 16).ToString() + @" " + slaveNumber.ToString() + @" " + cmd);
                }
                else
                {
                    botnana.EvaluateScript(textBoxSDOValueC.Text + @" " + Convert.ToInt32(textBoxSDOSubIndexC.Text, 16).ToString() + @" " + Convert.ToInt32(textBoxSDOIndexC.Text, 16).ToString() + @" " + slaveNumber.ToString() + @" " + cmd);
                }
                UpdateData();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        private void textBoxSDOSlaveNumber_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserUInt(sender, UIntTryParseNotZero);
        }

        private void textBoxSDOSlaveNumberSubmit(object sender)
        {
            TextBox tb = sender as TextBox;
            UInt32 n;
            if (UIntTryParseNotZero(tb.Text, out n))
            {
                slaveNumber = n;
                Reset();
                UpdateData();
            } else
            {
                tb.Text = slaveNumber.ToString();
            }
        }

        private void textBoxSDOSlaveNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) textBoxSDOSlaveNumberSubmit(sender);
        }

        private void textBoxSDOSlaveNumber_Leave(object sender, EventArgs e)
        {
            textBoxSDOSlaveNumberSubmit(sender);
        }

        private void textBoxSDOIndexC_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxUIntHex(sender);
        }

        private void textBoxSDOSubIndexC_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxUIntHex(sender);
        }

        private void textBoxSDOValueC_TextChanged(object sender, EventArgs e)
        {
            CheckTextBoxByParserInt(sender, Int32.TryParse);
        }

        private void buttonECSave_Click(object sender, EventArgs e)
        {
            botnana.EvaluateScript(slaveNumber.ToString() + @" ec-save");
        }
    }
}
