namespace SocNetwork.Models.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;

    public class CreateUserViewModel
    {
        public CreateUserViewModel()
        {
            this.UserRoles = new List<UserRole>();
            foreach (UserRole r in Enum.GetValues(typeof(UserRole)))
            {
                if (r != UserRole.None)
                {
                    this.UserRoles.Add(r);
                }
            }

            this.ErrorMessage = string.Empty;
        }

        public CreateUserViewModel(string error)
        {
            this.UserRoles = new List<UserRole>();
            foreach (UserRole r in Enum.GetValues(typeof(UserRole)))
            {
                if (r != UserRole.None)
                {
                    this.UserRoles.Add(r);
                }
            }
            this.ErrorMessage = error;
        }

        public List<UserRole> UserRoles { get; set; }

        public string ErrorMessage { get; set; }
    }
}