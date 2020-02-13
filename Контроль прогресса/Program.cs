using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Библиотека_классов;

namespace Контроль_прогресса
{
    internal static class Program
    {
        public static MainForm F1;
        public static Results F2;
        public static ParamsGraf F3;
        public static Book F4;
        public static BeginForm F5;
        public static FastVal F6;
        public static BeginBook F7;
        public static List<PowerLift> mas;
        public static PowerLift[] masn;
        public static string filename = "Набор данных по умолчанию.txt";
        public static bool changefile = false;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mas = new List<PowerLift>();
            F5 = new BeginForm();
            F1 = new MainForm();
            F2 = new Results();
            F3 = new ParamsGraf();
            F4 = new Book();
            F6 = new FastVal();
            F7 = new BeginBook();

            Application.Run(F1);
        }

        public static void FileChooseByRadioButton2(ref RadioButton radioButton1,ref RadioButton radioButton2)
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
    }
}
