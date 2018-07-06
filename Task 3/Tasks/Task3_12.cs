namespace Task_3.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Task_3.Resource;

    public class Task3_12 : Task
    {
        public override void Start()
        {
            string ResultStr = string.Empty;
            Console.Write("{0} 1: ", Captions.EnterStr);
            string FirstStr = Console.ReadLine();
            Console.Write("{0} 2: ", Captions.EnterStr);
            string SecondStr = Console.ReadLine();

            foreach (char ch in FirstStr)
            {
                if (!SecondStr.Contains(ch))
                {
                    ResultStr += ch;
                }
                else
                {
                    ResultStr += ch;
                    ResultStr += ch;
                }
            }

            Console.WriteLine("{0}: {1}", Captions.Result, ResultStr);
        }
    }
}
