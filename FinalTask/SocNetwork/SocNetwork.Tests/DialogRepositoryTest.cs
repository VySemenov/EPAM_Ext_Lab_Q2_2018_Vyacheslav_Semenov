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
    public class DialogRepositoryTest
    {
        private string connectionString = ConnectionString.GetConnectionString();
        private string connectionDbType = ConnectionString.GetConnectionDbType();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            IDialogRepository repo = new DialogRepository(connectionString, connectionDbType);
            List<Dialog> dialogues = repo.GetAll();
            if (dialogues == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetAllNotEmptyest()
        {
            IDialogRepository repo = new DialogRepository(connectionString, connectionDbType);
            repo.Save(new Dialog() { UserId = 0, InterlocutorId = 0 });
            List<Dialog> dialogues = repo.GetAll();
            if (dialogues.Count < 1)
            {
                Assert.Fail();
            }

            Dialog dialog = repo.Get(0, 0);
            repo.Delete(dialog.Id);
        }

        [TestMethod]
        public void GetNotNullTest()
        {
            IDialogRepository repo = new DialogRepository(connectionString, connectionDbType);
            repo.Save(new Dialog() { UserId = 0, InterlocutorId = 0 });

            Dialog dialog = repo.Get(0, 0);
            if (dialog == null)
            {
                Assert.Fail();
            }

            repo.Delete(dialog.Id);
        }

        [TestMethod]
        public void SaveEqualsTest()
        {
            IDialogRepository repo = new DialogRepository(connectionString, connectionDbType);
            repo.Save(new Dialog() { UserId = 0, InterlocutorId = 0 });

            Dialog dialog = repo.Get(0, 0);
            if (dialog == null || dialog.UserId != 0 || dialog.InterlocutorId != 0)
            {
                Assert.Fail();
            }

            repo.Delete(dialog.Id);
        }

        [TestMethod]
        public void DeleteTest()
        {
            IDialogRepository repo = new DialogRepository(connectionString, connectionDbType);
            repo.Save(new Dialog() { UserId = 0, InterlocutorId = 0 });

            Dialog dialog = repo.Get(0, 0);
            if (!repo.Delete(dialog.Id))
            {
                Assert.Fail();
            }
        }
    }
}
