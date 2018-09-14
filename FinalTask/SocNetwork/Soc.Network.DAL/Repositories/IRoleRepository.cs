namespace DAL.Repositories
{
    using DAL.Entities.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IRoleRepository
    {
        UserRole Get(int id);

        List<UserRole> GetAll();

        bool Save(UserRole entity);

        bool Delete(int id);
    }
}
