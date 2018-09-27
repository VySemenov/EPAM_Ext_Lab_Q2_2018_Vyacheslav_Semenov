namespace SocNetwork.Tests
{
    using System;
    using System.Collections.Generic;
    using DAL.ConnectionStrings;
    using DAL.Entities.Friends;
    using DAL.Entities.Posts;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PostRepositoryTests
    {
        private string connectionString = ConnectionString.GetConnectionString();
        private string connectionDbType = ConnectionString.GetConnectionDbType();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            PostRepository postRepository = new PostRepository(this.connectionString, this.connectionDbType);
            List<Post> posts = postRepository.GetAll();
            if (posts == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SaveTest()
        {
            PostRepository postRepository = new PostRepository(this.connectionString, this.connectionDbType);
            Post post = new Post(1, 1, 1, DateTime.Now, "kek");
            if (!postRepository.Save(post))
            {
                Assert.Fail();
            }

            postRepository.Delete(1);
        }
    }
}
