namespace SocNetwork.Controllers
{
    using DAL.Entities.Users;
    using DAL.Repositories;
    using SocNetwork.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    public class AccountController : Controller
    {
        UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public ActionResult LogOn()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return Redirect(string.Format("/user/{0}", Thread.CurrentPrincipal.Identity.Name));
            }

            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel userAndPassword, string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(userAndPassword.Email) && !string.IsNullOrEmpty(userAndPassword.Password))
            {
                User user = repo.GetAll().Find(e => e.Email == userAndPassword.Email);
                if (user != null)
                {
                    if (user.Password.Trim(' ').Equals(userAndPassword.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                        ReturnUrl = string.Format("/user/{0}", user.Id);

                        return Redirect(ReturnUrl);
                    }
                }
            }

            return View(userAndPassword);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return Redirect("~/");
        }
    }
}