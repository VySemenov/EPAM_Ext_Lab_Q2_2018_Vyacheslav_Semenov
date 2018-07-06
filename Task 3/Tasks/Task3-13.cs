namespace Task_3.Tasks
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Task_3.Resource;

    public class Task3_13 : Task
    {
        private static int maxIterations = 10000;
        private static int maxConcat = 200;
        private static int analysisMaxStep = 20;
        private static int analysisMinStep = 1;
        private static int maxConcatMinStep = 20;

        public override void Start()
        {
            int input = 0;
            do
            {
                Console.WriteLine("1 - {0}", Captions.DefTest);
                Console.WriteLine("2 - {0}", Captions.CustomTest);
                Console.WriteLine("3 - Analysis");
                Console.WriteLine(Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
            }
            while (input < 0 || input > 3);

            int numIterations = 0;
            int numConcat = 0;
            switch (input)
            {
                case 0: return;
                case 1: TestStart();
                        break;
                case 2:
                    Console.Write("{0}: ", Captions.EnterNumIter);
                    int.TryParse(Console.ReadLine(), out numIterations);
                    Console.Write("{0}: ", Captions.EnterNumConcat);
                    int.TryParse(Console.ReadLine(), out numConcat);
                    if (numIterations <= 0 || numConcat <= 0 || numIterations > maxIterations || numConcat > maxConcat)
                    {
                        Console.WriteLine(Captions.InputIsIncorrect);
                        break;
                    }

                    TestStart(numIterations, numConcat);
                    break;
                case 3: Analysis();
                        break;
            }
        }

        /// <summary>
        /// Conducts a series of tests for different values of the number of concatenations.
        /// </summary>
        private static void Analysis()
        {
            double concatStrAvgTime = 0;
            double concatSbAvgTime = 0;
            double ratio = 0;

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -27} | {3, -10} | {4, -10}", 
                Captions.NumConcat, 
                Captions.StrAvgTimeStlb, 
                Captions.StrAvgTimeStlb, 
                Captions.Ratio, 
                Captions.Winner);

            for (int i = analysisMinStep; i <= maxConcatMinStep; i += analysisMinStep)
            {
                concatStrAvgTime = StrTest(maxIterations, i) / maxIterations;
                concatSbAvgTime = SbTest(maxIterations, i) / maxIterations;
                ratio = concatStrAvgTime / concatSbAvgTime;

                string winner = string.Empty;
                if (ratio > 1)
                {
                    winner = Captions.StringBuilder;
                }
                else
                if (ratio < 1)
                {
                    winner = Captions.String;
                }
                else
                {
                    winner = Captions.Draw;
                }

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -27} | {3, -10:0.000} | {4, -10}",
                    i, 
                    concatStrAvgTime, 
                    concatSbAvgTime, 
                    ratio, 
                    winner);
            }

            for (int i = analysisMaxStep + maxConcatMinStep; i <= maxConcat; i += analysisMaxStep)
            {
                concatStrAvgTime = StrTest(maxIterations, i) / maxIterations;
                concatSbAvgTime = SbTest(maxIterations, i) / maxIterations;
                ratio = concatStrAvgTime / concatSbAvgTime;

                string winner = string.Empty;
                if (ratio > 1)
                {
                    winner = Captions.StringBuilder;
                }
                else
                if (ratio < 1)
                {
                    winner = Captions.String;   
                }
                else
                {
                    winner = Captions.Draw;
                }

                Console.WriteLine(
                    "{0, -10} | {1, -20} | {2, -27} | {3, -10:0.000} | {4, -10}", 
                    i, 
                    concatStrAvgTime, 
                    concatSbAvgTime, 
                    ratio, 
                    winner);
            }
        }

        /// <summary>
        /// Conducts a series of tests for a specific value of the number of concatenations.
        /// </summary>
        /// <param name="numIterations"></param>
        /// <param name="numConcat"></param>
        private static void TestStart(int numIterations = 1000, int numConcat = 100)
        {
            double concatStrAvgTime = StrTest(numIterations, numConcat) / numIterations;
            double concatSbAvgTime = SbTest(numIterations, numConcat) / numIterations;

            Console.WriteLine("{0}: {1}", Captions.StrAvgTime, concatStrAvgTime);
            Console.WriteLine("{0}: {1}", Captions.SbAvgTime, concatSbAvgTime);

            double ratio = concatStrAvgTime / concatSbAvgTime;
            Console.WriteLine("{0}: {1}", Captions.Ratio, ratio);

            if (ratio > 1)
            {
                Console.WriteLine("{0} - {1}", Captions.Winner, Captions.StringBuilder);
            }
            else
            if (ratio < 1)
            {
                Console.WriteLine("{0} - {1}", Captions.Winner, Captions.String);
            }
            else
            {
                Console.WriteLine("{0}", Captions.Draw);
            }
        }

        private static double StrTest(int numIterations, int numConcat)
        {
            string str = string.Empty;
            TimeSpan time = TimeSpan.Zero;

            for (int i = 0; i < numIterations; i++)
            {
                str = string.Empty;
                Stopwatch sw = Stopwatch.StartNew();
                for (int j = 0; j < numConcat; j++)
                {
                    str += "*";
                }

                sw.Stop();
                time += sw.Elapsed;
            }

            return time.TotalMilliseconds;
        }

        private static double SbTest(int numIterations, int numConcat)
        {
            StringBuilder sb;
            TimeSpan time = TimeSpan.Zero;

            for (int i = 0; i < numIterations; i++)
            {
                sb = new StringBuilder();
                Stopwatch sw = Stopwatch.StartNew();
                for (int j = 0; j < numConcat; j++)
                {
                    sb.Append("*");
                }

                sw.Stop();
                time += sw.Elapsed;
            }

            return time.TotalMilliseconds;
        }
    }
}
