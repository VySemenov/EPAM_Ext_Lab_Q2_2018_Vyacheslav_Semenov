namespace DAL.Entities.Dialogues
{
    using System;
    using System.Collections.Generic;

    public class Message
    {
        public Message()
        {
        }

        public Message(int id, int dialogId, int authorId, DateTime time, string text)
        {
            this.Id = id;
            this.DialogId = dialogId;
            this.AuthorId = authorId;
            this.Time = time;
            this.Text = text;
        }

        public int Id { get; set; }

        public int DialogId { get; set; }

        public int AuthorId { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
}
