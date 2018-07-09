namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_5 : Task
    {
        private static int n = 1000;
        private static int a = 3;
        private static int b = 5;

        public override void Start()
        {
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}:", Captions.SumAllNumMult, a, Captions.and, b, Captions.and, Captions.lessThan, n);
            Console.WriteLine(Sum(a) + Sum(b) - Sum(a * b));
        }

        /// <summary>
        /// Calculates the sum of an arithmetic progression
        /// </summary>
        /// <param name="d">Step of the arithmetic progression</param>
        /// <returns>The sum of the arithmetic progression</returns>
        private static int Sum(int d)
        {
            int an = (n - 1) / d;
            return (((2 * (an * d)) - (d * (an - 1))) * an) / 2;
        }
    }
}
