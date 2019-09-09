using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Библиотека_классов.PowerLift;

namespace Контроль_прогресса
{
    public partial class FastVal : Form
    {
        public FastVal()
        {
            InitializeComponent();
            Color col = Color.FromArgb(200, Color.Transparent);
            for (int i = 1; i <= 7; i++)
                this.Controls["label" + i.ToString()].BackColor = col;

            numericUpDown1_ValueChanged(new object(), new EventArgs());

            textBox1.TextChanged += (object o, EventArgs e) => numericUpDown1_ValueChanged(o, e);
            textBox2.TextChanged += (object o, EventArgs e) => numericUpDown1_ValueChanged(o, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SetLabels();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1_ValueChanged(sender, e);
        }

        private void SetLabels()
        {
            double weight = Convert.ToDouble(textBox1.Text);
            int set = (int)numericUpDown1.Value;
            int nset = (int)numericUpDown2.Value;
            double tmp = PM(weight, set);
            string txt = Math.Round(XPM(tmp, nset), 2).ToString();
            label8.Text = txt;

            double nw = Convert.ToDouble(textBox2.Text);
            txt = Math.Round(Set(tmp, nw), 1).ToString();
            label9.Text = txt;
        }
    }
}
