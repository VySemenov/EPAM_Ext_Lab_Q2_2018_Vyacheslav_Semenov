namespace SocNetwork.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using DAL.ConnectionStrings;
    using DAL.Entities.Friends;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;

    public static class RelListToUserListExtension
    {
        /// <summary>
        /// Converts list of Friends to list of Users.
        /// </summary>
        /// <param name="relList">List of friends relations</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static List<User> ToUserList(this List<Friends> relList, int userId)
        {
            IUserRepository usersRepo = new UserRepository(ConnectionString.GetConnectionString(), ConnectionString.GetConnectionDbType());

            List<User> userList = new List<User>();
            foreach (var r in relList)
            {
                if (r.UserId != userId)
                {
                    userList.Add(usersRepo.Get(r.UserId));
                }
                else
                {
                    userList.Add(usersRepo.Get(r.FriendId));
                }
            }

            return userList;
        }
    }
}