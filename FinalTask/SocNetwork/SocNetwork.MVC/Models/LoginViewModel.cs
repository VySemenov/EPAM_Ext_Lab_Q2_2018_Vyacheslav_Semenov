namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class LoginViewModel
    {
        public string Email { get; set; }//todo pn сюда бы регулярку с проверкой почты

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}