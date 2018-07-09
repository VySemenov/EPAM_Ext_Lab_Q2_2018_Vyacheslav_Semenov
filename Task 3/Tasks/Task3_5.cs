﻿namespace Task_3.Tasks
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
            Console.WriteLine(string.Format("The sum of all numbers of multiples of {0} and {1} and less than {2}:", a, b, n));//todo pn хардкод. в ресурсы.
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
