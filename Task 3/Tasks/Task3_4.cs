namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_4
    {
        /// <summary>
        /// Starting method of the task
        /// </summary>
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            int n = 0;
            Input(out n);

            for (int k = 1; k <= n; k++)
            {
                int j = 1;
                for (int i = 1; i <= k; i++)
                {
                    Console.Write(new string(' ', n - i));
                    Console.WriteLine(new string('*', j));
                    j += 2;
                }
            }

            Console.WriteLine(Captions.Separator);
        }

        private static void Input(out int n)
        {
            Console.WriteLine(Captions.NumInputRequest);
            Console.Write("N: ");
            int.TryParse(Console.ReadLine(), out n);
        }
    }
}
