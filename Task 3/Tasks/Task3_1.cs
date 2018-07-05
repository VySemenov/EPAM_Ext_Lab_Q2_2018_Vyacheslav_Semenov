namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_1
    {
        /// <summary>
        /// Starting method of the task
        /// </summary>
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            int a = 0;
            int b = 0;
            Input(out a, out b);

            if (a <= 0 || b <= 0)
            {
                Console.WriteLine(Captions.InputIsIncorrect);
            }
            else
            {
                Console.WriteLine(string.Format("{0}: {1}", Captions.RectangleArea, a * b));
            }

            Console.WriteLine(Captions.Separator);
        }

        private static void Input(out int a, out int b)
        {
            Console.WriteLine(Captions.NumsInputRequest);
            Console.Write("a: ");
            int.TryParse(Console.ReadLine(), out a);
            Console.Write("b: ");
            int.TryParse(Console.ReadLine(), out b);
        }
    }
}
