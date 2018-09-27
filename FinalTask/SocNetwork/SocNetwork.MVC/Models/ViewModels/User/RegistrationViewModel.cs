namespace SocNetwork.Models.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;

    public class RegistrationViewModel
    {
        public RegistrationViewModel()
        {
            this.User = new User();
            this.ErrorMessage = string.Empty;
        }

        public RegistrationViewModel(User user, string error)
        {
            this.User = user;
            this.ErrorMessage = error;
        }

        public User User { get; set; }

        public string ErrorMessage { get; set; }
    }
}