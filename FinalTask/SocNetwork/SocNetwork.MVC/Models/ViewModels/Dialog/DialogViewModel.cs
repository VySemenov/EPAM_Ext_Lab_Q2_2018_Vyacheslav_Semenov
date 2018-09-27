namespace SocNetwork.Models.ViewModels.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Dialogues;
    using DAL.Entities.Users;

    public class DialogViewModel
    {
        public DialogViewModel(Dialog dialog, User interlocutor)
        {
            this.Dialog = dialog;
            this.Interlocutor = interlocutor;
        }

        public Dialog Dialog { get; set; }

        public User Interlocutor { get; set; }
    }
}