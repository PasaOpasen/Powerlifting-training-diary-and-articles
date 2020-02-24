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

namespace Контроль_прогресса
{
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            button1.Hide();
            Color col = Color.FromArgb(100, Color.Transparent);
            groupBox1.BackColor = col;
            radioButton1.BackColor = Color.Transparent; radioButton2.BackColor = Color.Transparent;

            this.FormClosing += (object o, FormClosingEventArgs e) =>
            {
                listBox1.Items.Clear();
            };
        }

        private string[] listmas;
        private void button1_Click(object sender, EventArgs e)
        {
            Save(listmas);
        }

        private void Save(string[] st)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.FileName = Program.filename.Substring(0, Program.filename.IndexOf(".txt")) + " (удобочитаемый).txt";
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
                    try { s = savedialog.FileName; }
                    catch { s = Program.filename.Substring(0, Program.filename.IndexOf(".txt")) + " (удобочитаемый).txt"; }
                    System.IO.File.WriteAllLines(s, st);

                }
                catch
                {
                    MessageBox.Show("Невозможно сохранит файл", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                }
            }
        }

        private string[] Resp(PowerLift[] p)
        {
            Array.Sort(p);

            var list = new List<string>();
            list.Add($"Удобочитаемая версия дневника от {DateTime.Now}"); list.Add("");

            for (int i = 0; i < p.Length; i++)
            {
                list.Add($"Дата записи: {p[i].Time}");
                list.Add($"Максимальные результаты в базовых упражнениях: присед = {p[i].Squat}, жим = {p[i].Press}, тяга = {p[i].Lift}");
                list.Add($"\tТоннаж = {p[i].Tonnage}");
                string s = p[i].Comment;
                string[] st = s.Split(new string[] { "Другое упражнение №", "| Комментарий: " }, StringSplitOptions.RemoveEmptyEntries);
                if (st.Length > 0)
                {
                    for (int j = 1; j < st.Length - 1; j++)
                        list.Add($"\tДругое упражнение №{st[j]}");
                    if (st.Last().Replace(" ", "").Length > 0) list.Add($"\tКомментарий: {st.Last()}");
                }
                list.Add("");
            }
            var ll = list.ToArray();
            listBox1.Items.AddRange(ll);
            return ll;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Program.FileChooseByRadioButton2(ref radioButton1, ref radioButton2);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listmas = Resp(Program.mas.ToArray());
            button1.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
