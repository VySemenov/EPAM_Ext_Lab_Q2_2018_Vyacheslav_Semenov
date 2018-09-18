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

    public class UserController : Controller
    {
        [Authorize]
        public ActionResult Control()
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            return this.View();
        }

        public ActionResult Get(int id)
        {
            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            User user = repo.Get(id);
            if (user != null)
            {
                return this.View(user);
            }

            return new HttpNotFoundResult();
        }

        [Authorize]
        public ActionResult GetAll(int num = 20)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            List<User> users = repo.GetAll(num);
            if (users != null)
            {
                return this.View(users);
            }

            return this.View(new List<User>());
        }

        [Authorize]
        public ActionResult Create()
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            return this.View();
        }

        [HttpPost]
        public ActionResult Registration(FormCollection collection)
        {
            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                User user = new User();
                user.Firstname = collection.Get("Firstname");
                user.Surname = collection.Get("Lastname");
                user.Password = collection.Get("Password");
                user.Email = collection.Get("Email");
                user.UserRoleId = (int)UserRole.User;

                if (repo.Save(user))
                {
                    User up = repo.GetAll().Find(e => e.Email.Equals(user.Email));
                    FormsAuthentication.SetAuthCookie(up.Id.ToString(), false);
                    return this.Redirect(string.Format("/user/{0}", up.Id));
                }

                return this.View();
            }
            catch
            {
                return this.View();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                User user = new User();
                user.Firstname = collection.Get("Firstname");
                user.Surname = collection.Get("Lastname");
                user.Password = collection.Get("Password");
                user.Email = collection.Get("Email");
                user.UserRoleId = (int)UserRole.User;

                if (repo.Save(user))
                {
                    User up = repo.GetAll().Find(e => e.Email.Equals(user.Email));
                    return this.Redirect(string.Format("/user/{0}", up.Id));
                }

                return this.View();
            }
            catch
            {
                return this.View();
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            User user = repo.Get(id);
            if (user != null)
            {
                return this.View(user);
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(FormCollection collection)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            try
            {
                int id;
                int.TryParse(collection.Get("Id"), out id);
                User user = new User(repo.Get(id));
                user.Id = id;
                user.Firstname = collection.Get("Firstname");
                user.Surname = collection.Get("Lastname");
                user.Email = collection.Get("Email");

                if (repo.Save(user))
                {
                    return this.Redirect(string.Format("/user/{0}", id));
                }

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            return this.View(id);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(FormCollection collection)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.Redirect(string.Format("/user/{0}", uId));
            }

            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try
            {
                int id;
                int.TryParse(collection.Get("ID"), out id);

                User user = repo.Get(id);
                if (user != null)
                {
                    repo.Delete(id);
                }

                return this.Redirect("/users");
            }
            catch
            {
                return this.Redirect("/users");
            }
        }
    }
}
