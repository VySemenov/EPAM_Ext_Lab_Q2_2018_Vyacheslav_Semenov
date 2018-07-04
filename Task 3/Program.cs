using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_3.Resource;
using Task_3.Tasks;

namespace Task_3
{
    class Program
    {
        private enum TasksNums
        {
            Task1 = 1,
            Task2,
            Task3,
            Task4,
            Task5
        }

        private static List<TasksNums> tasksList = new List<TasksNums>
        {
            TasksNums.Task1,
            TasksNums.Task2,
            TasksNums.Task3,
            TasksNums.Task4,
            TasksNums.Task5
        };

        static void Main(string[] args)
        {
            int input = 0;
            Console.WriteLine(Captions.SelectTask);
            do
            {
                foreach(var t in tasksList)
                {
                    Console.WriteLine(String.Format("{0} - Task 3.{1}", (int)t, (int)t));
                }
                Console.WriteLine(Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
                switch (input)
                {
                    case (int)TasksNums.Task1: Task3_1.Start(); break;
                    case (int)TasksNums.Task2: Task3_2.Start(); break;
                    case (int)TasksNums.Task3: Task3_3.Start(); break;
                    case (int)TasksNums.Task4: Task3_4.Start(); break;
                    case (int)TasksNums.Task5: Task3_5.Start(); break;
                }
            }
            while (input != 0);
        }
    }
}
