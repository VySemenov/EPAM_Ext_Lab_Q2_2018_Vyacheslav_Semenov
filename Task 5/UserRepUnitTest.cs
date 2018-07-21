namespace Task_5
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task4;

    [TestClass]
    public class UserRepUnitTest
    {
        public static void FillUsersRepository(int n = 10)
        {
            UsersRepository rep = UsersRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                RemoveAll();
            }

            for (int i = 0; i < n; i++)
            {
                rep.Save(new User { Id = i });
            }
        }

        public static void RemoveAll()
        {
            UsersRepository rep = UsersRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                List<User> list = new List<User>(rep.GetAll());

                foreach (var u in list)
                {
                    rep.Delete(u.Id);
                }
            }
        }

        [TestMethod]
        public void UserGetEmptyRandomIdTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            Random rnd = new Random();
            int testId = rnd.Next(-100, 100);
            User user = rep.Get(testId);
            if (user != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UserGetAllEmptyTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            List<User> list = rep.GetAll();
            if (list.Count != 0)
            {
                Assert.Fail("list.Count != 0");
            }
        }

        [TestMethod]
        public void UserSaveCountTest()
        {
            int countBefore = UsersRepository.Instance.GetAll().Count;

            UsersRepository.Instance.Save(new User());
            UsersRepository.Instance.Save(new User());
            UsersRepository.Instance.Save(new User());

            List<User> list = UsersRepository.Instance.GetAll();
            if (list.Count != countBefore + 1)
            {
                Assert.Fail("list.Count != countBefore + 1");
            }
        }

        [TestMethod]
        public void UserSaveCountTest2()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            int n = 10;
            FillUsersRepository(n);        

            List<User> list = rep.GetAll();

            if (list.Count != n)
            {
                Assert.Fail("list.Count != n");
            }
        }

        [TestMethod]
        public void UserSaveEqualsTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            int uniqueId = 111;
            User user = new User { Id = uniqueId, Firstname = "TestUser" };
            rep.Save(user);

            User userFromList = rep.Get(uniqueId);

            if (!user.Equals(userFromList))
            {
                Assert.Fail("!Equals");
            }
        }

        [TestMethod]
        public void UserSaveNullTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            if (rep.Save(null))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UserGetNotEmptyTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            int n = 10;

            FillUsersRepository(n);

            User user = null;
            for (int i = 0; i < n; i++)
            {
                user = UsersRepository.Instance.Get(i);
                if (user != null)
                {
                    if (user.Id != i)
                    {
                        Assert.Fail("user.Id != testId");
                    }
                }
                else
                {
                    Assert.Fail("user == null");
                }

                user = null;
            }
        }

        [TestMethod]
        public void UserGetAllEqualsTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            List<User> list = new List<User>
            {
                new User { Id = 1, Firstname = "1" },
                new User { Id = 2, Firstname = "2" },
                new User { Id = 3, Firstname = "3" },
                new User { Id = 4, Firstname = "4" },
                new User { Id = 5, Firstname = "1" },
                new User { Id = 6, Firstname = "123" },
                new User { Id = 7, Firstname = "2" },
            };

            foreach (var u in list)
            {
                rep.Save(u);
            }

            List<User> usersList = rep.GetAll();

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(usersList[i]))
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void UserDeleteEmptyTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UserDeleteNotEmptyTest()
        {
            UsersRepository rep = UsersRepository.Instance;
            RemoveAll();

            int n = 10;
            FillUsersRepository(n);

            for (int i = 0; i < n; i++)
            {
                if (!rep.Delete(i))
                {
                    Assert.Fail();
                }
            }

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }
    }
}
