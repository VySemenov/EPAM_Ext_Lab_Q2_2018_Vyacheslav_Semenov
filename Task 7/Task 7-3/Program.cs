namespace Task_7_3
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Task_7_3.Searching;
    using static Task_7_3.Testing;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("== {0} ==", Resource.Hello);
            Console.WriteLine(Resource.JustLook);
            Console.WriteLine(Resource.Click);
            Console.ReadKey();
            Console.WriteLine(Resource.TakeTime);

            Analysis();

            Console.WriteLine("{0}.", Resource.ThatsAll);
            Console.WriteLine("{0}!", Resource.SeeYou);
            Console.ReadKey();
        }
    }
}
