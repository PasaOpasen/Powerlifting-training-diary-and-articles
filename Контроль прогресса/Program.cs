using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Библиотека_классов;

namespace Контроль_прогресса
{
    static class Program
    {
        public static Form1 F1;
       public  static Form2 F2;
        public static Form3 F3;
        public static Form4 F4;
        public static Form5 F5;
        public static Form6 F6;
        public static Form7 F7;
        public static List<PowerLift> mas;
        public static PowerLift[] masn;
        public static string filename= "Набор данных по умолчанию.txt";
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
            F5 = new Form5();
            F1 = new Form1();
            F2 = new Form2();
            F3 = new Form3();
            F4 = new Form4();
            F6 = new Form6();
            F7 = new Form7();
            
            Application.Run(F1);
        }
    }
}
