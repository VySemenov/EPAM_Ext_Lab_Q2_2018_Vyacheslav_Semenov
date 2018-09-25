namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;

    public class EditUserViewModel
    {
        public EditUserViewModel(User user)
        {
            this.User = user;
            this.UserRoles = new List<UserRole>();
            foreach(UserRole r in Enum.GetValues(typeof(UserRole)))
            {
                if (r != UserRole.None)
                {
                    this.UserRoles.Add(r);
                }
            }
        }

        public User User { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}