namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_9
    {
        private static int maxRandom = 50;
        private static int minRandom = -50;
        private static int maxArraySize = 20;
        private static int minArraySize = 1;

        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            Random rnd = new Random();
            int size = rnd.Next(minArraySize, maxArraySize);
            int[] array = new int[size];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(minRandom, maxRandom);
            }

            Console.WriteLine(Captions.ByRandom);
            Print(array);

            Console.WriteLine(string.Format("{0}: {1}", Captions.SumOfPos, SumOfPositive(array)));

            Console.WriteLine(Captions.Separator);
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
                Console.Write(string.Format("{0} ", a));
            }

            Console.WriteLine();
        }
    }
}
