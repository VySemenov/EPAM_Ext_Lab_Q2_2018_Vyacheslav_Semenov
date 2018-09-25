namespace SocNetwork.Tests
{
    using System;
    using System.Collections.Generic;
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RoleRepositoryTests
    {
        private string connectionString = ConnectionString.GetConnectionString();

        [TestMethod]
        public void GetAllNotNullTest()
        {
            RoleRepository roleRepository = new RoleRepository(this.connectionString);
            List<UserRole> roles = roleRepository.GetAll();
            if (roles == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetNotNullTest()
        {
            RoleRepository roleRepository = new RoleRepository(this.connectionString);
            UserRole role = roleRepository.Get((int)UserRole.Admin);
            if (role == UserRole.None || role != UserRole.Admin)
            {
                Assert.Fail();
            }
        }
    }
}
