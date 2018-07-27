namespace Task_7_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("{0}:", Resource.EnterStr);

            string str = Console.ReadLine();

            Console.WriteLine("{0}: {1}", Resource.Result, str.IsPosInt());

            Console.WriteLine("{0}! {1}.", Resource.ThatsAll, Resource.SeeYou);
            Console.ReadKey();
        }
    }
}
