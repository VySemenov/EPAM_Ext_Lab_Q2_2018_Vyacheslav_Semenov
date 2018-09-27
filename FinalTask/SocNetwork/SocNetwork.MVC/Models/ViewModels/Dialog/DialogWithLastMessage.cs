namespace SocNetwork.Models.ViewModels.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Dialogues;

    public class DialogWithLastMessage
    {
        public DialogWithLastMessage(Dialog dialog, Message message)
        {
            this.Dialog = dialog;
            this.LastMessage = message;
        }

        public Dialog Dialog { get; set; }

        public Message LastMessage { get; set; }
    }
}