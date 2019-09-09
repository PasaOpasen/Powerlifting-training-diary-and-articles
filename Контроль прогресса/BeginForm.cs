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
using System.IO;

namespace Контроль_прогресса
{
    public partial class BeginForm : Form
    {
        public BeginForm()
        {
            InitializeComponent();
           // textBox1.Hide();

            if (File.Exists("Adress.txt"))
                using (StreamReader re = new StreamReader("Adress.txt"))
                    textBox1.Text = re.ReadLine().Replace("\n", "");
            else
                textBox1.Text = Path.Combine(Environment.CurrentDirectory, Program.filename);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                Program.F5.textBox1.Text = openFileDialog1.FileName;
                Program.filename = openFileDialog1.FileName;
                sr.Close();
            }
            Program.changefile = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = Program.filename;
            savedialog.Title = "Создать файл как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = savedialog.FileName;
                    string s = textBox1.Text;
                    Program.filename = savedialog.FileName;
                    System.IO.File.WriteAllLines(s, new string[] { "" });
                }
                catch
                {
                    MessageBox.Show("Невозможно создать файл", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Program.changefile = true; this.Close(); }
            }
        }
    }
}
