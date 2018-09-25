namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;

    public class FriendsViewModel
    {
        public FriendsViewModel(User user, List<User> friends)
        {
            this.User = user;
            this.Friends = friends;
        }

        public User User { get; set; }

        public List<User> Friends { get; set; }
    }
}