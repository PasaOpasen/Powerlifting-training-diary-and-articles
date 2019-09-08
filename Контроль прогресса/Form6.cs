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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            Color col = Color.FromArgb(200, Color.Transparent);
            for (int i = 1; i <= 7; i++)
                this.Controls["label" + i.ToString()].BackColor = col;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label9.Text = "Узнать";
            label8.Text = "Узнать";
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1_ValueChanged(sender, e);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                double weight = Convert.ToDouble(textBox1.Text);
                int set = (int)numericUpDown1.Value;
                int nset = (int)numericUpDown2.Value;
                string txt = XPM(PM(weight, set), nset).ToString();
                if (txt.Length >= 6) txt = txt.Substring(0, 6);
                label8.Text = txt; 
            }
            catch { label8.Text = error; }
        }
        string error = "Ошибка";

        private void label9_Click(object sender, EventArgs e)
        {
            try
            {
                double weight = Convert.ToDouble(textBox1.Text);
                int set = (int)numericUpDown1.Value;
                double nw = Convert.ToDouble(textBox2.Text);
                string txt=Set(PM(weight, set), nw).ToString();
                if (txt.Length >= 6) txt = txt.Substring(0, 6);
                label9.Text =txt ;
            }
            catch { label9.Text = error; }
        }
    }
}
