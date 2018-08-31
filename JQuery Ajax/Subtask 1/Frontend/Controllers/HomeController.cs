using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        List<string> left = new List<string> { "op1", "op2"};
        List<string> right = new List<string>() { "op3", "op4" };

        public ActionResult Index()
        {
            ViewBag.Left = left;
            ViewBag.Right = right;
            return View();
        }

        /// <summary>
        /// The method moves items from one list to another.
        /// The list of items to move is stored in the list "change".
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="change"></param>
        /// <returns>Lists in json</returns>
        public JsonResult Move(string[] left, string[] right, string[] change)
        {
            List<string> leftList;
            List<string> rightList;
            if (left == null)
            {
                leftList = new List<string>();
            }
            else
            {
                leftList = left.ToList();
            }

            if (right == null)
            {
                rightList = new List<string>();
            }
            else
            {
                rightList = right.ToList();
            }

            if (change != null)
            {
                List<string> changeList = change.ToList();

                foreach (var ch in changeList)
                {
                    if (leftList.Contains(ch))
                    {
                        leftList.Remove(ch);
                        rightList.Add(ch);
                    }
                    else
                    {
                        rightList.Remove(ch);
                        leftList.Add(ch);
                    }
                }
            }

            return Json(new { leftList, rightList });
        }
    }
}