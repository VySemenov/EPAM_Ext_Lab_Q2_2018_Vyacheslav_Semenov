using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Resource;

namespace Task_3.Tasks
{
    class Task3_5
    {
        private static int N = 1000;
        private static int a = 3;
        private static int b = 5;

        private static int sum(int a)
        {
            int an = (N-1) / a;
            return (2 * an * a - a * (an - 1)) * an / 2;
        }

        public static void start()
        {
            Console.WriteLine(Captions.Separator);
            Console.WriteLine(sum(a) + sum(b) - sum(a*b));
            Console.WriteLine(Captions.Separator);
        }
    }
}
