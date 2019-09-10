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
using Библиотека_классов;
using static Библиотека_классов.PowerLift;

namespace Контроль_прогресса
{
    public partial class Results : Form
    {
        private int dru = 1;
        double tonn = 0;
        string s = "";

        public Results()
        {
            InitializeComponent();

            GetBeginData();
            SetToolTips();
            SetComboBox();

            button3.Hide();
            textBox8.Hide();
            monthCalendar1.Hide();
            groupBox7.Hide();
            label20.Hide();
            DownHide();
            


            Color col = Color.FromArgb(90, Color.Transparent);
            for (int i = 1; i <= 8; i++)
                this.Controls["groupBox" + i.ToString()].BackColor = col;
            label20.BackColor = col;
            button8.BackColor = col;
            //for (int i = 1; i <= 4; i++)
            //   groupBox3.Controls["checkBox" + i.ToString()].BackColor = Color.Transparent;
            //for (int i = 3; i <= 5; i++)
            //    groupBox2.Controls["radioButton" + i.ToString()].BackColor = Color.Transparent;
            //for (int i=1;i<=25;i++)              
            //    this.Controls["label" + i.ToString()]?.BackColor = col;
        }
        private void GetBeginData()
        {
            PowerLift p;
            Program.F1.GetMass();
            p = (Program.mas.Count == 0) ? new PowerLift(100, 50, 120, 67, DateTime.Now) : new PowerLift(Program.mas.Last());

            textBox8.Text = DateTime.Now.ToString().Split(' ')[0];
            textBox1.Text = p.Press.ToString();
            textBox3.Text = p.Squat.ToString();
            textBox5.Text = p.Lift.ToString();
            textBox7.Text = p.Weight.ToString();
        }
        private void SetToolTips()
        {
            toolTip1.AutoPopDelay = 8000;
            string mes = "Чтобы начать вводить данные о новом упражнении, заполните компоненты и нажмите на /добавить новое упражнение/; чтобы продолжить заполнять данные об упражнении, заполните компоненты и нажмите /добавить в серию подходов/";
            toolTip1.SetToolTip(label23, mes);
            toolTip1.SetToolTip(button5, "Очистить все данные о новом упражнении");
            toolTip1.SetToolTip(button6, "Отсортировать список возможных упражнений");
            toolTip1.SetToolTip(button7, "Зафиксировать верхние данные, чтобы можно было увидеть, как они отобразятся в дневнике");
            toolTip1.SetToolTip(button8, "Зафиксировать комментарий");
            toolTip1.SetToolTip(checkBox4, "Если нужно не просто зафиксировать результат, а полностью описать тренировку, выберите этот пункт");
        }
        private void SetComboBox()
        {
            if (textBox3.Text.Length>1)
                comboBox1.Text = "Присед со штангой на плечах";
            else if (textBox1.Text.Length > 1)
                comboBox1.Text = "Жим лёжа";
            else if (textBox5.Text.Length > 1)
                comboBox1.Text = "Становая тяга классикой";
            else
                comboBox1.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GiveCombVal();
            if (!checkBox1.Checked) { label6.Hide(); label4.Hide(); label5.Hide(); textBox3.Hide(); numericUpDown7.Hide(); }
            else { label6.Show(); label4.Show(); label5.Show(); textBox3.Show(); numericUpDown7.Show(); }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            GiveCombVal();
            if (!checkBox2.Checked) { label1.Hide(); label3.Hide(); label2.Hide(); textBox1.Hide();  numericUpDown6.Hide(); }
            else { label1.Show(); label3.Show(); label2.Show(); textBox1.Show(); numericUpDown6.Show(); }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            GiveCombVal();
            if (!checkBox3.Checked) { label9.Hide(); label7.Hide(); label8.Hide(); textBox5.Hide(); numericUpDown8.Hide(); }
            else { label9.Show(); label7.Show(); label8.Show(); textBox5.Show();  numericUpDown8.Show(); }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                bool b = Program.F1.GetMAssFromFile();
                if (!b)
                {
                    Program.F1.GetMass();
                    radioButton1.Checked = true;
                }
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            label20.Text = $"Тоннаж = {tonn}";
        }

        public static bool OpenClose = false;
        private void button4_Click(object sender, EventArgs e)
        {
            OpenClose = !OpenClose;
            if (OpenClose)
            {
                button3.Show();
                string
                    name = comboBox1.Text;

                AddToLists(dru, name);

                DownShow();
                UpHide();
                dru++;
                button4.Text = "Завершить запись упражнения";
            }
            else
            {
                UpShow();
                DownHide();
                button3.Hide();
                button5_Click(sender, e);
                button4.Text = "Добавить новое упражнение";
            }
        }
        private void AddToLists(int number,string name)
        {
            string tmp=$"Другое упражнение №{number}: {name} ;";
            s += tmp;
            listBox1.Items.Add(tmp);
        }
        private void AddToLists(string pod, string count, string weight)
        {
            string tmp = $"+ {pod} подходов по {count} повторений с весом {weight} ";
            s += tmp;
            listBox1.Items.Add(tmp);
        }

