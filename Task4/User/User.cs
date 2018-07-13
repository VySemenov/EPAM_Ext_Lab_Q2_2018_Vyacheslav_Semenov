namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class User
    {
        public UserDetailInfo DetailInfo { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public List<Dialog> Dialogs { get; set; }

        public List<Message> Messages { get; set; }

        public List<Post> Posts { get; set; }

        public UserRole UserRole { get; set; }
    }
}