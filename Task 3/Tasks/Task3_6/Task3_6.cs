namespace Task_3.Tasks.Task3_6
{
    using System;
    using Task_3.Resource;

    public class Task3_6
    {
        public static void Start()
        {
            Console.WriteLine(Captions.Separator);

            Style style = new Style();

            int input = 0;
            do
            {
                Console.WriteLine(Captions.TextStyle + ": " + style.ToString());

                for (int i = 0; i < Style.StyleNameArray.Length; i++)
                {
                    Console.WriteLine((i + 1) + " - " + Style.StyleNameArray[i]);
                }

                Console.WriteLine(Captions.Quit);

                int.TryParse(Console.ReadLine(), out input);
                if (input == 0)
                {
                    break;
                }
                else
                if (input < 0 || input > Style.StyleNameArray.Length)
                {
                    Console.WriteLine(Captions.InputIsIncorrect);
                }
                else
                {
                    style.Switch(input);
                }
            }
            while (input != 0);

            Console.WriteLine(Captions.Separator);
        }
    }
}
