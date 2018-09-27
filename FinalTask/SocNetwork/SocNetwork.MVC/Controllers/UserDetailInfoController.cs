namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using SocNetwork.FileUploading;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.UserDetailInfo;

    public class UserDetailInfoController : Controller
    {
        private IUserDetailInfoRepository userDetailInfoRepository;

        public UserDetailInfoController(IUserDetailInfoRepository repo)
        {
            this.userDetailInfoRepository = repo;
        }

        [Authorize]
        public ActionResult Edit()
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            UserDetailInfo info = this.userDetailInfoRepository.Get(userId);
            if (info == null)
            {
                info = new UserDetailInfo();
            }

            return this.View(new EditDetailInfoViewModel(info));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(UserDetailInfo info, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = int.Parse(Thread.CurrentPrincipal.Identity.Name);
                    UserDetailInfo uinfo = this.userDetailInfoRepository.Get(id);
                    if (uinfo == null)
                    {
                        uinfo = new UserDetailInfo() { UserId = id };
                    }

                    uinfo.City = info.City;
                    uinfo.DateOfBirth = info.DateOfBirth;
                    uinfo.Interests = info.Interests;
                    if (file != null)
                    {
                        string fileName = FileUpload.UploadFile(file, id);
                        if (!fileName.Equals(string.Empty))
                        {
                            uinfo.Avatar = FileUpload.UploadFile(file, id);
                        }
                    }

                    if (this.userDetailInfoRepository.Save(uinfo))
                    {
                        return this.RedirectToRoute("User", new { id });
                    }

                    return this.RedirectToAction("Edit");
                }

                return this.RedirectToAction("Edit");
            }
            catch
            {
                return this.RedirectToAction("Edit");
            }
        }
    }
}