namespace Task_3.Tasks.Task3_6
{
    using System;
    using Task_3.Resource;

    public class Task3_6 : Task
    {
        public override void Start()
        {
            Style style = new Style();

            int input = 0;
            do
            {
                Console.WriteLine(Captions.TextStyle + ": " + style.ToString());

                for (int i = 0; i < Style.GetStyleNameArray().Length; i++)
                {
                    Console.WriteLine((i + 1) + " - " + Style.GetStyleNameArray()[i]);
                }

                Console.WriteLine(Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
                if (input < 0 || input > Style.GetStyleNameArray().Length)
                {
                    Console.WriteLine(Captions.InputIsIncorrect);
                    continue;
                }
                else
                if (input > 0)
                {
                    style.Switch(input);
                }
            }
            while (input != 0);
        }
    }
}
