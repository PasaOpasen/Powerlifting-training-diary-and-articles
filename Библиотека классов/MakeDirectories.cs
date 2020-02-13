using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Библиотека_классов
{
    public static class MakeDirectories
    {
        public static void Make(int a, int b, string s = "", int step = 1)
        {
            for (int i = a; i <= b; i += step)
                Directory.CreateDirectory(s + (( s.Equals(string.Empty)) ? s : "\\") + i.ToString());
        }
    }
}
