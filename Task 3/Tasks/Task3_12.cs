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
            string resultStr = string.Empty;
            Console.Write("{0} 1: ", Captions.EnterStr);
            string firstStr = Console.ReadLine();
            Console.Write("{0} 2: ", Captions.EnterStr);
            string secondStr = Console.ReadLine();

            foreach (char ch in firstStr)
            {
                if (!secondStr.Contains(ch))
                {
                    resultStr += ch;
                }
                else
                {
                    resultStr += ch;
                    resultStr += ch;
                }
            }

            Console.WriteLine("{0}: {1}", Captions.Result, resultStr);
        }
    }
}
