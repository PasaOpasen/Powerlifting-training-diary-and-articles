using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RMdator.Program;

namespace RMdator
{
    public partial class TF : Form
    {
        public TF()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
form.checkedListBox1.Items.Add(textBox1.Text,true);
                form.list.Add(textBox1.Text);
            }
            
            this.Close();
        }
    }
}
