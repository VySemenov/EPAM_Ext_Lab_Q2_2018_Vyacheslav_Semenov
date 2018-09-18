namespace SocNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using DAL.Entities.Users;
    using DAL.Repositories;

    public class RoleAuth
    {
        public static bool IsInRole(int userId, int roleId)
        {
            UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            User user = repo.Get(userId);

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