        private void DownShow()
        {
            label11.Show();
            textBox10.Show();
            label12.Show();
            numericUpDown4.Show();
            label15.Show();
            numericUpDown5.Show();
            menuStrip1.Show();
        }
        private void DownHide()
        {
            label11.Hide();
            textBox10.Hide();
            label12.Hide();
            numericUpDown4.Hide();
            label15.Hide();
            numericUpDown5.Hide();
            menuStrip1.Hide();
        }
        private void UpShow()
        {
            label14.Show();
            button6.Show();
            button5.Show();
            label23.Show();
            comboBox1.Show();
        }
        private void UpHide()
        {
            label14.Hide();
            button6.Hide();
            button5.Hide();
            label23.Hide();
            comboBox1.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            comboBox1.Text = "";
            numericUpDown4.Value = 1;
            button3.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Sorted = true;
        }

        private DateTime dat;
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dat = Convert.ToDateTime(e.Start);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Show();
            textBox8.Hide();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Hide();
            textBox8.Show();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Hide();
            textBox8.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                string s = (string)listBox1.Items[listBox1.Items.Count - 1];
                if (s.Contains("\tКомментарий:")) listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
            }

            listBox1.Items.Insert(listBox1.Items.Count, $"\tКомментарий: {textBox13.Text}");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void button3_Click(object sender, EventArgs e)
        {
            string
               weigth = textBox10.Text,
               count = numericUpDown4.Value.ToString(),
               pod = numericUpDown5.Value.ToString();

            AddToLists(pod, count, weigth);

            try { tonn += Convert.ToDouble(pod) * Convert.ToDouble(count) * Convert.ToDouble(weigth); } finally { }
            label20_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PowerLift p = GetNew();
            string s = p.ToString();
            if (listBox1.Items.Count > 0)
            {
                string ss = (string)listBox1.Items[0];
                if (ss.Contains("Date")) listBox1.Items.RemoveAt(0);
            }
            listBox1.Items.Insert(0, s.Substring(0, s.IndexOf("Another")));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            s = "";
            dru = 1;
            tonn = 0;
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                groupBox7.Show();
            else
                groupBox7.Hide();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Hide();
            textBox8.Hide();
        }

        private void ускоренноеЗаданиеПодходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Speed().ShowDialog();
        }

        private PowerLift GetNew()
        {
            double press1 = Convert.ToDouble(textBox1.Text), 
                sq1 = Convert.ToDouble(textBox3.Text), 
                l1 = Convert.ToDouble(textBox5.Text), 
                w = Convert.ToDouble(textBox7.Text);
            int press2 = Convert.ToInt32(numericUpDown6.Value),
            sq2 = Convert.ToInt32(numericUpDown7.Value),
            l2 = Convert.ToInt32(numericUpDown8.Value);

            if (!checkBox1.Checked) { sq1 = sq2 = 0; }
            if (!checkBox2.Checked) { press1 = press2 = 0; }
            if (!checkBox3.Checked) { l1 = l2 = 0; }

            PowerLift res = new PowerLift(PM(sq1, sq2), PM(press1, press2), PM(l1, l2), w, GetTime());
            res.Tonnage = tonn;

            if (textBox13.Text.Length > 0) s += $"| Комментарий: {textBox13.Text}";
            res.Comment = new string(s.ToCharArray());

            return res;
        }
        private DateTime GetTime()
        {
            DateTime t = DateTime.Now;
            if (radioButton4.Checked) t = Convert.ToDateTime(textBox8.Text);
            if (radioButton5.Checked) t = dat;
            if (radioButton6.Checked) t = t.AddDays(-1);
            return t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.mas.Add(GetNew());

            PowerLift.PrintInFile(new StreamWriter(Program.filename), Program.mas.ToArray());
            s = "";
            listBox1.Items.Clear();
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void GiveCombVal()
        {
            if (checkBox1.Checked)
                comboBox1.Text = "Присед со штангой на плечах";
            else if (checkBox2.Checked)
                comboBox1.Text = "Жим лёжа";
            else if (checkBox3.Checked)
                comboBox1.Text = "Становая тяга классикой";
            else
                comboBox1.Text = "";
        }
    }
}
