namespace SocNetwork.Tests
{
    using System;
    using System.Collections.Generic;
    using DAL.ConnectionStrings;
    using DAL.Entities.Friends;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FriendsRepositoryTests
    {
        private string connectionString = ConnectionString.GetConnectionString();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            FriendsRepository friendsRepository = new FriendsRepository(this.connectionString);
            int uid = 1;
            int fid = 2;
            int rel = 0;
            friendsRepository.Save(new Friends(uid, fid, rel));
            List<Friends> fr = friendsRepository.GetAllFriends(1);
            if (fr == null || !fr.Contains(new Friends(uid, fid, rel)))
            {
                friendsRepository.Delete(uid, fid);
                Assert.Fail();
            }
            friendsRepository.Delete(uid, fid);
        }

        [TestMethod]
        public void SaveTest()
        {
            FriendsRepository friendsRepository = new FriendsRepository(this.connectionString);
            int uid = 1;
            int fid = 2;
            int rel = 0;
            if (!friendsRepository.Save(new Friends(uid, fid, rel)))
            {
                Assert.Fail();
            }

            friendsRepository.Delete(uid, fid);
        }

        [TestMethod]
        public void SaveEqualsTest()
        {
            FriendsRepository friendsRepository = new FriendsRepository(this.connectionString);
            int uid = 1;
            int fid = 2;
            int rel = 0;
            Friends friends = new Friends(uid, fid, rel);
            friendsRepository.Save(friends);

            Friends fr = friendsRepository.Get(uid, fid);

            if (!fr.Equals(friends))
            {
                friendsRepository.Delete(uid, fid);
                Assert.Fail();
            }

            friendsRepository.Delete(uid, fid);
        }
    }
}
