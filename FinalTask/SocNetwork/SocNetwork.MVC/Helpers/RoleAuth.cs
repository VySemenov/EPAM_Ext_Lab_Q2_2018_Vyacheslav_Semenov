namespace SocNetwork.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;

    public class RoleAuth
    {
        private static UserRepository userRepository = new UserRepository(ConnectionString.GetConnectionString());

        /// <summary>
        /// Checks user role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool IsInRole(int userId, int roleId)
        {
            User user = userRepository.Get(userId);

            if (user.UserRoleId != roleId)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}