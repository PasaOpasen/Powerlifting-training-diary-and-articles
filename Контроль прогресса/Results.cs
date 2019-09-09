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
        private int sqt = 0, prs = 0, lft = 0, dr = 0, dru = 1;
        double tonn = 0;
        string s = "";
        public Results()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            PowerLift p;
            Program.F1.GetMass();
            if (Program.mas.Count == 0) p = new PowerLift(100, 50, 120, 67, DateTime.Now);
            else p = new PowerLift(Program.mas.Last());
            textBox2.Text = textBox4.Text = textBox6.Text = textBox14.Text = textBox15.Text = textBox16.Text = "1";
            textBox8.Text = DateTime.Now.ToString().Split(' ')[0];
            textBox1.Text = p.Press.ToString();
            textBox3.Text = p.Squat.ToString();
            textBox5.Text = p.Lift.ToString();
            textBox7.Text = p.Weight.ToString();
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            radioButton3.Checked = true;
            label11.Hide(); label12.Hide(); label13.Hide(); label14.Hide(); label15.Hide(); button3.Hide(); textBox9.Hide(); textBox10.Hide(); textBox11.Hide(); textBox12.Hide(); numericUpDown4.Hide(); numericUpDown5.Hide();
            label21.Hide(); button4.Hide(); button6.Hide(); button5.Hide();comboBox1.Hide(); label22.Hide(); textBox17.Hide();label23.Hide();
            monthCalendar1.Hide();

            //toolTip1.AutomaticDelay = 3000;
            toolTip1.AutoPopDelay = 8000;
            string mes = "Чтобы начать вводить данные о новом упражнении, заполните компоненты и нажмите на /добавить новое упражнение/; чтобы продолжить заполнять данные об упражнении, заполните компоненты и нажмите /добавить в серию подходов/";
            toolTip1.SetToolTip(label23, mes);
            toolTip1.SetToolTip(button5, "Очистить все данные о новом упражнении");
            toolTip1.SetToolTip(button6, "Отсортировать список возможных упражнений");
            toolTip1.SetToolTip(button7, "Зафиксировать верхние данные, чтобы можно было увидеть, как они отобразятся в дневнике");
            toolTip1.SetToolTip(button8, "Зафиксировать комментарий");
            toolTip1.SetToolTip(checkBox4, "Если нужно не просто зафиксировать результат, а полностью описать тренировку, выберите этот пункт");

            Color col=Color.FromArgb(90, Color.Transparent);
            groupBox1.BackColor = col;
            for (int i = 1; i <= 5; i++)
                this.Controls["groupBox" + i.ToString()].BackColor = col;
            for (int i = 1; i <= 4; i++)
                groupBox3.Controls["checkBox" + i.ToString()].BackColor = Color.Transparent;
            for (int i = 3; i <= 5; i++)
                groupBox2.Controls["radioButton" + i.ToString()].BackColor = Color.Transparent;
            for (int i=1;i<=25;i++)
                this.Controls["label" + i.ToString()].BackColor = col;
            label24.Hide();
            label25.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GiveCombVal();
            sqt++;
            sqt %= 2;
            if (sqt == 0) { label6.Hide(); label4.Hide(); label5.Hide(); textBox3.Hide(); textBox4.Hide(); label17.Hide(); textBox15.Hide(); numericUpDown7.Hide(); numericUpDown2.Hide(); }
            else { label6.Show(); label4.Show(); label5.Show(); textBox3.Show(); textBox4.Show(); label17.Show(); textBox15.Show(); numericUpDown7.Show(); numericUpDown2.Show(); }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            GiveCombVal();
            prs++;
            prs %= 2;
            if (prs == 0) { label1.Hide(); label3.Hide(); label2.Hide(); textBox1.Hide(); textBox2.Hide(); label16.Hide(); textBox14.Hide(); numericUpDown6.Hide(); numericUpDown1.Hide(); }
            else { label1.Show(); label3.Show(); label2.Show(); textBox1.Show(); textBox2.Show(); label16.Show(); textBox14.Show(); numericUpDown6.Show(); numericUpDown1.Show(); }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            GiveCombVal();
            lft++;
            lft %= 2;
            if (lft == 0) { label9.Hide(); label7.Hide(); label8.Hide(); textBox5.Hide(); textBox6.Hide(); label18.Hide(); textBox16.Hide(); numericUpDown8.Hide(); numericUpDown3.Hide(); }
            else { label9.Show(); label7.Show(); label8.Show(); textBox5.Show(); textBox6.Show(); label18.Show(); textBox16.Show(); numericUpDown8.Show(); numericUpDown3.Show(); }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Program.F1.GetMAssFromFile();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            label21.Show();
            double press1 = Convert.ToDouble(textBox1.Text), sq1 = Convert.ToDouble(textBox3.Text), l1 = Convert.ToDouble(textBox5.Text), w = Convert.ToDouble(textBox7.Text);
            int press2 = Convert.ToInt32(numericUpDown6.Value),
            sq2 = Convert.ToInt32(numericUpDown7.Value),
            l2 = Convert.ToInt32(numericUpDown8.Value),
            press3 = Convert.ToInt32(numericUpDown1.Value),
            sq3 = Convert.ToInt32(numericUpDown2.Value),
            l3 = Convert.ToInt32(numericUpDown3.Value);

            if (!checkBox1.Checked) { sq1 = sq2 = 0; }
            if (!checkBox2.Checked) { press1 = press2 = 0; }
            if (!checkBox3.Checked) { l1 = l2 = 0; }

            //tonn += sq1 * sq2 * sq3 + l1 * l2 * l3 + press1 * press2 * press3;

            label20.Text = tonn.ToString();
            //tonn -= sq1 * sq2 * sq3 + l1 * l2 * l3 + press1 * press2 * press3;
        }

        public static int OpenClose = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            OpenClose++;
            OpenClose %= 2;
            if (OpenClose == 1)
            {
                button3.Show();
                string
                    name = comboBox1.Text/*textBox11.Text*/,
                    weigth = textBox10.Text,
                    count = numericUpDown4.Value.ToString(),
                    pod = numericUpDown5.Value.ToString();
                double hi;
                
                string hii = textBox17.Text;
                try
                {
                    hi = Convert.ToDouble(textBox17.Text);
                if (hi != 0)
                {
                    s += $"Другое упражнение №{dru}: {name} ; {pod} подходов по {count} повторений с весом {weigth} при высоте {hi} ";
                    listBox1.Items.Add($"\tДругое упражнение №{dru}: {name} ; {pod} подходов по {count} повторений с весом {weigth} при высоте {hi} ");
                }
                else
                {
                    s += $"Другое упражнение №{dru}: {name} ; {pod} подходов по {count} повторений с весом {weigth} ";
                    listBox1.Items.Add($"\tДругое упражнение №{dru}: {name} ; {pod} подходов по {count} повторений с весом {weigth} ");
                }
                }
                catch
                {
                    s += $"Другое упражнение №{dru}: {name} ; {pod} подходов по {count} повторений с весом {weigth} при высоте {hii} ";
                    listBox1.Items.Add($"\tДругое упражнение №{dru}: {name} ; {pod} подходов по {count} повторений с весом {weigth} при высоте {hii} ");
                }



                try
                { tonn += Convert.ToDouble(pod) * Convert.ToDouble(count) * Convert.ToDouble(weigth); }
                finally { }
                label20.Text = "?";
                label20.Text = tonn.ToString();
                dru++;
                button4.Text = "Завершить запись упражнения";
            }
            else
            {
                button3.Hide();
                button5_Click(sender, e);
                button4.Text = "Добавить новое упражнение";
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            comboBox1.Text = "";
            textBox11.Text = "";
            textBox17.Text = "0";
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
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add("");
            if(listBox1.Items.Count>0)
            {
            string s = (string)listBox1.Items[listBox1.Items.Count - 1];
            if(s.Contains("\tКомментарий:")) listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
            }

            listBox1.Items.Insert(listBox1.Items.Count,$"\tКомментарий: {textBox13.Text}");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void button3_Click(object sender, EventArgs e)
        {
            string
    //name = textBox11.Text,
    weigth = textBox10.Text,
    count = numericUpDown4.Value.ToString(),
    pod = numericUpDown5.Value.ToString();
            double hi;
            
            string hii = textBox17.Text;
            try
            {
                hi = Convert.ToDouble(textBox17.Text);
            if (hi != 0)
            {
                s += $"+ {pod} подходов по {count} повторений с весом {weigth} при высоте {hi} ";
                listBox1.Items[listBox1.Items.Count-1]+= $"+ {pod} подходов по {count} повторений с весом {weigth} при высоте {hi} ";
            }
            else
            {
                s += $"+ {pod} подходов по {count} повторений с весом {weigth} ";
                listBox1.Items[listBox1.Items.Count - 1] += $"+ {pod} подходов по {count} повторений с весом {weigth} ";
            }
            }
            catch
            {
                s += $"+ {pod} подходов по {count} повторений с весом {weigth} при высоте {hii} ";
                listBox1.Items[listBox1.Items.Count - 1] += $"+ {pod} подходов по {count} повторений с весом {weigth} при высоте {hii} ";
            }

            try { tonn += Convert.ToDouble(pod) * Convert.ToDouble(count) * Convert.ToDouble(weigth); } finally { }
            label20.Text = "?";
            label20.Text = tonn.ToString();
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
            dr++;
            dr %= 2;
            if (dr == 0) { label23.Hide(); label11.Hide(); label12.Hide(); label13.Hide(); label14.Hide(); label15.Hide(); button3.Hide(); button4.Hide(); button5.Hide(); button6.Hide(); textBox9.Hide(); textBox10.Hide(); textBox11.Hide();comboBox1.Hide(); textBox12.Hide(); numericUpDown4.Hide(); numericUpDown5.Hide();label22.Hide();textBox17.Hide(); }
            else { label23.Show(); label11.Show(); label12.Show(); label13.Show(); label14.Show(); label15.Show(); /*button3.Show();*/ button4.Show(); button5.Show();button6.Show(); textBox9.Show(); textBox10.Show(); textBox11.Show();comboBox1.Show(); textBox12.Show(); numericUpDown4.Show(); numericUpDown5.Show(); label22.Show(); textBox17.Show(); }
        }

        private void ускоренноеЗаданиеПодходовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Speed().ShowDialog();
        }

        private PowerLift GetNew()
        {
            double press1 = Convert.ToDouble(textBox1.Text), sq1 = Convert.ToDouble(textBox3.Text), l1 = Convert.ToDouble(textBox5.Text), w = Convert.ToDouble(textBox7.Text);
            int press2 = Convert.ToInt32(numericUpDown6.Value),
            sq2 = Convert.ToInt32(numericUpDown7.Value),
            l2 = Convert.ToInt32(numericUpDown8.Value),
            press3 = Convert.ToInt32(numericUpDown1.Value),
            sq3 = Convert.ToInt32(numericUpDown2.Value),
            l3 = Convert.ToInt32(numericUpDown3.Value);

            /*if(label20.Text=="?")*/
            //tonn += sq1 * sq2 * sq3 + l1 * l2 * l3 + press1 * press2 * press3;

            DateTime t = DateTime.Now;
            if (radioButton4.Checked) t = Convert.ToDateTime(textBox8.Text);
            if (radioButton5.Checked) t = dat;
            if (radioButton6.Checked) t = t.AddDays(-1);

            if (!checkBox1.Checked) { sq1 = sq2 = 0; }
            if (!checkBox2.Checked) { press1 = press2 = 0; }
            if (!checkBox3.Checked) { l1 = l2 = 0; }

            PowerLift res = new PowerLift(PM(sq1, sq2), PM(press1, press2), PM(l1, l2), w, t);
            //Program.mas.Add(new PowerLift(PM(sq1, sq2), PM(press1, press2), PM(l1, l2), w, t));
            res.Tonn = tonn;
            //Program.mas.Last().Tonn = tonn;

            //s += "Tonnage = " + tonn;
            if (textBox13.Text.Length > 0) s += "| Комментарий: " + textBox13.Text;
            res.Comment = new string(s.ToCharArray());
            //Program.mas.Last().Comment = new string(s.ToCharArray());
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (radioButton2.Checked) Program.F1.GetMAssFromFile();

            Program.mas.Add(GetNew());

            PowerLift.PrintInFile(new StreamWriter(Program.filename), Program.mas.ToArray());

            s = "";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
