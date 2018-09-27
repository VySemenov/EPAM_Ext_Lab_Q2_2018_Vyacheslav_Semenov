namespace SocNetwork.Models.ViewModels.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Dialogues;
    using DAL.Entities.Users;

    public class DialoguesViewModel
    {
        public DialoguesViewModel(List<DialogWithLastMessage> dialogues, List<User> users)
        {
            this.Dialogues = dialogues;
            this.Users = users;
        }

        public List<DialogWithLastMessage> Dialogues { get; set; }

        public List<User> Users { get; set; }
    }
}