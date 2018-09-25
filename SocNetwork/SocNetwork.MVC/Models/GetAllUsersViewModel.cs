namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;

    public class GetAllUsersViewModel
    {
        public GetAllUsersViewModel(List<User> users, int page)
        {
            this.Users = users;
            this.Page = page;
        }

        public List<User> Users { get; set; }

        public int Page { get; set; }
    }
}