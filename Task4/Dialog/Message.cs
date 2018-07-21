namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Message
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public List<Attachment> Attachment { get; set; }

        public override bool Equals(object obj)
        {
            var message = obj as Message;
            return message != null &&
                   this.Id == message.Id &&
                   this.Text == message.Text &&
                   EqualityComparer<List<Attachment>>.Default.Equals(this.Attachment, message.Attachment);
        }

        public override int GetHashCode()
        {
            var hashCode = -1059379818;
            hashCode = (hashCode * -1521134295) + this.Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Text.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<List<Attachment>>.Default.GetHashCode(this.Attachment);
            return hashCode;
        }
    }
}