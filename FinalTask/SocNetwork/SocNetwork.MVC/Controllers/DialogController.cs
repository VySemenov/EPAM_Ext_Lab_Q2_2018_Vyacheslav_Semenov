namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using DAL.Entities.Dialogues;
    using DAL.Entities.Users;
    using DAL.Repositories.Abstract;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.Dialog;

    public class DialogController : Controller
    {
        private const int MaxLengthLastMsg = 30;
        private const string AdditionalLastMsg = "...";

        private IDialogRepository dialogRepository;
        private IUserRepository userRepository;
        private IMessageRepository messageRepository;

        public DialogController(IDialogRepository drepo, IUserRepository urepo, IMessageRepository mrepo)
        {
            this.dialogRepository = drepo;
            this.userRepository = urepo;
            this.messageRepository = mrepo;
        }

        [Authorize]
        public ActionResult Index()
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            List<Dialog> dialogList = this.dialogRepository.GetAll(userId);

            List<User> users = new List<User>();
            List<DialogWithLastMessage> dialogues = new List<DialogWithLastMessage>();
            Message lastMessage;
            foreach (var d in dialogList)
            {
                User user;
                if (d.UserId == userId)
                {
                    user = this.userRepository.Get(d.InterlocutorId);
                }
                else
                {
                    user = this.userRepository.Get(d.UserId);
                }

                users.Add(user);

                List<Message> messages = this.messageRepository.GetAllByDialog(d.Id).OrderBy(m => m.Time).ToList();
                if (messages.Any())
                {
                    lastMessage = messages.Last();
                    if (lastMessage.Text.Contains("\n"))
                    {
                        int index = lastMessage.Text.IndexOf('\n');
                        lastMessage.Text = lastMessage.Text.Substring(0, index);
                    }

                    if (lastMessage.Text.Length > DialogController.MaxLengthLastMsg)
                    {
                        lastMessage.Text = lastMessage.Text.Substring(0, DialogController.MaxLengthLastMsg);
                        lastMessage.Text += DialogController.AdditionalLastMsg;
                    }
                }
                else
                {
                    lastMessage = new Message() { Text = string.Empty };
                }

                dialogues.Add(new DialogWithLastMessage(d, lastMessage));
            }

            dialogues = dialogues.OrderByDescending(d => d.LastMessage.Time).ToList();

            return this.View(new DialoguesViewModel(dialogues, users));
        }

        [Authorize]
        public ActionResult Get(int? dialogId)
        {
            if (dialogId == null)
            {
                return this.RedirectToAction("Index");
            }

            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            Dialog dialog = this.dialogRepository.Get((int)dialogId);
            if (dialog.UserId != userId && dialog.InterlocutorId != userId)
            {
                return this.RedirectToAction("Index");
            }

            User user;
            User interlocutor;
            if (dialog.UserId != userId)
            {
                interlocutor = this.userRepository.Get(dialog.UserId);
                user = this.userRepository.Get(dialog.InterlocutorId);
            }
            else
            {
                interlocutor = this.userRepository.Get(dialog.InterlocutorId);
                user = this.userRepository.Get(dialog.UserId);
            }

            List<Message> messages = this.messageRepository.GetAllByDialog((int)dialogId);
            messages = messages.OrderBy(m => m.Time).ToList();

            return this.View(new OpenDialogViewModel(dialog, user, interlocutor, messages));
        }
    }
}