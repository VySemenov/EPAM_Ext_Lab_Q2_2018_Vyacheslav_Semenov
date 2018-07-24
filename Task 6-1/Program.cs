namespace Task_6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Task_6.Sorting;

    public class Program
    {
        public delegate string[] GetStr();

        public delegate bool SortMethod(string str1, string str2);

        public static void Main(string[] args)
        {
            Console.WriteLine("== {0} ==", Resource.Hello);
            Console.WriteLine("{0} ({1})", Resource.WantToEnter, Resource.YesNo);

            string choice = Console.ReadLine().ToLower();

            while (!choice.Equals("yes") && !choice.Equals("no"))
            {
                Console.WriteLine("{0} {1}", Resource.IncorrectInput, Resource.TryAgain);
                choice = Console.ReadLine();
            }

            GetStr getStr;
            SortMethod sortMethod = CompareStr;
            switch (choice)
            {
                case "yes":
                    getStr = UseInput;
                    Sort(getStr, sortMethod);
                    break;
                case "no":
                    getStr = UseDefault;
                    Sort(getStr, sortMethod);
                    break;
                default: break;
            }

            Console.WriteLine("== {0} ==", Resource.SeeYou);
            Console.ReadKey();
        }

        public static bool CompareStr(string str1, string str2)
        {
            if (str1.Length == str2.Length)
            {
                return string.Compare(str1, str2) < 0;
            }

            if (str1.Length > str2.Length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string[] UseInput()
        {
            Console.Write("{0}: ", Resource.EnterNumber);
            int size;
            int.TryParse(Console.ReadLine(), out size);
            while (size <= 0)
            {
                Console.WriteLine("{0} {1}", Resource.IncorrectInput, Resource.TryAgain);
                int.TryParse(Console.ReadLine(), out size);
            }

            string[] strings = new string[size];

            Console.WriteLine("{0}:", Resource.EnterStr);
            for (int i = 0; i < size; i++)
            {
                strings[i] = Console.ReadLine();
            }

            Console.WriteLine("== {0} ==", Resource.InputSuccess);
            Console.WriteLine();

            return strings;
        }

        public static string[] UseDefault()
        {
            Console.WriteLine("{0}", Resource.DontComplain);

            return new string[] 
            {
                "E",
                "The cat says meow",
                "A",
                "C",
                "Z",
                "Cat on the street",
                "B",
                "ABC",
                "CAB",
                "Cat at home",
            };
        }
    }
}
