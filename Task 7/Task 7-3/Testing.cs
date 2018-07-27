namespace Task_7_3
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Task_7_3.Program;
    using static Task_7_3.Searching;

    public class Testing
    {
        private const int NumIterations = 2000;
        private const int MaxArraySize = 20000;
        private const int MinRndValue = -10;
        private const int MaxRndValue = 10;
        private const int Step = 500;
        private static int arraySize = 20;

        public delegate bool SearchCondition(int arg);

        /// <summary>
        /// Search condition
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool Condition(int arg)
        {
            return arg > 0;
        }

        /// <summary>
        /// The method compares the time of work of different search methods and finds the fastest
        /// </summary>
        public static void Analysis()
        {
            int arraySizeBefore = arraySize;

            SearchCondition condDelegate = Condition;
            SearchCondition condAnonDelegate = delegate(int arg) { return arg > 0; };
            SearchCondition condLambda = (x) => x > 0;

            Console.WriteLine(
                "{0, -10} | {1, -10} | {2, -10} | {3, -15} | {4, -10} | {5, -10} | {6, -10}",
                Resource.ArraySize,
                Resource.Direct,
                Resource.Delegate,
                Resource.AnonDelegate,
                Resource.Lambda,
                Resource.Linq,
                Resource.Winner);

            for (int i = Step; i <= MaxArraySize; i += Step)
            {
                arraySize = i;

                double directTime = StartDirectlyTest();
                double delegateTime = StartDelegateTest(condDelegate);
                double anonTime = StartDelegateTest(condAnonDelegate);
                double lambdaTime = StartDelegateTest(condLambda);
                double linqTime = StartLinqTest();

                double[] array = new double[]
                {
                    directTime,
                    delegateTime,
                    anonTime,
                    lambdaTime,
                    linqTime
                };

                string winner = string.Empty;
                double min = array.Min();

                // Here the studio did not let me use the switch, so I'm a Hindu
                if (min == directTime)
                {
                    winner = Resource.Direct;
                }
                else
                if (min == delegateTime)
                {
                    winner = Resource.Delegate;
                }
                else
                if (min == anonTime)
                {
                    winner = Resource.AnonDelegate;
                }
                else
                if (min == lambdaTime)
                {
                    winner = Resource.Lambda;
                }
                else
                {
                    winner = Resource.Linq;
                }

                Console.WriteLine(
                    "{0, -10} | {1, -10} | {2, -10} | {3, -15} | {4, -10} | {5, -10} | {6, -20}",
                    i,
                    directTime,
                    delegateTime,
                    anonTime,
                    lambdaTime,
                    linqTime,
                    winner);
            }

            arraySize = arraySizeBefore;
        }

        /// <summary>
        /// Finds the average time of direct search
        /// </summary>
        /// <returns></returns>
        public static double StartDirectlyTest()
        {
            int[] array = new int[arraySize];
            FillArray(array);

            TimeSpan time = TimeSpan.Zero;

            for (int i = 0; i < NumIterations; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                Search(array);
                sw.Stop();
                time += sw.Elapsed;
            }

            return time.TotalMilliseconds / NumIterations;
        }

        /// <summary>
        /// Finds the average search time through the delegate
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public static double StartDelegateTest(SearchCondition cond)
        {
            int[] array = new int[arraySize];
            FillArray(array);

            TimeSpan time = TimeSpan.Zero;

            for (int i = 0; i < NumIterations; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                Search(array, cond);
                sw.Stop();
                time += sw.Elapsed;
            }

            return time.TotalMilliseconds / NumIterations;
        }

        /// <summary>
        /// Finds the average search time through LINQ
        /// </summary>
        /// <returns></returns>
        public static double StartLinqTest()
        {
            int[] array = new int[arraySize];
            FillArray(array);

            TimeSpan time = TimeSpan.Zero;

            for (int i = 0; i < NumIterations; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                SearchLinq(array);
                sw.Stop();
                time += sw.Elapsed;
            }

            return time.TotalMilliseconds / NumIterations;
        }

        /// <summary>
        /// Fills an array with random numbers
        /// </summary>
        /// <param name="array"></param>
        public static void FillArray(int[] array)
        {
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(MinRndValue, MaxRndValue);
            }
        }
    }
}
