namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using DAL.Entities.Dialogues;
    using DAL.Repositories.Abstract;

    public class MessageController : Controller
    {
        private IDialogRepository dialogRepository;
        private IUserRepository userRepository;
        private IMessageRepository messageRepository;

        public MessageController(IDialogRepository drepo, IUserRepository urepo, IMessageRepository mrepo)
        {
            this.dialogRepository = drepo;
            this.userRepository = urepo;
            this.messageRepository = mrepo;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Message message, int interlocutorId)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);

                Dialog dialog = this.dialogRepository.Get(message.AuthorId, interlocutorId);
                if (dialog == null)
                {
                    dialog = new Dialog() { UserId = userId, InterlocutorId = interlocutorId };
                    bool save = this.dialogRepository.Save(dialog);
                    dialog = this.dialogRepository.Get(userId, interlocutorId);
                }

                message.AuthorId = userId;
                message.DialogId = dialog.Id;
                message.Time = DateTime.Now;
                this.messageRepository.Save(message);
            }

            return this.Redirect(string.Format("/Dialog/Get?dialogId={0}", message.DialogId));
        }
    }
}