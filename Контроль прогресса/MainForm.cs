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
using МатКлассы;
using System.Diagnostics;
using JR.Utils.GUI.Forms;


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

            CreateDefaultFile();
        }

        public void CreateDefaultFile()
        {
            if(!File.Exists(Program.filename))
            {
               // File.Create(Program.filename);
                StreamWriter f = new StreamWriter(Program.filename);f.Close();
                MessageBox.Show($"Похоже, вы в первый раз открыли эту программу! Для вас автоматически создан дневник по умолчанию по адресу \"{Path.Combine(Environment.CurrentDirectory, Program.filename)}\". Если вы хотите изменить дневник или выбрать существующий, это можно сделать в программе", "Создан дневник по умолчанию", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetMass()
        {
            Program.filename = Program.F5.textBox1.Text;
            StreamReader s;
            if (File.Exists(Program.filename))
                s = new StreamReader(Program.filename);
            else
            {
                Program.filename = "Набор данных по умолчанию.txt";
                Program.F5.textBox1.Text = Program.filename;
                s = new StreamReader(Program.filename);
            }

            try { Program.mas = PowerLift.GetFromFile(s).ToList(); }
            catch
            {
                Program.mas = new List<PowerLift>();
            }
            finally { s.Dispose(); }
        }
        /// <summary>
        /// Задать массив из файла, сперва выбрав файл
        /// </summary>
        /// <returns></returns>
        public bool GetMAssFromFile()
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
                return true;
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetMass();
            Program.F3 = new ParamsGraf();
            Program.F3.ShowDialog();
        }

        private void определитьДневникПоУмолчаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.F5.ShowDialog();
        }

        private void добавитьЗаписьОТренировкеВДневникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetMass();
            Program.F2 = new Results();
            Program.F2.ShowDialog();
        }

        private void открытьДневникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetMass();
            Program.F4.ShowDialog();
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
            string text = $"Присед = {p.Squat}; жим = {p.Press}; тяга = {p.Lift}; последний вес = {p.Weight}.{Environment.NewLine}Последняя тренировка: \t{p.Time.Date.ToString().Substring(0, 10)}."
                + Environment.NewLine + $"Всего тренировок: \t{Program.masn.Length}"
                + Environment.NewLine + $"Сумма = {p.Sum}, процентное соотношение присед-жим-тяга от суммы: \t{p.SquatPercent}-{p.PressPercent}-{p.LiftPercent}."
                + Environment.NewLine + $"Отношение приседа/жима/тяги к собственному весу: \t{p.SquatWeight}/{p.PressWeight}/{p.LiftWeight}.";

            MessageBox.Show(text, "Максимальные результаты по дневнику", MessageBoxButtons.OK);
        }

        private void написатьАвторуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] st = new string[]
            {
                "GitHub:\t https://github.com/PasaOpasen",
                "Gmail:\t qtckpuhdsa@gmail.com",
                "Discord:\t Пася Опасен#3065",
                "Instagram: ofdegradation",
                "Telegram:\t @PasaOpasen",
                "Steam:\t https://steamcommunity.com/id/PasaOpasen",
                "VK:\t https://vk.com/roman_disease",
                "PornHub: https://rt.pornhub.com/users/demetrypaskal"
            };
            FlexibleMessageBox.MAX_WIDTH_FACTOR = 2;
            FlexibleMessageBox.Show(Expendator.StringArrayToString(st), "Контакты", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void скачатьМатериалыПоПауэрлифтингуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://yadi.sk/d/xTukkNXI3Zrq8y");
            System.Diagnostics.Process.Start("https://github.com/PasaOpasen/LittleHelps/tree/master/Материалы%20по%20пауэрлифтингу%20и%20не%20только");
        }
    }
}
