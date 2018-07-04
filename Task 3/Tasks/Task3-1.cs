using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3.Tasks
{
    class Task3_1
    {
        public static void start()
        {
            Console.WriteLine("Enter numbers");
            Console.Write("a: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Write("b: ");
            int b;
            int.TryParse(Console.ReadLine(), out b);
            if(a <= 0 || b <= 0)
            {
                Console.WriteLine("Введены некорректные значения");
            }
            else
            {
                Console.WriteLine("Площадь прямоугольника: " + a*b);
            }

        }

    }
}
