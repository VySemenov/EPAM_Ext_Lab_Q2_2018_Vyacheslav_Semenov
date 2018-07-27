namespace Task_7_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class StringExtension
    {
        private const char FirstNull = '0';

        public static bool IsPosInt(this string str)
        {
            str = str.TrimStart(FirstNull);

            if (!str.Equals(string.Empty))
            {
                return str.All(char.IsDigit);
            }

            return false;
        }
    }
}
