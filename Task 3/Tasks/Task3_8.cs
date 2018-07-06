namespace Task_3.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Task_3.Resource;

    public class Task3_8 : Task
    {
        private static int maxRandom = 50;
        private static int minRandom = -50;
        private static int maxArraySize = 4;
        private static int minArraySize = 2;

        public override void Start()
        {
            Random rnd = new Random();
            int x = rnd.Next(minArraySize, maxArraySize);
            int y = rnd.Next(minArraySize, maxArraySize);
            int z = rnd.Next(minArraySize, maxArraySize);
            int[,,] array = new int[x, y, z];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        array[i, j, k] = rnd.Next(minRandom, maxRandom);
                    }
                }
            }

            Console.WriteLine(Captions.ByRandom);
            Console.WriteLine(Captions.PrintByLayers);
            Console.WriteLine(Captions.BeforeRep);
            Print(array);

            ReplaceAllNegative(array);
            Console.WriteLine(Captions.AfterRep);
            Print(array);
        }

        /// <summary>
        /// Displays a three-dimensional array "on layers"
        /// </summary>
        /// <param name="array">Three-dimensional array</param>
        private static void Print(int[,,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.WriteLine("{0} layer", i);
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        Console.Write("{0} ", array[i, j, k]);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Replaces all negative numbers in a three-dimensional array with a new value
        /// </summary>
        /// <param name="array">Three-dimensional array</param>
        /// <param name="substitute">New value</param>
        private static void ReplaceAllNegative(int[,,] array, int substitute = 0)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        if (array[i, j, k] < 0)
                        {
                            array[i, j, k] = substitute;
                        }
                    }
                }
            }
        }
    }
}
