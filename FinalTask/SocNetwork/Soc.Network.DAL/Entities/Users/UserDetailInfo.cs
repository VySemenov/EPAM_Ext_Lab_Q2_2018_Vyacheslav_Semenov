namespace DAL.Entities.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserDetailInfo
    {
        public UserDetailInfo()
        {
            this.DateOfBirth = DateTime.Now;
            this.City = string.Empty;
            this.Interests = string.Empty;
        }

        public int UserId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }

        public string Interests { get; set; }
    }
}
