using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Библиотека_классов;

namespace Тестирование_в_консоли
{
    class Консоль
    {
        static void Main(string[] args)
        {
            //MakeDirectories.Make(1, 47);
            //PowerLift.Generate(30,3,2,2, true,false,false);
            PowerLift[] p = new PowerLift[3];
            p[0] = new PowerLift(10, 90, 30);
            p[1] = new PowerLift(20, 30, 40);
            p[2] = new PowerLift(10, 40, 50);

            PowerLift[] t = PowerLift.Correct(p);
            for (int i = 0; i < 3; i++)
                Console.WriteLine(t[i].Squat + " " + t[i].Press + " " + t[i].Lift);

            Console.ReadKey();
        }
    }
}
