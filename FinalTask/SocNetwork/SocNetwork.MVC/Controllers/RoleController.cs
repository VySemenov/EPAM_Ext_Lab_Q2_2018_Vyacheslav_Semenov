namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using DAL.Entities.Users;
    using DAL.Repositories;

    public class RoleController : Controller
    {
        public ActionResult Control()
        {
            return this.View();
        }

        public ActionResult GetAll()
        {
            RoleRepository repo = new RoleRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            List<UserRole> roles = repo.GetAll();
            if (roles != null)
            {
                return this.View(roles);
            }

            return this.View(new List<UserRole>());
        }
    }
}
