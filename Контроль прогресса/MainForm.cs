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

namespace Контроль_прогресса
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            for (int i = 0; i < 6; i++)
            {
                chart1.Series[i].IsVisibleInLegend = false;
                chart1.Series[i].ToolTip = "X = #VALX, Y = #VALY";
            }
                
            button4.Hide();
            button5.Hide();
            button1.Hide();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.SetToolTip(button3, "Показать графически прогресс в золотой тройке и не только");
            toolTip1.SetToolTip(button6, "Быстро оценить повторный максимум");

            this.FormClosing += (object o, FormClosingEventArgs e) =>
             {
                 if (Program.changefile)
                 {
                     var res = MessageBox.Show("Требуется ли сохранить текущий файл по умолчанию как постоянный файл по умолчанию?", "Сохранение данных", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (res == System.Windows.Forms.DialogResult.Yes)
                         using (StreamWriter w = new StreamWriter("Adress.txt"))
                             w.WriteLine(Program.filename);
                 }
             };
        }
        public void GetMass()
        {
            //
            Program.filename = Program.F5.textBox1.Text;
            StreamReader s;
            try { s = new StreamReader(Program.filename); }
            catch
            {
                Program.filename = "Набор данных по умолчанию.txt";
                Program.F5.textBox1.Text = Program.filename;
                s = new StreamReader(Program.filename);
            }
            //Program.mas;
            try { Program.mas = PowerLift.GetFromFile(s).ToList(); }
            catch
            {
                Program.mas = new List<PowerLift>();
                //Program.mas.Add(new PowerLift(0, 0, 0));
            }
            finally { s.Dispose(); }
        }
        public void GetMAssFromFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                Program.filename = openFileDialog1.FileName;
                try
                {
                    Program.mas = PowerLift.GetFromFile(sr).ToList();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }

                sr.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetMass();
            Program.F3 = new ParamsGraf();
            Program.F3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {          
            Program.F2 = new Results(); 
             GetMass();
            Program.F2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetMass();
            Program.F4.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://vk.com/romandisease");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.F5.ShowDialog();
        }

        private void определитьДневникПоУмолчаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void добавитьЗаписьОТренировкеВДневникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void открытьДневникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.F6.ShowDialog();
        }

        private void начатьНовыйДневникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.F7.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GetMass();
            Program.mas.Sort();
            Program.masn = PowerLift.Correct(Program.mas.ToArray());
            PowerLift.Correct(Program.masn);
            PowerLift p = Program.masn.Last();
            string text = $"Присед = {p.Squat}; жим = {p.Press}; тяга = {p.Lift}; последний вес = {p.Weight}; последняя тренировка: {p.Time.Date.ToString().Substring(0,10)}."
                + Environment.NewLine +$"Всего тренировок: {Program.masn.Length}"
                +Environment.NewLine+$"Сумма = {p.Sum}, процентное соотношение присед-жим-тяга от суммы: {p.SquatPercent}-{p.PressPercent}-{p.LiftPercent}."
                +Environment.NewLine+$"Отношение приседа/жима/тяги к собственному весу: {p.SquatWeight}/{p.PressWeight}/{p.LiftWeight}.";
            MessageBox.Show(text,"Мои максимальные результаты", MessageBoxButtons.OK);
        }

        private void написатьАвторуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.me/romandisease");
        }

        private void скачатьМатериалыПоПауэрлифтингуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://yadi.sk/d/xTukkNXI3Zrq8y");
        }
    }
}
