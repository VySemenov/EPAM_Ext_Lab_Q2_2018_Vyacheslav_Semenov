namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Friends;
    using DAL.Entities.Users;
    using SocNetwork.Helpers;

    public class GetUserViewModel
    {
        public GetUserViewModel(User user)
        {
            this.User = user;
            this.Friends = user.Friends.Where(f => f.RelationId == (int)Relation.Friends).ToList().ToUserList(user.Id);
            this.Posts = user.Posts.ToPostWithAuthorList(user.Id);
        }

        public User User { get; set; }

        public List<User> Friends { get; set; }

        public List<PostWithAuthor> Posts { get; set; }
    }
}