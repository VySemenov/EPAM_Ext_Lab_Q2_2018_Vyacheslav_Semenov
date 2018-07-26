namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_7 : Task
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
            Console.WriteLine("{0}:", Captions.BeforeSort);
            Print(array);

            Sort(array);
            Console.WriteLine("{0}:", Captions.AfterSort);
            Print(array);

            Console.WriteLine("{0}: {1}", Captions.Min, FindMin(array));
            Console.WriteLine("{0}: {1}", Captions.Max, FindMax(array));
        }

        private static void Sort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int pivot = Partition(array, start, end);
            QuickSort(array, start, pivot - 1);
            QuickSort(array, pivot + 1, end);
        }

        private static int Partition(int[] array, int start, int end)
        {
            int temp;
            int marker = start;
            for (int i = start; i <= end; i++)
            {
                if (array[i] < array[end])
                {
                    temp = array[marker];
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                }
            }
            
            temp = array[marker];
            array[marker] = array[end];
            array[end] = temp;
            return marker;
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
