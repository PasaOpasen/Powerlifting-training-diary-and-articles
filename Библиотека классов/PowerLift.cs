using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Библиотека_классов
{
    /// <summary>
    /// Класс достижений в пауэрлифтинге
    /// </summary>
    public struct PowerLift : IComparable
    {
        public double Squat { get; }
        public double Lift { get; }
        public double Press { get; } 
        public double Weight { get; }
        public DateTime Time { get; }
        public double Sum => Squat + Press + Lift;
        public string Comment;
        public double Tonnage;
        private const double coef = 0.0333;
        public double SquatPercent => Math.Round(Squat / Sum * 100, 2);
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
            Tonnage = 0;
            Weight = 0;
            Time = DateTime.Now;
            Comment = "";
            this.Squat = squat;
            this.Lift = lift;
            this.Press = press;
        }
        public PowerLift(double squat, double press, double lift, DateTime d) : this(squat, press, lift) { this.Time = d; }
        public PowerLift(double squat, double press, double lift, double weight, DateTime d) : this(squat, press, lift, d) { this.Weight = weight; }
        public PowerLift(double squat, double press, double lift, double weight, DateTime d, double tonnage, string comment) : this(squat, press, lift, weight, d)
        {
            this.Comment = comment;
            this.Tonnage = tonnage;
        }
        public PowerLift(PowerLift p) : this(p.Squat, p.Press, p.Lift, p.Weight, p.Time) { this.Tonnage = p.Tonnage; }
        public PowerLift(Workout w, double val)
        {
            this.Squat = 0;
            this.Lift = 0;
            this.Press = 0;
            Tonnage = 0;
            Weight = 0;
            Time = DateTime.Now;
            Comment = "";

            switch (w)
            {
                case Workout.Squat:
                    this.Squat = val;
                    break;
                case Workout.Press:
                    this.Press = val;
                    break;
                default:
                    this.Lift = val;
                    break;
            }
        }
        /// <summary>
        /// Конструктор по движению, взятому весу и числу повторений
        /// </summary>
        /// <param name="w"></param>
        /// <param name="val"></param>
        /// <param name="set"></param>
        public PowerLift(Workout w, double val, int set) : this(w, PM(val, set)) { }

        public PowerLift(Workout w, double val, int set, DateTime t) : this(w, val, set) { this.Time = t; }

        /// <summary>
        /// Вычисляет повторный максимум по данным val * set
        /// </summary>
        /// <param name="val"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static double PM(double val, int set)
        {
            if (set <= 1) return val;
            //tex: $\max = result \cdot (1+count \cdot 0.0333)$
            return val * (1.0 + set * coef);
        }
        /// <summary>
        /// Вычисляет наибольший вес по повторному максимуму и нужному числу повторений
        /// </summary>
        /// <param name="PM"></param>
        /// <param name="set"></param>
        /// <returns></returns>
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
            return $"Date = {Time} \tSquat = {Squat} \tPress = {Press} \tLift = {Lift} \tWeight = {Weight} \tTonnage = {Tonnage} \tAnother: {Comment}";
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

            void Setscom()
            {
                if (s.IndexOf("Another: ") > 0)
                {
                    com = s.Substring(s.IndexOf("Another: ") + 8, s.Length - 8 - s.IndexOf("Another: "));
                    s = s.Substring(0, s.IndexOf("Another: "));
                }
            }

            try
            {
                Setscom();
            }
            finally
            {
                while (s != null)
                {
                    Setscom();
                    string[] st = s.Split(new string[] { " ", "Date", "=", "Squat", "Press", "Lift", "Weight", "Tonnage", "\t" }, StringSplitOptions.RemoveEmptyEntries);

                    DateTime t = Convert.ToDateTime(st[0] + " " + st[1]);
                    double
                        sq = Convert.ToDouble(st[2]),
                        pr = Convert.ToDouble(st[3]),
                        li = Convert.ToDouble(st[4]),
                        we = Convert.ToDouble(st[5]),
                        tonn = Convert.ToDouble(st[6]);

                    res.Add(new PowerLift(sq, pr, li, we, t, tonn, com));
                    s = file.ReadLine();
                }
                file.Close();
            }

            return res.ToArray();
        }
        /// <summary>
        /// Откорректировать массив (чтобы результаты не падали и пропуски заполнялись предыдущими результатами)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PowerLift[] Correct(PowerLift[] p)
        {
            PowerLift[] res = new PowerLift[p.Length];
            res[0] = new PowerLift(p[0]);
            for (int i = 1; i < p.Length; i++)
            {
                ref var rs = ref res[i];
                ref var rs0 = ref res[i-1];
                ref var pp = ref p[i];

                rs = new PowerLift(Math.Max(rs0.Squat, pp.Squat), Math.Max(rs0.Press, pp.Press), Math.Max(rs0.Lift, pp.Lift), Math.Max(p[i - 1].Weight, pp.Weight), pp.Time);
                rs.Tonnage = pp.Tonnage;
            }
            return res.Where(n => n.Tonnage > 0).ToArray();
        }

        public int CompareTo(object obj) => CompareTo((PowerLift)obj);
        public int CompareTo(PowerLift p) => Time.CompareTo(p.Time);

        public override bool Equals(object obj)
        {
            return obj is PowerLift lift &&                                           
                   Tonnage == lift.Tonnage&&
                   Time == lift.Time && 
                  Squat == lift.Squat &&
                   Lift == lift.Lift &&
                   Press == lift.Press &&
                   Weight == lift.Weight ;
        }

        public override int GetHashCode()
        {
            var hashCode = 568324721;
            hashCode = hashCode * -1521134295 + Squat.GetHashCode();
            hashCode = hashCode * -1521134295 + Lift.GetHashCode();
            hashCode = hashCode * -1521134295 + Press.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            hashCode = hashCode * -1521134295 + Tonnage.GetHashCode();
            return hashCode;
        }

#if DEBUG
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
#endif
    }
}
