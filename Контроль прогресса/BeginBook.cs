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
using System.IO;

namespace Контроль_прогресса
{
    public partial class BeginBook : Form
    {
        public BeginBook()
        {
            InitializeComponent();
            Color col = Color.FromArgb(130, Color.Transparent);
            for (int i = 1; i <= 11; i++)
                this.Controls["label" + i.ToString()].BackColor = col;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            numericUpDown6.Value = 1;
            numericUpDown7.Value = 1;
            numericUpDown8.Value = 1;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double press1 = Convert.ToDouble(textBox1.Text), sq1 = Convert.ToDouble(textBox3.Text), l1 = Convert.ToDouble(textBox5.Text), w = Convert.ToDouble(textBox7.Text);
                int press2 = Convert.ToInt32(numericUpDown6.Value),
                sq2 = Convert.ToInt32(numericUpDown7.Value),
                l2 = Convert.ToInt32(numericUpDown8.Value);
                Program.mas = new List<Библиотека_классов.PowerLift>();
                Program.mas.Add(new Библиотека_классов.PowerLift(PM(sq1, sq2), PM(press1, press2), PM(l1, l2), w, DateTime.Now));
            }
            catch
            {
                MessageBox.Show("Введены некорректные данные, перепроверьте их");
            }

            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить файл как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string s;
                     s = savedialog.FileName;
                    Program.filename = s;
                    Program.masn = Program.mas.ToArray();
                    string[] st = new string[Program.masn.Length];
                    for (int i = 0; i < st.Length; i++)
                        st[i] = Program.mas[i].ToString();
                    System.IO.File.WriteAllLines(s, st);

                }
                catch
                {
                    MessageBox.Show("Невозможно сохранит файлйцтцйивйцивйц", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Close();
                }
            }
        }
    }
}
