namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_11 : Task
    {
        private static char[] delimiters = Captions.Delimeters.ToCharArray();

        public override void Start()
        {
            Console.WriteLine("1 - {0}", Captions.EnterStr);
            Console.WriteLine("2 - {0}", Captions.UseDef);

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
                case 1: Console.Write("{0}: ", Captions.EnterStrPls);
                        str = Console.ReadLine();
                        break;
                case 2: Console.WriteLine(Captions.DefStr);
                        str = Captions.DefStr;
                        break;
            }

            Console.WriteLine("{0}: {1}", Captions.AvLen, GetAvl(str));
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
