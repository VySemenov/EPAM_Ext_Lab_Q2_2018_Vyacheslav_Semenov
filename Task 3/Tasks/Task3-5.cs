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

        /// <summary>
        /// Calculates the sum of an arithmetic progression
        /// </summary>
        /// <param name="a">Step of the arithmetic progression</param>
        /// <returns>The sum of the arithmetic progression</returns>
        private static int Sum(int a)
        {
            int an = (N-1) / a;
            return (2 * an * a - a * (an - 1)) * an / 2;
        }

        /// <summary>
        /// Starting method of the task
        /// </summary>
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);
            Console.WriteLine(Sum(a) + Sum(b) - Sum(a*b));
            Console.WriteLine(Captions.Separator);
        }
    }
}
