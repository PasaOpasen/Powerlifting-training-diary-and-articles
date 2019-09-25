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

            FillList();

            checkedListBox1.Items.Clear();
            for (int i = 0; i < list.Count; i++)
                checkedListBox1.Items.Add(list[i], boollist[i]);

            checkedListBox1.Update();
        }
        private void FillList()
        {
            list.Add("(x,y,r) ="); boollist.Add(true);
            list.Add("3D (x,y,r) ="); boollist.Add(true);
            list.Add("x ="); boollist.Add(true);
            list.Add("center = "); boollist.Add(true);
            list.Add("3D ur, uz(title ,"); boollist.Add(true);
            list.Add("OnePoint("); boollist.Add(true);
            list.Add("urAbs"); boollist.Add(true);
            list.Add("urIm"); boollist.Add(true);
            list.Add("urRe"); boollist.Add(true);
            list.Add("uzAbs"); boollist.Add(true);
            list.Add("uzIm"); boollist.Add(true);
            list.Add("uzRe"); boollist.Add(true);
        
            list.Add("uxw circle"); boollist.Add(false);
            list.Add("f(w) from"); boollist.Add(false);
            list.Add("Array"); boollist.Add(false);
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

            Parallel.Invoke(
                () =>
            {
                string[] all = (checkBox1.Checked) ? Directory.GetFiles(textBox1.Text, "*.*", System.IO.SearchOption.AllDirectories) : Directory.GetFiles(textBox1.Text);

                for (int i = 0; i < all.Length; i++)
                    for (int k = 0; k < st.Length; k++)
                        if (Path.GetFileName(all[i]).StartsWith(st[k]))
                        {
                            File.Delete(all[i]);
                            break;
                        }
            },
            () =>
            {
                string[] all2 = (checkBox1.Checked) ? Directory.GetDirectories(textBox1.Text, "*.*", System.IO.SearchOption.AllDirectories) : Directory.GetDirectories(textBox1.Text);
                string tmp;
                for (int i = 0; i < all2.Length; i++)
                {
                    tmp = all2[i].Substring(Path.GetDirectoryName(all2[i]).Length + 1);
                    for (int k = 0; k < st.Length; k++)
                        if (tmp.StartsWith(st[k]))
                        {
                            Directory.Delete(all2[i], true);
                            break;
                        }
                }
            }
            );
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new TF().ShowDialog();
        }
    }
}
