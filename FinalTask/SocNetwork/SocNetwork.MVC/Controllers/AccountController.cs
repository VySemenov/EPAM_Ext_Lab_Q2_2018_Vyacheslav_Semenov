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
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.Account;

    public class AccountController : Controller
    {
        private IUserRepository userRepository;

        public AccountController(IUserRepository repo)
        {
            this.userRepository = repo;
        }

        public ActionResult LogOn()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return this.RedirectToRoute("User", new { id = int.Parse(Thread.CurrentPrincipal.Identity.Name) });
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel userAndPassword, string returnUrl)
        {
            if (!string.IsNullOrEmpty(userAndPassword.Email) && !string.IsNullOrEmpty(userAndPassword.Password))
            {
                User user = this.userRepository.GetAll().Find(e => e.Email.Equals(userAndPassword.Email));
                if (user != null)
                {
                    if (user.Password.Trim(' ').Equals(userAndPassword.Password))
                    {
                        if (userAndPassword.RememberMe)
                        {
                            Response.Cookies.Clear();

                            DateTime expiryDate = DateTime.Now.AddDays(30);
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, user.Id.ToString(), DateTime.Now, expiryDate, false, string.Empty);
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            authenticationCookie.Expires = ticket.Expiration;

                            Response.Cookies.Add(authenticationCookie);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                        }

                        if (returnUrl == null || returnUrl.Equals(string.Empty))
                        {
                            return this.RedirectToRoute("User", new { id = user.Id });
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