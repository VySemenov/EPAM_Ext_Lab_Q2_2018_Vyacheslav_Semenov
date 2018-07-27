namespace Task_7_1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ArrayExtension
    {
        public static int Sum(this Array array)
        {
            Type t = array.GetType();
            if (t.Equals(typeof(int[])))
            {
                int sum = default(int);

                foreach (int a in array)
                {
                    sum += a;
                }

                return sum;
            }

            return 0;
        }
    }
}
