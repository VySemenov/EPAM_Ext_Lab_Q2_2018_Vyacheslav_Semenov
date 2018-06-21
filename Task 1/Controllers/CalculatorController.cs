using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_1.Models;

namespace Task_1.Controllers
{
    public class CalculatorController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public ActionResult Calc(string arg1, string arg2, string operation)
        {
            ViewBag.LogLines = Repository.Instance.LogLines;
            int x = 0;
            int y = 0;
            string result = "";

            if (!int.TryParse(arg1, out x) || !int.TryParse(arg2, out y))
            {
                ViewBag.Message = "Enter the numbers";
                return View();
            }

            switch (operation)
            {
                case "+": result = Sum(x, y); break;
                case "*": result = Prod(x, y); break;
            }

            Repository.Instance.LogLines.Add(new LogLine(DateTime.Now, arg1, arg2, operation, result));

            return View();
        }

        public string Sum (int x, int y)
        {
            return (x + y).ToString();
        }

        public string Prod (int x, int y)
        {
            return (x * y).ToString();
        }
    }
}