namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_2
    {
        /// <summary>
        /// Starting method of the task
        /// </summary>
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            int n = 0;
            Input(out n);

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine(new string('*', i));
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
