namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using SocNetwork.Helpers;
    using SocNetwork.Models;

    public class RoleController : Controller
    {
        private RoleRepository roleRepository;

        public RoleController()
        {
            this.roleRepository = new RoleRepository(ConnectionString.GetConnectionString());
        }

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

        [Authorize]
        public ActionResult GetAll()
        {
            int uId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (!RoleAuth.IsInRole(uId, (int)UserRole.Admin))
            {
                return this.RedirectToRoute("User", new { id = uId });
            }

            List<UserRole> roles = this.roleRepository.GetAll();
            if (roles != null)
            {
                return this.View(roles);
            }

            return this.View(new List<UserRole>());
        }
    }
}
