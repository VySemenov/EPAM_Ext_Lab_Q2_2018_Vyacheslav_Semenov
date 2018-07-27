namespace Task_7_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Task_7_3.Program;
    using static Task_7_3.Testing;

    public class Searching
    {
        /// <summary>
        /// Finds and returns all positive integers
        /// </summary>
        /// <param name="array">Array for searching</param>
        /// <returns></returns>
        public static int[] Search(int[] array)
        {
            List<int> list = new List<int>();
            foreach (var a in array)
            {
                if (a > 0)
                {
                    list.Add(a);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Finds and returns array elements according to the specified condition
        /// </summary>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int[] Search(int[] array, SearchCondition condition)
        {
            List<int> list = new List<int>();
            foreach (var a in array)
            {
                if (condition(a))
                {
                    list.Add(a);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Finds and returns all positive integers. Uses LINQ
        /// </summary>
        /// <param name="array">Array for searching</param>
        /// <returns></returns>
        public static int[] SearchLinq(int[] array)
        {
            var list = from item in array
                       where item > 0
                       select item;

            return list.ToArray();
        }
    }
}
