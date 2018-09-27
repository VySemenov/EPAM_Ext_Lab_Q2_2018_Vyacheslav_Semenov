namespace DAL.Repositories.Abstract
{
    using System.Collections.Generic;
    using DAL.Entities.Users;

    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> GetAll(int num);

        List<User> GetOffset(int offset, int fetch);
    }
}
