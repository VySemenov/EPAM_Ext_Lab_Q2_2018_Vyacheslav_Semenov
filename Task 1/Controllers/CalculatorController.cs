namespace Task_1.Controllers
{
    using System;
    using System.Web.Mvc;
    using Task_1.Models;

    public class CalculatorController : Controller
    {
        /// <summary>
        /// Accepts two numbers on the input and, depending on the operation, 
        /// add a new LogLine
        /// </summary>
        /// <param name="arg1">First number</param>
        /// <param name="arg2">Second number</param>
        /// <param name="operation">Math operation</param>
        /// <returns></returns>
        public ActionResult Calc(string arg1, string arg2, string operation)
        {
            ViewBag.LogLines = Repository.Instance.LogLines;
            int x = 0;
            int y = 0;
            string result = string.Empty;

            if (!int.TryParse(arg1, out x) || !int.TryParse(arg2, out y))
            {
                ViewBag.Message = "Enter the numbers";
                return this.View();
            }

            switch (operation)
            {
                case "+": result = this.Sum(x, y);
                                   break;
                case "*": result = this.Prod(x, y);
                                   break;
            }

            Repository.Instance.LogLines.Add(new LogLine(DateTime.Now, arg1, arg2, operation, result));

            return this.View();
        }

        /// <summary>
        /// Sums up two numbers and return it as string
        /// </summary>
        /// <param name="x">First number</param>
        /// <param name="y">Second number</param>
        /// <returns></returns>
        public string Sum(int x, int y)
        {
            return (x + y).ToString();
        }

        /// <summary>
        /// Multiplies two numbers and return it as string
        /// </summary>
        /// <param name="x">First number</param>
        /// <param name="y">Second number</param>
        /// <returns></returns>
        public string Prod(int x, int y)
        {
            return (x * y).ToString();
        }
    }
}