namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.Search;
    using SocNetwork.Models.ViewModels.User;

    public class SearchController : Controller
    {
        private readonly int fetchnum;
        private IUserRepository userRepository;

        public SearchController(IUserRepository repo)
        {
            this.userRepository = repo;
            this.fetchnum = 20;
        }

        public ActionResult Index(int page = 1)
        {
            int offset = (page - 1) * this.fetchnum;
            List<User> users = this.userRepository.GetOffset(offset, this.fetchnum);
            if (users != null)
            {
                return this.View(new GetAllUsersViewModel(users, page));
            }

            return this.View(new GetAllUsersViewModel(new List<User>(), page));
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel model, int page = 1)
        {
            int offset = (page - 1) * this.fetchnum;

            List<User> users = this.userRepository.GetAll();
            if (users != null)
            {
                if (model.Firstname != null && !model.Equals(string.Empty))
                {
                   users = users.Where(u => u.Firstname.Trim(' ').Equals(model.Firstname)).ToList();
                }

                if (model.Lastname != null && !model.Equals(string.Empty))
                {
                    users = users.Where(u => u.Surname.Trim(' ').Equals(model.Lastname)).ToList();
                }

                if (model.City != null && !model.Equals(string.Empty))
                {
                    users = users.Where(u => u.DetailInfo.City.Equals(model.City)).ToList();
                }

                return this.View(new GetAllUsersViewModel(users, page));
            }

            return this.View(new GetAllUsersViewModel(new List<User>(), page));
        }
    }
}