namespace SocNetwork.Models.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Friends;
    using DAL.Entities.Users;
    using SocNetwork.Helpers;
    using SocNetwork.Models.ViewModels.Post;

    public class GetUserViewModel
    {
        public GetUserViewModel(User user, User currentUser, string error = "")
        {
            this.User = user;
            this.Friends = user.Friends.Where(f => f.RelationId == (int)Relation.Friends).ToList().ToUserList(user.Id);
            this.Posts = user.Posts.ToPostWithAuthorList(user.Id);
            this.CurrentUser = currentUser;
            this.ErrorMessage = error;
        }

        public User User { get; set; }

        public List<User> Friends { get; set; }

        public List<PostWithAuthor> Posts { get; set; }

        public User CurrentUser { get; set; }

        public string ErrorMessage { get; set; }
    }
}