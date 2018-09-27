namespace SocNetwork.Tests
{
    using System;
    using System.Collections.Generic;
    using DAL.ConnectionStrings;
    using DAL.Entities.Dialogues;
    using DAL.Entities.Friends;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using DAL.Repositories.Concrete;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserDetailInfoRepositoryTests
    {
        private string connectionString = ConnectionString.GetConnectionString();
        private string connectionDbType = ConnectionString.GetConnectionDbType();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            IUserDetailInfoRepository repo = new UserDetailInfoRepository(connectionString, connectionDbType);
            List<UserDetailInfo> infos = repo.GetAll();
            if (infos == null)
            {
                Assert.Fail();
            }
        }
    }
}
