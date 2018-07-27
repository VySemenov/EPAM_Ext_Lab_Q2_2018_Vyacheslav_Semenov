namespace Task_7_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        private const int Size = 10;
        private const int MinValue = -10;
        private const int MaxValue = 10;

        public static void Main(string[] args)
        {
            Console.WriteLine("{0}", Resource.InitIntByRandom);
            Console.WriteLine("{0}:", Resource.GenArray);

            int[] array = new int[Size];
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(MinValue, MaxValue);
                Console.Write("{0} ", array[i]);
            }

            Console.WriteLine();

            Console.WriteLine("{0}: {1}", Resource.Sum, array.Sum());

            Console.ReadKey();
        }
    }
}
