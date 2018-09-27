namespace SocNetwork.Models.ViewModels.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Dialogues;
    using DAL.Entities.Users;

    public class OpenDialogViewModel
    {
        public OpenDialogViewModel(Dialog dialog, User user, User interlocutor, List<Message> messages)
        {
            this.Dialog = dialog;
            this.User = user;
            this.Interlocutor = interlocutor;
            this.Messages = messages;
        }

        public Dialog Dialog { get; set; }

        public User User { get; set; }

        public User Interlocutor { get; set; }

        public List<Message> Messages { get; set; }
    }
}