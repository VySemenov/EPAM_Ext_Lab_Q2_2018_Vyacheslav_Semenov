namespace Task_3.Tasks
{
    using System;
    using Task_3.Resource;

    public class Task3_1 : Task
    {
        public override void Start()
        {
            int a = 0;
            int b = 0;

            while(a <= 0 || b <= 0)
            {
                Input(out a, out b);
                if(a <= 0 || b <= 0)
                {
                    Console.WriteLine(Captions.InputIsIncorrect);
                }
            }

            Console.WriteLine("{0}: {1}", Captions.RectangleArea, a * b);
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
