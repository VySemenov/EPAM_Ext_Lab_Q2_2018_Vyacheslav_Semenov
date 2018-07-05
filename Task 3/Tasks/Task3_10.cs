namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_10
    {
        private static int maxRandom = 9;
        private static int minRandom = -9;
        private static int maxArraySize = 5;
        private static int minArraySize = 2;

        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            Random rnd = new Random();
            int x = rnd.Next(minArraySize, maxArraySize);
            int y = rnd.Next(minArraySize, maxArraySize);
            int[,] array = new int[x, y];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rnd.Next(minRandom, maxRandom);
                }
            }

            Console.WriteLine(Captions.ByRandom);

            Print(array);

            Console.WriteLine(string.Format("{0}: {1}", Captions.SumInEvenPos, Sum(array)));

            Console.WriteLine(Captions.Separator);
        }

        private static int Sum(int[,] array)
        {
            int sum = 0;

            for (int i = 0; i < array.GetLength(0); i += 2)
            {
                for (int j = 0; j < array.GetLength(1); j += 2)
                {
                    sum += array[i, j];
                }
            }

            for (int i = 1; i < array.GetLength(0); i += 2)
            {
                for (int j = 1; j < array.GetLength(1); j += 2)
                {
                    sum += array[i, j];
                }
            }

            return sum;
        }

        private static void Print(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(string.Format("{0} ", array[i, j]));
                }

                Console.WriteLine();
            }
        }
    }
}
