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
    using SocNetwork.Helpers;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.User;
    using SocNetwork.Resources;

    public class UserController : Controller
    {
        private readonly int fetchnum;
        private IUserRepository userRepository;

        public UserController(IUserRepository repo)
        {
            this.userRepository = repo;
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
                try
                {
                    User currentUser;
                    if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    {
                        currentUser = this.userRepository.Get(int.Parse(Thread.CurrentPrincipal.Identity.Name));
                    }
                    else
                    {
                        currentUser = new User();
                    }

                    User user = this.userRepository.Get(id);
                    if (user != null)
                    {
                        return this.View(new GetUserViewModel(user, currentUser));
                    }
                }
                catch
                {
                    return new HttpNotFoundResult();
                }
            }

            return new HttpNotFoundResult();
        }

        [Authorize]
        public ActionResult GetAll(int page = 1)
        {
            try
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
            catch
            {
                return this.RedirectToRoute("User");
            }
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
                    string errorMsg;
                    User u = this.userRepository.GetAll().Find(e => e.Email.Equals(user.Email));
                    if (u != null)
                    {
                        errorMsg = Captions.EmailInUse;
                        return this.View(new CreateUserViewModel(errorMsg));
                    }

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

        [HttpGet]
        public ActionResult Registration()
        {
            return this.View(new RegistrationViewModel());
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string errorMsg;
                    User u = this.userRepository.GetAll().Find(e => e.Email.Equals(user.Email));
                    if (u != null)
                    {
                        errorMsg = Captions.EmailInUse;
                        return this.View(new RegistrationViewModel(user, errorMsg));
                    }

                    user.UserRoleId = (int)UserRole.User;

                    if (this.userRepository.Save(user))
                    {
                        User up = this.userRepository.GetAll().Find(e => e.Email.Equals(user.Email));
                        FormsAuthentication.SetAuthCookie(up.Id.ToString(), false);
                        return this.RedirectToRoute("User", new { id = up.Id });
                    }

                    return this.View(new RegistrationViewModel(user, Captions.UnexpectedError));
                }
                catch (Exception e)
                {
                    return this.View(new RegistrationViewModel(user, e.Message));
                }
            }

            return this.View(new RegistrationViewModel(user, Captions.UnexpectedError));
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
                    string errorMsg;
                    User u = this.userRepository.GetAll().Find(e => e.Email.Equals(user.Email));
                    if (u != null && u.Id != user.Id)
                    {
                        errorMsg = Captions.EmailInUse;
                        return this.View(new EditUserViewModel(user, errorMsg));
                    }

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
