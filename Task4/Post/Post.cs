namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Post
    {
        public int Id { get; set; }

        public int Text { get; set; }

        public List<Attachment> Attachments { get; set; }

        public override bool Equals(object obj)
        {
            var post = obj as Post;
            return post != null &&
                   Id == post.Id &&
                   Text == post.Text &&
                   EqualityComparer<List<Attachment>>.Default.Equals(Attachments, post.Attachments);
        }

        public override int GetHashCode()
        {
            var hashCode = 1313838127;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Text.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Attachment>>.Default.GetHashCode(Attachments);
            return hashCode;
        }
    }
}