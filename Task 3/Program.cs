namespace Task_3
{
    using System;
    using System.Collections.Generic;
    using Task_3.Resource;
    using Task_3.Tasks;
    using Task_3.Tasks.Task3_6;

    public class Program
    {
        private static List<Task> tasksList = new List<Task>
        {
            new Task3_1(),
            new Task3_2(),
            new Task3_3(),
            new Task3_4(),
            new Task3_5(),
            new Task3_6(),
            new Task3_7(),
            new Task3_8(),
            new Task3_9(),
            new Task3_10(),
            new Task3_11(),
            new Task3_12(),
            new Task3_13()
        };

        public static void Main(string[] args)
        {
            int input = 0;
            Console.WriteLine(Captions.SelectTask);
            do
            {
                for (int i = 0; i < tasksList.Count; i++)
                {
                    Console.WriteLine("{0,2} - Task 3.{1}", i + 1, i + 1);
                }

                Console.WriteLine(" {0}", Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
                if (input < 0 || input > tasksList.Count)
                {
                    Console.WriteLine(Captions.InputIsIncorrect);
                    continue;
                }
                else
                if (input > 0)
                {
                    Console.WriteLine(Captions.Separator);

                    Task task = tasksList[input - 1];
                    task.Start();

                    Console.WriteLine(Captions.Separator);
                    Console.WriteLine();
                }
            }
            while (input != 0);
        }
    }
}
