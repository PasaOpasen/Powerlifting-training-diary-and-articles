using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace RMdator
{
    public partial class Form1 : Form
    {
       public List<string> list = new List<string>();
        public List<bool> boollist = new List<bool>();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Environment.CurrentDirectory;

            list.Add("(x,y,r) =");boollist.Add(true);
            list.Add("3D (x,y,r) ="); boollist.Add(true);
            list.Add("x ="); boollist.Add(true);
            list.Add("center = "); boollist.Add(true);
            list.Add("3D ur, uz(title , (x,y,r)"); boollist.Add(true);
            list.Add("uxw circle"); boollist.Add(false);

            checkedListBox1.Items.Clear();
            for(int i=0;i<list.Count;i++)
            checkedListBox1.Items.Add(list[i], true);

            checkedListBox1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TF().ShowDialog();
        }

        private string[] GetNames()
        {
            var ind = checkedListBox1.CheckedIndices;
            string[] st = new string[ind.Count];
            for (int i = 0; i < st.Length; i++)
                st[i] = checkedListBox1.Items[ind[i]].ToString();
            return st;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var st = GetNames();

            string[] all =(checkBox1.Checked)? Directory.GetFiles(textBox1.Text, "*.*", System.IO.SearchOption.AllDirectories): Directory.GetFiles(textBox1.Text);

            for (int i = 0; i < all.Length; i++)
                for (int k = 0; k < st.Length; k++)
                    if (Path.GetFileName(all[i]).StartsWith(st[k]))
                    {
                        File.Delete(all[i]);
                        break;
                    }

            all = Directory.GetDirectories(textBox1.Text);
            string tmp;
            for (int i = 0; i < all.Length; i++)
            {
                tmp = all[i].Substring(Path.GetDirectoryName(all[i]).Length + 1);
                for (int k = 0; k < st.Length; k++)
                    if (tmp.StartsWith(st[k]))
                    {
                        Directory.Delete(all[i],true);
                        break;
                    }
            }


            this.Close();
        }
    }
}
