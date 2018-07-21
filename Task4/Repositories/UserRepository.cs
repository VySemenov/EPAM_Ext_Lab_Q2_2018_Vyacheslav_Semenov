namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Task4.Repositories;

    public class UsersRepository : IBaseRepository<User>
    {
        private static volatile UsersRepository instance;
        private static object syncRoot = new object();

        private List<User> users = new List<User>();

        private UsersRepository()
        {
        }

        public static UsersRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UsersRepository();
                        }
                    }
                }

                return instance;
            }
        }

        public User Get(int id)
        {
            User user = this.users.Find(u => u.Id == id);
            return user;
        }

        public List<User> GetAll()
        {
            return this.users;
        }

        public bool Save(User entity)
        {
            if (entity == null)
            {
                return false;
            }

            User user = this.users.Find(u => u.Id == entity.Id);
            if (user == null)
            {
                this.users.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            User user = this.users.Find(u => u.Id == id);
            if (user == null)
            {
                return false;
            }

            this.users.Remove(user);
            return true;
        }
    }
}