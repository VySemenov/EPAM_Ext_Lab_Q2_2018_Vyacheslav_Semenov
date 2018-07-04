using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Resource;

namespace Task_3.Tasks
{
    class Task3_3
    {
        /// <summary>
        /// Starting method of the task
        /// </summary>
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            Console.WriteLine(Captions.NumInputRequest);
            Console.Write("N: ");
            int n;
            int.TryParse(Console.ReadLine(), out n);

            int j = 1;
            for (int i = 1; i <= n; i++)
            {
                Console.Write(new string(' ', n - i));
                Console.WriteLine(new string('*', j));
                j += 2;
            }

            Console.WriteLine(Captions.Separator);
        }
    }
}
