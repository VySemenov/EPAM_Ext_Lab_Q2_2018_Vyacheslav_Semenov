namespace Task_3.Tasks.Task3_6
{
    using System;
    using Task_3.Resource;

    public class Style
    {
        private static string[] styleNameArray = 
        {
            Captions.Bold, Captions.Italic, Captions.Underline
        };

        private bool[] styleStateArray = new bool[styleNameArray.Length];

        public static string[] GetStyleNameArray()
        {
            return styleNameArray;
        }

        public bool[] GetStyleStateArray()
        {
            return this.styleStateArray;
        }

        public void Switch(int styleNum)
        {
            try
            {
                if (this.styleStateArray[styleNum - 1])
                {
                this.styleStateArray[styleNum - 1] = false;
                }
                else
                {
                    this.styleStateArray[styleNum - 1] = true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine(Captions.InputIsIncorrect);
            }
        }

        public override string ToString()
        {
            bool found = false;
            for (int i = 0; i < this.styleStateArray.Length; i++)
            {
                if (this.styleStateArray[i])
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return Captions.None;
            }

            string s = string.Empty;
            for (int i = 0; i < this.styleStateArray.Length; i++)
            {
                if (this.styleStateArray[i])
                {
                    s += styleNameArray[i] + " ";
                }
            }

            return s;
        }
    }
}
