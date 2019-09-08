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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            listBox1.Items.Clear();
            Color col = Color.FromArgb(100, Color.Transparent);
            groupBox1.BackColor = col;
            radioButton1.BackColor = Color.Transparent; radioButton2.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (radioButton2.Checked) Program.F1.GetMAssFromFile();


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

                    //StreamWriter r = new StreamWriter(s);
                    //StreamReader sr = new StreamReader("Полное решение.txt");
                    //while (s != null)
                    //{
                    //    s = sr.ReadLine();
                    //    r.WriteLine(s);
                    //}
                    //sr.Close();
                    //r.Close();
                    listBox1.Items.Clear();
                    string[] st = Resp(Program.mas.ToArray());
                    System.IO.File.WriteAllLines(s, st);

                }
                catch
                {
                    MessageBox.Show("Невозможно сохранит файлйцтцйивйцивйц", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //this.Close();
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
                //list.Add(p[i].ToString());
                list.Add($"Дата записи: {p[i].Time}");
                list.Add($"Максимальные результаты в базовых упражнениях: присед = {p[i].Squat}, жим = {p[i].Press}, тяга = {p[i].Lift}");
                list.Add($"\tТоннаж = {p[i].Tonn}");
                string s = p[i].Comment;
                //string s = new string(p[i].Comment.ToCharArray());
                //s = s.Substring(s.IndexOf("Another: ")+8,s.Length);
                //list.Add(s);
                string[] st = s.Split(new string[] { "Другое упражнение №", "| Комментарий: " }, StringSplitOptions.RemoveEmptyEntries);
                if (st.Length > 0)
                {
                    for (int j = 1; j < st.Length - 1; j++)
                        list.Add($"\tДругое упражнение №{st[j]}");
                    if (st.Last().Replace(" ", "").Length > 0) list.Add($"\tКомментарий: {st.Last()}");
                }


                list.Add("");
            }
            listBox1.Items.AddRange(list.ToArray());
            return list.ToArray();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Program.F1.GetMAssFromFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] st = Resp(Program.mas.ToArray());
        }
    }
}
