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
    public class MessageRepositoryTests
    {
        private string connectionString = ConnectionString.GetConnectionString();
        private string connectionDbType = ConnectionString.GetConnectionDbType();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            IMessageRepository repo = new MessageRepository(this.connectionString, this.connectionDbType);
            List<Message> messages = repo.GetAll();
            if (messages == null)
            {
                Assert.Fail();
            }
        }
    }
}
