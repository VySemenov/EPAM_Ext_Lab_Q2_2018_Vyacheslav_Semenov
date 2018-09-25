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
    using SocNetwork.Helpers;
    using SocNetwork.Models;

    public class UserController : Controller
    {
        private readonly int fetchnum;
        private UserRepository userRepository;

        public UserController()
        {
            this.userRepository = new UserRepository(ConnectionString.GetConnectionString());
            this.fetchnum = 20;
        }

        [Authorize]
        public ActionResult Control()
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            return this.View();
        }

        public ActionResult Get(int id)
        {
            if (ModelState.IsValid)
            {
                User user = this.userRepository.Get(id);
                if (user != null)
                {
                    return this.View(new GetUserViewModel(user));
                }
            }

            return new HttpNotFoundResult();
        }

        [Authorize]
        public ActionResult GetAll(int page = 1)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            int offset = (page - 1) * this.fetchnum;
            List<User> users = this.userRepository.GetOffset(offset, this.fetchnum);
            if (users != null)
            {
                return this.View(new GetAllUsersViewModel(users, page));
            }

            return this.View(new GetAllUsersViewModel(new List<User>(), page));
        }

        [Authorize]
        public ActionResult Create()
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            return this.View(new CreateUserViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(User user)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (this.userRepository.Save(user))
                    {
                        User up = this.userRepository.GetAll().Find(e => e.Email.Equals(user.Email));
                        return this.RedirectToRoute("User", new { id = up.Id });
                    }

                    return this.RedirectToAction("Create");
                }
                catch
                {
                    return this.RedirectToAction("Create");
                }
            }

            return this.RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.UserRoleId = (int)UserRole.User;

                    if (this.userRepository.Save(user))
                    {
                        User up = this.userRepository.GetAll().Find(e => e.Email.Equals(user.Email));
                        FormsAuthentication.SetAuthCookie(up.Id.ToString(), false);
                        return this.RedirectToRoute("User", new { id = up.Id });
                    }

                    return this.View();
                }
                catch
                {
                    return this.View();
                }
            }

            return this.View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            if (ModelState.IsValid)
            {
                User user = this.userRepository.Get(id);
                if (user != null)
                {
                    return this.View(new EditUserViewModel(user));
                }
            }

            return this.RedirectToAction("Control");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (this.userRepository.Save(user))
                    {
                        return this.RedirectToRoute("User", new { user.Id });
                    }

                    return this.RedirectToRoute("User", new { user.Id });
                }
                catch
                {
                    return this.View();
                }
            }

            return this.View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            return this.View(id);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(DeleteUserViewModel model)
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = this.userRepository.Get(model.Id);
                    if (user != null)
                    {
                        this.userRepository.Delete(model.Id);
                    }

                    return this.RedirectToRoute("Users");
                }
                catch
                {
                    return this.RedirectToRoute("Users");
                }
            }

            return this.RedirectToRoute("Users");
        }
    }
}
