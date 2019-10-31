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
    public partial class RealTimeScriptControl : UserControl
    {
        private Botnana botnana;

        public RealTimeScriptControl()
        {
            InitializeComponent();
        }

        public void InitializeBotnana(Botnana bot)
        {
            botnana = bot;
        }

        private bool preventEvaluate = false;
        private void textBoxScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) preventEvaluate = true;
        }

        private void textBoxScript_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                preventEvaluate = false;
            }
            else if (e.KeyCode == Keys.Enter && !preventEvaluate)
            {
                botnana.EvaluateScript((sender as TextBox).Text);
                (sender as TextBox).Text = "";
            }
        }
    }
}
