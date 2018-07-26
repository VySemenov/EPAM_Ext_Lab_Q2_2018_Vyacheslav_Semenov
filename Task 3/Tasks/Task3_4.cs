namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_4 : Task
    {
        private static char symbol = '*';
        private static char space = ' ';

        public override void Start()
        {
            int n = 0;

            while (n <= 0)
            {
                Input(out n);
                if (n <= 0)
                {
                    Console.WriteLine(Captions.InputIsIncorrect);
                }
            }

            for (int k = 1; k <= n; k++)
            {
                int j = 1;
                for (int i = 1; i <= k; i++)
                {
                    Console.Write(new string(space, n - i));
                    Console.WriteLine(new string(symbol, j));
                    j += 2;
                }
            }
        }

        private static void Input(out int n)
        {
            Console.WriteLine(Captions.NumInputRequest);
            Console.Write("N: ");
            int.TryParse(Console.ReadLine(), out n);
        }
    }
}
