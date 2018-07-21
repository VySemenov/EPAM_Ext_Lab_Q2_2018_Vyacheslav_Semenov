namespace Task_5
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task4;

    [TestClass]
    public class PostRepUnitTest
    {
        public void FillPostsRepository(int n = 10)
        {
            PostsRepository rep = PostsRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                RemoveAll();
            }

            for (int i = 0; i < n; i++)
            {
                rep.Save(new Post { Id = i });
            }
        }

        public void RemoveAll()
        {
            PostsRepository rep = PostsRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                List<Post> list = new List<Post>(rep.GetAll());

                foreach (var m in list)
                {
                    rep.Delete(m.Id);
                }
            }
        }

        [TestMethod]
        public void PostGetEmptyRandomIdTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            Random rnd = new Random();
            int testId = rnd.Next(-100, 100);
            Post post = rep.Get(testId);
            if (post != null)
            {
                Assert.Fail("");
            }
        }

        [TestMethod]
        public void PostGetAllEmptyTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            List<Post> list = rep.GetAll();
            if (list.Count != 0)
            {
                Assert.Fail("list.Count != 0");
            }
        }

        [TestMethod]
        public void PostSaveCountTest()
        {
            int countBefore = PostsRepository.Instance.GetAll().Count;

            PostsRepository.Instance.Save(new Post());
            PostsRepository.Instance.Save(new Post());
            PostsRepository.Instance.Save(new Post());

            List<Post> list = PostsRepository.Instance.GetAll();
            if (list.Count != countBefore + 1)
            {
                Assert.Fail("list.Count != countBefore + 1");
            }
        }

        [TestMethod]
        public void PostSaveCountTest2()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            int n = 10;
            FillPostsRepository(n);

            List<Post> list = rep.GetAll();

            if (list.Count != n)
            {
                Assert.Fail("list.Count != n");
            }
        }

        [TestMethod]
        public void PostSaveEqualsTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            int uniqueId = 111;
            Post post = new Post { Id = uniqueId };
            rep.Save(post);

            Post postFromList = rep.Get(uniqueId);

            if (!post.Equals(postFromList))
            {
                Assert.Fail("!Equals");
            }
        }

        [TestMethod]
        public void PostSaveNullTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            if (rep.Save(null))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PostGetNotEmptyTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            int n = 10;

            FillPostsRepository(n);

            Post post = null;
            for (int i = 0; i < n; i++)
            {
                post = PostsRepository.Instance.Get(i);
                if (post != null)
                {
                    if (post.Id != i)
                    {
                        Assert.Fail("post.Id != testId");
                    }
                }
                else
                {
                    Assert.Fail("post == null");
                }

                post = null;
            }
        }

        [TestMethod]
        public void PostGetAllEqualsTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            List<Post> list = new List<Post>
            {
                new Post { Id = 1},
                new Post { Id = 2},
                new Post { Id = 3},
                new Post { Id = 4},
                new Post { Id = 5},
                new Post { Id = 6},
                new Post { Id = 7},
            };

            foreach (var u in list)
            {
                rep.Save(u);
            }

            List<Post> postsList = rep.GetAll();

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(postsList[i]))
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void PostDeleteEmptyTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PostDeleteNotEmptyTest()
        {
            PostsRepository rep = PostsRepository.Instance;
            RemoveAll();

            int n = 10;
            FillPostsRepository(n);

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
