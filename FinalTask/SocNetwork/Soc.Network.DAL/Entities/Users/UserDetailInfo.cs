namespace DAL.Entities.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserDetailInfo
    {
        private const string DefaultAvatar = "default-avatar.png";
        private const string DefaultCity = "Default City";
        private const string DefaultInterests = "None";

        public UserDetailInfo()
        {
            this.DateOfBirth = DateTime.Now;
            this.City = DefaultCity;
            this.Interests = DefaultInterests;
            this.Avatar = DefaultAvatar;
        }

        public int UserId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string City { get; set; }

        public string Interests { get; set; }

        public string Avatar { get; set; }
    }
}
