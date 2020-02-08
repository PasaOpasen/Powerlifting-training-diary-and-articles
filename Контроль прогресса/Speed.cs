using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using МатКлассы;
using JR.Utils.GUI.Forms;


namespace Контроль_прогресса
{
    public partial class Speed : Form
    {
        public Speed()
        {
            InitializeComponent();
            button1.Hide();

            int getlen(TextBox t) => t.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            void setColors()
            {
                if (len1 != len)
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Green;

                if (len2 != len)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Green;

                if (len > 0 && len == len1 && len == len2)
                    button1.Show();
                else
                    button1.Hide();
            }


            textBox1.TextChanged += (object o, EventArgs e) =>
              {
                  len = getlen(textBox1);
                  label4.Text = len.ToString();

                  if (len == 0)
                      textBox3.Text = "";
                  else
                      textBox3.Text = Expendator.Max(Expendator.Repeat(1, len), textBox3.Text.ToIntMas()).ToStringFromExp();

                  setColors();
              };
            textBox2.TextChanged += (object o, EventArgs e) =>
            {
                len1 = getlen(textBox2);
                label5.Text = len1.ToString();

                setColors();
            };
            textBox3.TextChanged += (object o, EventArgs e) =>
            {
                len2 = getlen(textBox3);
                label6.Text = len2.ToString();

                setColors();
            };

        }

        int len = 0, len1 = 0, len2 = 0;

        private void label7_Click(object sender, EventArgs e)
        {
            var st = new string[]
            {
                "Выражение вида:",
                "\tВеса: 50 80 100",
                "\tРазы: 15 10 10",
                "\tСеты: 1 1 2",
                "означает 1 подход в 50кг на 15 раз + 1 подход в 80кг на 10 раз + 2 подхода в 100кг на 10 раз"
            };
            FlexibleMessageBox.Show(Expendator.StringArrayToString(st), "Пример использования", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] w = textBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] st = textBox2.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] sets = textBox3.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < w.Length; i++)
            {
                Program.F2.textBox10.Text = w[i];
                Program.F2.numericUpDown4.Value = Convert.ToInt32(st[i]);
                Program.F2.numericUpDown5.Value = Convert.ToInt32(sets[i]);
                Program.F2.button3_Click(new object(), new EventArgs());
            }

            this.Close();
        }
    }
}
