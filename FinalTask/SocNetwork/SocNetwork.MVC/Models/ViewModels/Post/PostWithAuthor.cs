namespace SocNetwork.Models.ViewModels.Post
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Posts;
    using DAL.Entities.Users;

    public class PostWithAuthor
    {
        public PostWithAuthor(Post post, User user)
        {
            this.Post = post;
            this.User = user;
        }

        public Post Post { get; set; }

        public User User { get; set; }
    }
}