namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_11
    {
        private static char[] delimiters = { ' ', '.', ',', '/', '?', '\"', '*', '!', '\t', ':' };

        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            Console.WriteLine(string.Format("1 - {0}", Captions.EnterStr));
            Console.WriteLine(string.Format("2 - {0}", Captions.UseDef));

            int input = 2;
            do
            {
                int.TryParse(Console.ReadLine(), out input);
                if (input < 1 || input > 2)
                {
                    Console.WriteLine(Captions.InputIsIncorrect);
                }
            }
            while (input < 1 || input > 2);
            
            string str = string.Empty;
            switch (input)
            {
                case 1: Console.Write(string.Format("{0}: ", Captions.EnterStrPls));
                        str = Console.ReadLine();
                        break;
                case 2: Console.WriteLine(Captions.DefStr);
                        str = Captions.DefStr;
                        break;
            }

            Console.WriteLine(string.Format("{0}: {1}", Captions.AvLen, GetAvl(str)));

            Console.WriteLine(Captions.Separator);
        }

        private static double GetAvl(string str)
        {
            string[] substring = str.Split(delimiters);

            double length = 0;
            foreach (var s in substring)
            {
                length += s.Length;
            }

            return length / substring.Length;
        }
    }
}
