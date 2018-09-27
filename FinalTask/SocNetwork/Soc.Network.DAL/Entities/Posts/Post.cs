namespace DAL.Entities.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Helpers;
    using DAL.Repositories;

    public class Post
    {
        public Post()
        {
        }

        public Post(int id, int uid, int pid, DateTime pdate, string text)
        {
            this.Id = id;
            this.AuthorId = uid;
            this.PageId = pid;
            this.PublicationDate = pdate;
            this.Text = text;
        }

        public int Id { get; set; }

        public DateTime PublicationDate { get; set; }

        public int AuthorId { get; set; }

        public int PageId { get; set; }

        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            var post = obj as Post;
            return post != null &&
                   this.Id == post.Id &&
                   this.PublicationDate == post.PublicationDate &&
                   this.AuthorId == post.AuthorId &&
                   this.PageId == post.PageId &&
                   this.Text == post.Text;
        }

        public override int GetHashCode()
        {
            var hashCode = -2019289579;
            hashCode = (hashCode * -1521134295) + this.Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.PublicationDate.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.AuthorId.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.PageId.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Text);
            return hashCode;
        }
    }
}
