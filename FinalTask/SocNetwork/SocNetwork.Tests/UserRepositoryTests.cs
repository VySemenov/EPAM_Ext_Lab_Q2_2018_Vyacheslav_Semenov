namespace SocNetwork.Tests
{
    using System;
    using System.Collections.Generic;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserRepositoryTests
    {
        private string connectionString = @"Data Source=BUG\EPAM_SQL17;Initial Catalog=SocNetwork;Integrated Security=True";

        [TestMethod]
        public void GetAllNotNullTest()
        {
            UserRepository repo = new UserRepository(this.connectionString);
            List<User> users = repo.GetAll();
            if (users.Count == 0)
            {
                Assert.Fail();
            }

            foreach (var u in users)
            {
                Console.WriteLine(u.Id + " " + u.Firstname);
            }
        }

        [TestMethod]
        public void GetNotNullTest()
        {
            UserRepository repo = new UserRepository(this.connectionString);
            User user = repo.Get(1);
            if (user == null)
            {
                Assert.Fail();
            }

            Console.WriteLine(user.Id + " " + user.Firstname);
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            int num = 2;
            UserRepository repo = new UserRepository(this.connectionString);
            List<User> users = repo.GetAll(num);
            if (users.Count != num)
            {
                Assert.Fail();
            }

            foreach (var u in users)
            {
                Console.WriteLine(u.Id + " " + u.Firstname);
            }
        }

        [TestMethod]
        public void GetNullTest()
        {
            UserRepository repo = new UserRepository(this.connectionString);
            int nonExistId = 999;

            User user = repo.Get(nonExistId);
            if (user != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteNonExistTest()
        {
            IUserRepository repo = new UserRepository(this.connectionString);
            int nonExistId = 999;
            
            if (repo.Delete(nonExistId))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            IUserRepository repo = new UserRepository(this.connectionString);

            User user = new User();
            user.Firstname = "Kek";
            user.Surname = "Keks";
            user.Email = "email";
            user.Password = "supersecurepass";
            user.UserRoleId = UserRole.User;

            if (!repo.Save(user))
            {
                Assert.Fail();
            }

            User u = repo.GetAll().Find(e => e.Email.Equals(user.Email));

            if (!repo.Delete(u.Id))
            {
                Assert.Fail();
            }

            if (repo.Get(u.Id) != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SaveTest()
        {
            IUserRepository repo = new UserRepository(this.connectionString);

            User user = new User();
            user.Firstname = "Kek";
            user.Surname = "Keks";
            user.Email = "email";
            user.Password = "supersecurepass";
            user.UserRoleId = UserRole.User;

            if (!repo.Save(user))
            {
                Assert.Fail();
            }

            User u = repo.GetAll().Find(e => e.Email.Equals(user.Email));

            repo.Delete(u.Id);
        }

        [TestMethod]
        public void SaveEqualTest()
        {
            IUserRepository repo = new UserRepository(this.connectionString);

            int uniqueId = 123;
            repo.Delete(uniqueId);

            User user = new User();
            user.Id = uniqueId;
            user.Firstname = "Kek";
            user.Surname = "Keks";
            user.Email = "email";
            user.Password = "supersecurepass";
            user.UserRoleId = UserRole.User;

            if (!repo.Save(user))
            {
                Assert.Fail("Not saved");
            }

            if (user.Equals(repo.Get(user.Id)))
            {
                Assert.Fail("Not equal");
            }

            repo.Delete(uniqueId);
        }
    }
}
