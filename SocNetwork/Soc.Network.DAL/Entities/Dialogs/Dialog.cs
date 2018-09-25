namespace DAL.Entities.Dialogs
{
    using System;
    using System.Collections.Generic;
    using DAL.Entities.Users;

    public class Dialog
    {
        public int Id { get; set; }

        public List<Message> Messages { get; set; }

        public List<User> Users { get; set; }
    }
}
