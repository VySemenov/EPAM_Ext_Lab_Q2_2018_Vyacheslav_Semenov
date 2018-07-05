namespace Task_3
{
    using System;
    using System.Collections.Generic;
    using Task_3.Resource;
    using Task_3.Tasks;
    using Task_3.Tasks.Task3_6;

    /// <summary>
    /// Starting class for Task 3
    /// </summary>
    public class Program
    {
        private static List<TasksNums> tasksList = new List<TasksNums>
        {
            TasksNums.Task1,
            TasksNums.Task2,
            TasksNums.Task3,
            TasksNums.Task4,
            TasksNums.Task5,
            TasksNums.Task6,
            TasksNums.Task7,
            TasksNums.Task8,
            TasksNums.Task9,
            TasksNums.Task10,
            TasksNums.Task11
        };

        private enum TasksNums
        {
            Task1 = 1,
            Task2,
            Task3,
            Task4,
            Task5,
            Task6,
            Task7,
            Task8,
            Task9,
            Task10,
            Task11
        }

        public static void Main(string[] args)
        {
            int input = 0;
            Console.WriteLine(Captions.SelectTask);
            do
            {
                foreach (var t in tasksList)
                {
                    Console.WriteLine(string.Format("{0} - Task 3.{1}", (int)t, (int)t));
                }

                Console.WriteLine(Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
                switch (input)
                {
                    case (int)TasksNums.Task1: Task3_1.Start();
                                               break;
                    case (int)TasksNums.Task2: Task3_2.Start();
                                               break;
                    case (int)TasksNums.Task3: Task3_3.Start();
                                               break;
                    case (int)TasksNums.Task4: Task3_4.Start();
                                               break;
                    case (int)TasksNums.Task5: Task3_5.Start();
                                               break;
                    case (int)TasksNums.Task6: Task3_6.Start();
                                               break;
                    case (int)TasksNums.Task7: Task3_7.Start();
                                               break;
                    case (int)TasksNums.Task8: Task3_8.Start();
                                               break;
                    case (int)TasksNums.Task9: Task3_9.Start();
                                               break;
                    case (int)TasksNums.Task10: Task3_10.Start();
                                                break;
                    case (int)TasksNums.Task11: Task3_11.Start();
                                                break;
                }
            }
            while (input != 0);
        }
    }
}
