namespace DAL.Entities.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DAL.Entities.Attachments;

    public class Post
    {
        public int Id { get; set; }

        public int Text { get; set; }

        public List<Attachment> Attachments { get; set; }

        public override bool Equals(object obj)
        {
            var post = obj as Post;
            return post != null &&
                   this.Id == post.Id &&
                   this.Text == post.Text &&
                   EqualityComparer<List<Attachment>>.Default.Equals(this.Attachments, post.Attachments);
        }

        public override int GetHashCode()
        {
            var hashCode = 1313838127;
            hashCode = (hashCode * -1521134295) + this.Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Text.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<List<Attachment>>.Default.GetHashCode(this.Attachments);
            return hashCode;
        }
    }
}
