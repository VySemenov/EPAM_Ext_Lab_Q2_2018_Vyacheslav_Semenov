namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using SocNetwork.Models;

    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return this.Redirect(string.Format("/user/{0}", Thread.CurrentPrincipal.Identity.Name));
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel userAndPassword, string returnUrl)
        {
            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString); //todo pn не корректно так создавать экзепляр репозитория. Он должен создаваться в конструкторе.

            if (!string.IsNullOrEmpty(userAndPassword.Email) && !string.IsNullOrEmpty(userAndPassword.Password))
            {
                User user = repo.GetAll().Find(e => e.Email.Equals(userAndPassword.Email));
                if (user != null)
                {
                    if (user.Password.Trim(' ').Equals(userAndPassword.Password))
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);

                        if (returnUrl == null || returnUrl.Equals(string.Empty))
                        {
                            returnUrl = string.Format("/user/{0}", user.Id);
                        }

                        return this.Redirect(returnUrl);
                    }
                }
            }

            return this.View(userAndPassword);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.Redirect("~/");
        }
    }
}