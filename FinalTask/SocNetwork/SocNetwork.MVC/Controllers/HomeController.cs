namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    public class HomeController : Controller
    {
        public void Index()
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }
    }
}