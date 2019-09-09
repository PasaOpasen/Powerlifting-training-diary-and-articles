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
    public partial class ParamsGraf : Form
    {
        //PowerLift[] masn;
        public ParamsGraf()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            radioButton4.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            //checkBox6.Hide();
            //masn = new PowerLift[0];
            toolTip1.SetToolTip(checkBox7, "Коррекция означает, что для каждого дня будут выводиться максимальные данные к этому дню включительно");
            toolTip1.AutoPopDelay = 5000;

            Color col = Color.FromArgb(90, Color.Transparent);
            groupBox1.BackColor = col;
            for (int i = 1; i <= 2; i++)
                this.Controls["groupBox" + i.ToString()].BackColor = col;
            col = Color.Transparent;
            for (int i = 1; i <= 7; i++)
                try { groupBox1.Controls["checkBox" + i.ToString()].BackColor = col; }
                catch { groupBox2.Controls["checkBox" + i.ToString()].BackColor = col; }
            for (int i = 1; i <= 4; i++)
                try { groupBox1.Controls["radioButton" + i.ToString()].BackColor = col; }
                catch { groupBox2.Controls["radioButton" + i.ToString()].BackColor = col; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<6;i++)
            {
Program.F1.chart1.Series[i].Points.Clear();
                Program.F1.chart1.Series[i].IsVisibleInLegend = false;
            }
            
            //if (radioButton2.Checked) Program.F1.GetMAssFromFile();
            if (Program.mas.Count > 1)
            {
                Program.mas.Sort();
                //Program.masn = PowerLift.Correct(Program.mas.Where(n => !(n.Sum == 0)).ToArray());
                if(checkBox7.Checked)Program.masn = PowerLift.Correct(Program.mas.ToArray());
                else Program.masn = Program.mas.ToArray();

                if (checkBox1.Checked) Program.F1.chart1.Series[0].IsVisibleInLegend = true;
                if (checkBox2.Checked) Program.F1.chart1.Series[1].IsVisibleInLegend = true;
                if (checkBox3.Checked) Program.F1.chart1.Series[2].IsVisibleInLegend = true;
                if (checkBox4.Checked) Program.F1.chart1.Series[3].IsVisibleInLegend = true;
                if (checkBox5.Checked) Program.F1.chart1.Series[4].IsVisibleInLegend = true;
                if (checkBox6.Checked) Program.F1.chart1.Series[5].IsVisibleInLegend = true;

                if (radioButton4.Checked) { ShowAbsolete(); /*Program.F1.chart1.Series[4].IsVisibleInLegend = true;*/ }
                else
                    try { ShowOtn(); }
                    catch { ShowAbsolete(); }

                Program.F1.label1.Hide();
                Program.F1.linkLabel1.Hide();
                Program.F1.pictureBox1.Hide();

                this.Dispose();
            }
            else MessageBox.Show("Невозможно вывести график при пустых входных данных! Чтобы отслеживать прогресс, требуется иметь данные хотя бы о двух тренировках! Проверьте, правильно ли выбран файл данных, либо добавьте новые данные в файл.");
            //this.Close();
        }

        private void FindMin()
        {
            var list = new List<double>();
            for (int i = 0; i < Program.masn.Length; i++)
            {
                if (checkBox1.Checked)
                   list.Add(Program.masn[i].Squat);
                if (checkBox2.Checked)
                    list.Add(Program.masn[i].Press);
                if (checkBox3.Checked)
                    list.Add(Program.masn[i].Lift);
                if (checkBox4.Checked)
                    list.Add(Program.masn[i].Sum);
                if (checkBox5.Checked)
                    list.Add(Program.masn[i].Tonnage);
                if (checkBox6.Checked)
                    list.Add(Program.masn[i].Weight);
            }

            list.RemoveAll(n => n == 0);
            double t = 0.05;
                Program.F1.chart1.ChartAreas[0].AxisY.Minimum = list.Min()*(1-t);
            Program.F1.chart1.ChartAreas[0].AxisY.Maximum = list.Max() * (1 + t);
        }

        private void ShowAbsolete()
        {
            FindMin();

            //for(int i=0;i<6;i++)
            //{
            //    Program.F1.chart1.Series.Add(i.ToString());
            //    Program.F1.chart1.Series[6 + i].IsVisibleInLegend = false;
            //    Program.F1.chart1.Series[6 + i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //    Program.F1.chart1.Series[6 + i].BorderWidth = 4;
            //    Program.F1.chart1.Series[6 + i].Color = Program.F1.chart1.Series[i].Color;
            //}

            if (checkBox1.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Squat > 0)
                    {
                        Program.F1.chart1.Series[0].Points.AddXY(Program.masn[i].Time, Program.masn[i].Squat);
                        //Program.F1.chart1.Series[0+6].Points.AddXY(Program.masn[i].Time, Program.masn[i].Squat);
                    }
            if (checkBox2.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Press > 0)
                    {
                        Program.F1.chart1.Series[1].Points.AddXY(Program.masn[i].Time, Program.masn[i].Press);
                        //Program.F1.chart1.Series[1+6].Points.AddXY(Program.masn[i].Time, Program.masn[i].Press);
                    }
            if (checkBox3.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Lift > 0)
                    {
                        Program.F1.chart1.Series[2].Points.AddXY(Program.masn[i].Time, Program.masn[i].Lift);
                        //Program.F1.chart1.Series[2+6].Points.AddXY(Program.masn[i].Time, Program.masn[i].Lift);
                    }
            if (checkBox4.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Sum > 0)
                    {
                        Program.F1.chart1.Series[3].Points.AddXY(Program.masn[i].Time, Program.masn[i].Sum);
                        //Program.F1.chart1.Series[3+6].Points.AddXY(Program.masn[i].Time, Program.masn[i].Sum);
                    }
            if (checkBox5.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Tonnage > 0)
                    {
                        Program.F1.chart1.Series[4].Points.AddXY(Program.masn[i].Time, Program.masn[i].Tonnage);
                        //Program.F1.chart1.Series[4+6].Points.AddXY(Program.masn[i].Time, Program.masn[i].Tonnage);
                    }
            if (checkBox6.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Weight > 0)
                    {
                        Program.F1.chart1.Series[5].Points.AddXY(Program.masn[i].Time, Program.masn[i].Weight);
                        //Program.F1.chart1.Series[5+6].Points.AddXY(Program.masn[i].Time, Program.masn[i].Weight);
                    }

        }
        private void ShowOtn()
        {
            if (checkBox1.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Squat > 0 && Program.masn[i].Weight!=0) Program.F1.chart1.Series[0].Points.AddXY(Program.masn[i].Time, Program.masn[i].Squat/ Program.masn[i].Weight);
            if (checkBox2.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Press > 0 && Program.masn[i].Weight != 0) Program.F1.chart1.Series[1].Points.AddXY(Program.masn[i].Time, Program.masn[i].Press / Program.masn[i].Weight);
            if (checkBox3.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Lift > 0 && Program.masn[i].Weight != 0) Program.F1.chart1.Series[2].Points.AddXY(Program.masn[i].Time, Program.masn[i].Lift / Program.masn[i].Weight);
            if (checkBox4.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Sum > 0 && Program.masn[i].Weight != 0) Program.F1.chart1.Series[3].Points.AddXY(Program.masn[i].Time, Program.masn[i].Sum / Program.masn[i].Weight);
            if (checkBox5.Checked)
                for (int i = 0; i < Program.masn.Length; i++)
                    if (Program.masn[i].Tonnage > 0 && Program.masn[i].Weight != 0) Program.F1.chart1.Series[4].Points.AddXY(Program.masn[i].Time, Program.masn[i].Tonnage / Program.masn[i].Weight);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Program.F1.GetMAssFromFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int ves = 0;
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ves++;
            ves %= 2;
            if (ves == 1) checkBox6.Hide();
            else checkBox6.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
