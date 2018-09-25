namespace SocNetwork.Models
{
    using DAL.Entities.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CreateUserViewModel
    {
        public CreateUserViewModel()
        {
            UserRoles = new List<UserRole>();
            foreach (UserRole r in Enum.GetValues(typeof(UserRole)))
            {
                if (r != UserRole.None)
                {
                    this.UserRoles.Add(r);
                }
            }
        }

        public List<UserRole> UserRoles { get; set; }
    }
}