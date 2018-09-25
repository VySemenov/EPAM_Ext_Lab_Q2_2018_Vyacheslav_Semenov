namespace DAL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Users;
    using DAL.Repositories;

    public class UserService : IUserService
    {
        private IUserRepository userRepo;

        public UserService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public User Get(int id)
        {
            return this.userRepo.Get(id);
        }

        public List<User> GetAll()
        {
            return this.userRepo.GetAll();
        }

        public bool Save(User entity)
        {
            return this.userRepo.Save(entity);
        }

        public bool Delete(int id)
        {
            return this.userRepo.Delete(id);
        }
    }
}
