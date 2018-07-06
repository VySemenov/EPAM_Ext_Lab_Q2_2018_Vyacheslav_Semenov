namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_9 : Task
    {
        private static int maxRandom = 50;
        private static int minRandom = -50;
        private static int maxArraySize = 20;
        private static int minArraySize = 1;

        public override void Start()
        {
            Random rnd = new Random();
            int size = rnd.Next(minArraySize, maxArraySize);
            int[] array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(minRandom, maxRandom);
            }

            Console.WriteLine(Captions.ByRandom);
            Print(array);

            Console.WriteLine("{0}: {1}", Captions.SumOfPos, SumOfPositive(array));
        }

        private static int SumOfPositive(int[] array)
        {
            int sum = 0;
            foreach (var a in array)
            {
                if (a > 0)
                {
                    sum += a;
                }
            }

            return sum;
        }

        private static void Print(int[] array)
        {
            foreach (var a in array)
            {
                Console.Write("{0} ", a);
            }

            Console.WriteLine();
        }
    }
}
