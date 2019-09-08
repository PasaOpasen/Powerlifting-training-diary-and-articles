using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Контроль_прогресса
{
    public partial class Speed : Form
    {
        public Speed()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] w = textBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] st=textBox2.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < w.Length; i++)
            {
                Program.F2.textBox10.Text = w[i];
                Program.F2.numericUpDown4.Value = Convert.ToInt32(st[i]);
                Program.F2.button3_Click(new object(), new EventArgs());
            }

            this.Close();
        }
    }
}
