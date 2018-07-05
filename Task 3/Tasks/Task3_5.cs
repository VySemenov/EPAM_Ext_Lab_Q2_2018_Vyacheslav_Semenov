namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_5
    {
        private static int n = 1000;
        private static int a = 3;
        private static int b = 5;

        /// <summary>
        /// Starting method of the task
        /// </summary>
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);
            Console.WriteLine(string.Format("The sum of all numbers of multiples of {0} and {1} and less than {2}:", a, b, n));
            Console.WriteLine(Sum(a) + Sum(b) - Sum(a * b));
            Console.WriteLine(Captions.Separator);
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
