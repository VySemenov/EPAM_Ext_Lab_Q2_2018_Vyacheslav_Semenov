using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.Tasks
{
    class Task3_4
    {
        public static void start()
        {
            Console.WriteLine("Enter the number");
            Console.Write("N: ");
            int n;
            int.TryParse(Console.ReadLine(), out n);

            for (int k = 1; k <= n; k++)
            {
                int j = 1;
                for (int i = 1; i <= k; i++)
                {
                    Console.Write(new string(' ', n - i));
                    Console.WriteLine(new string('*', j));
                    j += 2;
                }
            }
        }
    }
}
