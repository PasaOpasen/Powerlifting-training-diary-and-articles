using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Библиотека_классов;

namespace Создать_множество_директорий
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a, b, step;
            a = Convert.ToInt32(textBox1.Text);
           b = Convert.ToInt32(textBox2.Text);
            step= Convert.ToInt32(textBox3.Text);

            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string s = openFileDialog1.SelectedPath;
                MakeDirectories.Make(a, b, s, step);
               // this.Close();
            }
            }
    }
}
