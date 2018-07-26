namespace Task_6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Task_6.Program;

    public class Sorting
    {
        private static Random pivotRng = new Random();

        public static void Sort(GetStr getStr, SortMethod sortMethod)
        {
            string[] items = getStr();
            QuickSort(items, 0, items.Length - 1, sortMethod);
            Console.WriteLine("== {0} ==", Resource.Result);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private static void QuickSort(string[] items, int left, int right, SortMethod sortMethod)
        {
            if (left < right)
            {
                int pivotIndex = pivotRng.Next(left, right);
                int newPivot = Partition(items, left, right, pivotIndex, sortMethod);

                QuickSort(items, left, newPivot - 1, sortMethod);
                QuickSort(items, newPivot + 1, right, sortMethod);
            }
        }

        private static int Partition(string[] items, int left, int right, int pivotIndex, SortMethod sortMethod)
        {
            string pivotValue = items[pivotIndex];

            Swap(items, pivotIndex, right);

            int storeIndex = left;

            for (int i = left; i < right; i++)
            {
                if (sortMethod(items[i], pivotValue))
                {
                    Swap(items, i, storeIndex);
                    storeIndex += 1;
                }
            }

            Swap(items, storeIndex, right);
            return storeIndex;
        }

        private static void Swap(string[] items, int left, int right)
        {
            if (left != right)
            {
                string temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }
    }
}
