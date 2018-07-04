using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Resource;

namespace Task_3.Tasks
{
    class Task3_1
    {
        public static void start()
        {
            Console.WriteLine(Captions.Separator);

            Console.WriteLine(Captions.NumsInputRequest);
            Console.Write("a: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Write("b: ");
            int b;
            int.TryParse(Console.ReadLine(), out b);
            if(a <= 0 || b <= 0)
            {
                Console.WriteLine(Captions.InputIsIncorrect);
            }
            else
            {
                Console.WriteLine(Captions.RectangleArea + ": " + a*b);
            }

            Console.WriteLine(Captions.Separator);

        }

    }
}
