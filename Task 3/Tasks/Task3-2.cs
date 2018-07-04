using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.Tasks
{
    class Task3_2
    {
        public static void start()
        {
            Console.WriteLine("Enter the number");
            Console.Write("N: ");
            int n;
            int.TryParse(Console.ReadLine(), out n);

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(new string('*', i));
            }
        }
    }
}
