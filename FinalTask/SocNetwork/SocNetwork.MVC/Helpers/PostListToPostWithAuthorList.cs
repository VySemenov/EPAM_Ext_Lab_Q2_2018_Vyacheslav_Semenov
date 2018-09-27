namespace SocNetwork.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using DAL.ConnectionStrings;
    using DAL.Entities.Posts;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.Post;

    public static class PostListToPostWithAuthorList
    {
        /// <summary>
        /// Converts list of posts to list of PostWithAuthor-objects.
        /// New list contains list of posts and authors name.
        /// </summary>
        /// <param name="postList"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<PostWithAuthor> ToPostWithAuthorList(this List<Post> postList, int userId)
        {
            IUserRepository usersRepo = new UserRepository(ConnectionString.GetConnectionString(), ConnectionString.GetConnectionDbType());

            List<PostWithAuthor> posts = new List<PostWithAuthor>();
            foreach (var p in postList)
            {
                User user = usersRepo.Get(p.AuthorId);
                posts.Add(new PostWithAuthor(p, user));
            }

            return posts;
        }
    }
}