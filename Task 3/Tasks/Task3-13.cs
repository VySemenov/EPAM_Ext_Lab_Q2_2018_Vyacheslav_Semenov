namespace Task_3.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Task_3.Resource;

    public class Task3_13 : Task
    {
        public override void Start()
        {
            int input = 0;
            do
            {
                Console.WriteLine("1 - {0}", Captions.DefTest);
                Console.WriteLine("2 - {0}", Captions.CustomTest);
                Console.WriteLine(Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
            }
            while (input < 0 || input > 2);

            int numIterations = 0;
            int numConcat = 0;
            switch(input)
            {
                case 0: return;
                case 1: TestStart();  break;
                case 2:
                    Console.Write("{0}: ", Captions.EnterNumIter);
                    int.TryParse(Console.ReadLine(), out numIterations);
                    Console.Write("{0}: ", Captions.EnterNumConcat);
                    int.TryParse(Console.ReadLine(), out numConcat);
                    TestStart(numIterations, numConcat);
                    break;
            }
        }

        private static void TestStart(int numIterations = 1000, int numConcat = 100)
        {
            double strConcatAv = StrTest(numIterations, numConcat) / numIterations;
            double sbConcatAv = SbTest(numIterations, numConcat) / numIterations;

            Console.WriteLine("{0}: {1}", Captions.StrAvgTime, strConcatAv);
            Console.WriteLine("{0}: {1}", Captions.SbAvgTime, sbConcatAv);

            //Console.WriteLine("StringBuilder ");
        }

        private static double StrTest(int numIterations, int numConcat)
        {
            string str = string.Empty;
            TimeSpan strTime = TimeSpan.Zero;

            for (int i = 0; i < numIterations; i++)
            {
                str = string.Empty;
                Stopwatch sw = Stopwatch.StartNew();
                for (int j = 0; j < numConcat; j++)
                {
                    str += "*";
                }
                sw.Stop();
                strTime += sw.Elapsed;
            }

            return strTime.TotalMilliseconds;
        }

        private static double SbTest(int numIterations, int numConcat)
        {
            StringBuilder sb;
            TimeSpan sbTime = TimeSpan.Zero;

            for (int i = 0; i < numIterations; i++)
            {
                sb = new StringBuilder();
                Stopwatch sw = Stopwatch.StartNew();
                for (int j = 0; j < numConcat; j++)
                {
                    sb.Append("*");
                }
                sw.Stop();
                sbTime += sw.Elapsed;
            }

            return sbTime.TotalMilliseconds;
        }
    }
}
