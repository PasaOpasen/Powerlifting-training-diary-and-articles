using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Библиотека_классов
{
    public static class MakeDirectories
    {
        public static void Make(int a, int b, string s = "", int step = 1)
        {
            for (int i = a; i <= b; i += step)
                Directory.CreateDirectory(s + ((s == "") ? s : "\\") + i.ToString());
        }
    }

    /// <summary>
    /// Класс достижений в пауэрлифтинге
    /// </summary>
    public class PowerLift : IComparable
    {
        private double squat = 0, press = 0, lift = 0, weight = 0;
        public double Squat => squat;
        public double Lift => lift;
        public double Press => press;
        public double Weight => weight;
        private DateTime time = DateTime.Now;
        public DateTime Time => time;
        public double Sum => Squat + Press + Lift;
        public string Comment = "";
        public double Tonn = 0;
        private const double coef = 0.0333;
        public double SquatPercent => Math.Round(Squat / Sum * 100,2);
        public double PressPercent => Math.Round(Press / Sum * 100, 2);
        public double LiftPercent => Math.Round(Lift / Sum * 100, 2);
        public double SquatWeight => Math.Round(Squat / Weight, 3);
        public double PressWeight => Math.Round(Press / Weight, 3);
        public double LiftWeight => Math.Round(Lift / Weight, 3);

        /// <summary>
        /// Движение
        /// </summary>
        public enum Workout
        {
            Squat,
            Press,
            Lift
        }
        public PowerLift(double squat, double press, double lift)
        {
            this.squat = squat;
            this.lift = lift;
            this.press = press;
        }
        public PowerLift(double squat, double press, double lift, DateTime d) : this(squat, press, lift) { this.time = d; }
        public PowerLift(double squat, double press, double lift, double weight, DateTime d) : this(squat, press, lift, d) { this.weight = weight; }
        public PowerLift(PowerLift p) : this(p.squat, p.press, p.lift, p.weight, p.time) { this.Tonn = p.Tonn; }
        public PowerLift(Workout w, double val)
        {
            switch (w)
            {
                case Workout.Squat:
                    this.squat = val;
                    break;
                case Workout.Press:
                    this.press = val;
                    break;
                default:
                    this.lift = val;
                    break;
            }
        }
        /// <summary>
        /// Конструктор по движению, взятому весу и числу повторений
        /// </summary>
        /// <param name="w"></param>
        /// <param name="val"></param>
        /// <param name="set"></param>
        public PowerLift(Workout w, double val, int set) : this(w, val * (1 + set * coef)) { }
        public PowerLift(Workout w, double val, int set, DateTime t) : this(w, val, set) { this.time = t; }
        public static double PM(double val, int set)
        {
            if (set <= 1) return val;
            return val * (1 + set * coef);
        }
        public static double XPM(double PM, int set)
        {
            if (set <= 1) return PM;
            return PM / (1 + set * coef);
        }
        public static double Set(double PM, double val)
        {
            double e = ((PM - val) / coef / val);
            if (e < 1 && e > 0) e++;
            else if (e < 0) e = 0;

            return e;
        }

        public override string ToString()
        {
            return $"Date = {Time} \tSquat = {Squat} \tPress = {Press} \tLift = {Lift} \tWeight = {Weight} \tTonnage = {Tonn} \t Another: {Comment}";
        }

        public static void PrintInFile(StreamWriter s, PowerLift[] p)
        {
            for (int i = 0; i < p.Length - 1; i++)
                s.WriteLine(p[i]);
            s.Write(p[p.Length - 1]);
            s.Close();
        }
        public static PowerLift[] GetFromFile(StreamReader file)
        {
            var res = new List<PowerLift>();
            string s = ""; string com = "";
            s = file.ReadLine();
            try
            {
                if (s.IndexOf("Another: ") > 0)
                {
                    com = s.Substring(s.IndexOf("Another: ") + 8, s.Length - 8 - s.IndexOf("Another: "));
                    s = s.Substring(0, s.IndexOf("Another: "));

                }

            }
            finally
            {
                while (s != null)
                {
                    //s = file.ReadLine();                    
                    if (s.IndexOf("Another: ") > 0)
                    {
                        com = s.Substring(s.IndexOf("Another: ") + 8, s.Length - 8 - s.IndexOf("Another: "));
                        s = s.Substring(0, s.IndexOf("Another: "));

                    }
                    string[] st = s.Split(new string[] { " ", "Date", "=", "Squat", "Press", "Lift", "Weight", "Tonnage", "\t" /*, "Another: "*/ }, StringSplitOptions.RemoveEmptyEntries);

                    DateTime t = Convert.ToDateTime(st[0] + " " + st[1]);
                    double
                        sq = Convert.ToDouble(st[2]),
                        pr = Convert.ToDouble(st[3]),
                        li = Convert.ToDouble(st[4]),
                        we = Convert.ToDouble(st[5]),
                        tonn = Convert.ToDouble(st[6]);

                    //try { com = st[7]; }
                    //catch { com = ""; }

                    res.Add(new PowerLift(sq, pr, li, we, t));
                    res.Last().Comment = com;
                    res.Last().Tonn = tonn;
                    s = file.ReadLine();

                }
                file.Close();
            }

            return res.ToArray();
        }
        public static PowerLift[] Correct(PowerLift[] p)
        {
            PowerLift[] res = new PowerLift[p.Length];
            res[0] = new PowerLift(p[0]);
            for (int i = 1; i < p.Length; i++)
            {
                res[i] = new PowerLift(Math.Max(res[i - 1].squat, p[i].squat), Math.Max(res[i - 1].press, p[i].press), Math.Max(res[i - 1].lift, p[i].lift), Math.Max(p[i - 1].weight, p[i].weight), p[i].Time);
                //res[i].Tonn = Math.Max(p[i - 1].Tonn, p[i].Tonn);
                res[i].Tonn = p[i].Tonn;
            }

            return res.Where(n=>n.Tonn>0).ToArray();
        }

        public int CompareTo(object obj)
        {
            return Time.CompareTo(((PowerLift)obj).Time);
        }







        public static void Generate(int lenth, double force = 40, double mass = 40, double speed = 20, bool fo = true, bool ma = false, bool sp = false)
        {
            double sum = force + mass + speed;
            force = force / sum * 100;
            mass = mass / sum * 100;
            speed = speed / sum * 100;

            List<int[]> l = new List<int[]>();
            for (int i = 1; i <= lenth; i++)
            {
                int a = Rand(), b = Rand(i);
                if (fo && b <= force && a <= force) { i--; continue; }
                else if (ma && b <= force + mass && a <= force + mass) { i--; continue; }
                else if (sp && b > force + mass && a > force + mass) { i--; continue; }
                l.Add(new int[] { a, b });
                //Console.WriteLine(a + " " + b);
                Thread.Sleep(20);
            }

            for (int i = 0; i < lenth; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    int tmp = l[i][j];
                    if (tmp <= force) l[i][j] = 1;
                    else if (tmp <= force + mass) l[i][j] = 2;
                    //else if (tmp <= force + mass + speed) l[i][j] = 3;
                    else l[i][j] = 3;

                }
                //Console.WriteLine(l[i][0] + " " + l[i][1]);
            }

            //l.RemoveAll(n => n[0] == n[1] && (n[0] == 1 /*|| n[0] == 3*/));

            var t = ToEnum(l);
            for (int i = 0; i < l.Count; i++) Console.WriteLine(t[i][0] + " \t" + t[i][1]);

        }
        private static int Rand() { Random r = new Random(); return r.Next(1, 101); }
        private static int Rand(int i) { Random r = new Random(i); return r.Next(1, 101); }
        private static List<TypeOfTrain[]> ToEnum(List<int[]> t)
        {
            List<TypeOfTrain[]> r = new List<TypeOfTrain[]>();
            for (int i = 0; i < t.Count; i++)
            {
                TypeOfTrain a, b;
                a = b = TypeOfTrain.Force;
                if (t[i][0] == 1) a = TypeOfTrain.Force;
                else if (t[i][0] == 2) a = TypeOfTrain.Mass;
                else if (t[i][0] == 3) a = TypeOfTrain.Speed;
                if (t[i][1] == 1) b = TypeOfTrain.Force;
                else if (t[i][1] == 2) b = TypeOfTrain.Mass;
                else if (t[i][1] == 3) b = TypeOfTrain.Speed;
                r.Add(new TypeOfTrain[] { a, b });
            }
            return r;
        }
        private enum TypeOfTrain
        {
            Force,
            Mass,
            Speed
        };
    }
}
