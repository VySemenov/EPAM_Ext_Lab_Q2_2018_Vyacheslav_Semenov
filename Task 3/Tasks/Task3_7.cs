namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_7
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
            Console.WriteLine(string.Format("{0}:", Captions.BeforeSort));
            Print(array);

            Sort(array);
            Console.WriteLine(string.Format("{0}:", Captions.AfterSort));
            Print(array);

            Console.WriteLine(string.Format("{0}: {1}", Captions.Min, FindMin(array)));
            Console.WriteLine(string.Format("{0}: {1}", Captions.Max, FindMax(array)));

            Console.WriteLine(Captions.Separator);
        }

        private static void Sort(int[] array)
        {
            int temp;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        private static int FindMax(int[] array)
        {
            int imax = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > array[imax])
                {
                    imax = i;
                }
            }

            return array[imax];
        }

        private static int FindMin(int[] array)
        {
            int imin = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < array[imin])
                {
                    imin = i;
                }
            }

            return array[imin];
        }

        private static void Print(int[] array)
        {
            foreach (var i in array)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}
