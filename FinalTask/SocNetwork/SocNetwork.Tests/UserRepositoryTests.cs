﻿namespace SocNetwork.Tests
{
    using System;
    using System.Collections.Generic;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserRepositoryTests
    {
        private string connectionString = ConnectionString.GetConnectionString();
        private string connectionDbType = ConnectionString.GetConnectionDbType();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);
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
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);
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
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);
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
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);
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
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);
            int nonExistId = 999;
            
            if (repo.Delete(nonExistId))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);

            User user = new User();
            user.Firstname = "Kek";
            user.Surname = "Keks";
            user.Email = "email";
            user.Password = "supersecurepass";
            user.UserRoleId = (int)UserRole.User;

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
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);

            User user = new User();
            user.Firstname = "Kek";
            user.Surname = "Keks";
            user.Email = "email";
            user.Password = "supersecurepass";
            user.UserRoleId = (int)UserRole.User;

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
            IUserRepository repo = new UserRepository(this.connectionString, this.connectionDbType);

            int uniqueId = 123;
            repo.Delete(uniqueId);

            User user = new User();
            user.Id = uniqueId;
            user.Firstname = "Kek";
            user.Surname = "Keks";
            user.Email = "email";
            user.Password = "supersecurepass";
            user.UserRoleId = (int)UserRole.User;

            if (!repo.Save(user))
            {
                Assert.Fail("Not saved");
            }

            if (user.Equals(repo.Get(user.Id)))
            {
                Assert.Fail("Not equal");
            }

            User u = repo.GetAll().Find(e => e.Email.Equals(user.Email));

            repo.Delete(u.Id);
        }
    }
}
