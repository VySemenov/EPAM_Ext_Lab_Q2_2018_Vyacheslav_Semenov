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
    using SocNetwork.Models;

    public class UserDetailInfoController : Controller
    {
        private UserDetailInfoRepository userDetailInfoRepository;

        public UserDetailInfoController()
        {
            this.userDetailInfoRepository = new UserDetailInfoRepository(ConnectionString.GetConnectionString());
        }

        [Authorize]
        public ActionResult Edit()
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            UserDetailInfo info = this.userDetailInfoRepository.Get(userId);
            if (info == null)
            {
                info = new UserDetailInfo() { UserId = userId };
            }

            return this.View(new EditDetailInfoViewModel(info));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                int id = int.Parse(Thread.CurrentPrincipal.Identity.Name);
                UserDetailInfo info = new UserDetailInfo() { UserId = id };
                info.City = form.Get("City");
                DateTime dob = DateTime.Now;
                DateTime.TryParse(form.Get("DOB"), out dob);
                info.DateOfBirth = dob;
                info.Interests = form.Get("Interests");

                if (this.userDetailInfoRepository.Save(info))
                {
                    return this.RedirectToRoute("User", new { id });
                }

                return this.RedirectToRoute("User", new { id });
            }
            catch
            {
                return this.RedirectToAction("Edit");
            }
        }
    }
}