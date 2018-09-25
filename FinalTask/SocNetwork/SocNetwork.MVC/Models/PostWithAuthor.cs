using DAL.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocNetwork.Models
{
    public class PostWithAuthor
    {
        public PostWithAuthor(Post Post, string Firstname, string Lastname)
        {
            this.Post = Post;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
        }

        public Post Post { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}