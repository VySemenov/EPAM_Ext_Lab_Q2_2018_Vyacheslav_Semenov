namespace DAL.Entities.Users
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities;
    using DAL.Entities.Dialogs;
    using DAL.Entities.Friends;
    using DAL.Entities.Posts;
    using DAL.ConnectionStrings;
    using DAL.Repositories;

    public class User
    {
        public User()
        {
            this.Firstname = string.Empty;
            this.Surname = string.Empty;
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.Dialogs = new List<Dialog>();
            this.Messages = new List<Message>();
            this.Posts = new List<Post>();
            this.UserRoleId = (int)UserRole.None;
            this.Friends = new List<Friends>();
            this.DetailInfo = new UserDetailInfo();
        }

        public User(int Id, string Firstname, string Surname, string Email, string Password, int UserRoleId)
        {
            this.Id = Id;
            this.Firstname = Firstname;
            this.Surname = Surname;
            this.Email = Email;
            this.Password = Password;
            this.UserRoleId = UserRoleId;

            string connectionString = ConnectionString.GetConnectionString();

            FriendsRepository friendsRepository = new FriendsRepository(connectionString);
            this.Friends = friendsRepository.GetAllFriends(this.Id);

            PostRepository postRepository = new PostRepository(connectionString);
            this.Posts = postRepository.GetPostsByPageId(this.Id).OrderByDescending(p => p.PublicationDate).ToList();

            UserDetailInfoRepository userDetailInfoRepository = new UserDetailInfoRepository(connectionString);
            this.DetailInfo = userDetailInfoRepository.Get(this.Id);
            if (this.DetailInfo == null)
            {
                this.DetailInfo = new UserDetailInfo();
            }
        }

        public int Id { get; set; }

        public UserDetailInfo DetailInfo { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Dialog> Dialogs { get; set; }

        public List<Message> Messages { get; set; }

        public List<Post> Posts { get; set; }

        public int UserRoleId { get; set; }

        public List<Friends> Friends { get; set; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   this.Id == user.Id &&
                   EqualityComparer<UserDetailInfo>.Default.Equals(this.DetailInfo, user.DetailInfo) &&
                   this.Firstname == user.Firstname &&
                   this.Surname == user.Surname &&
                   this.Email == user.Email &&
                   this.Password == user.Password &&
                   EqualityComparer<List<Dialog>>.Default.Equals(this.Dialogs, user.Dialogs) &&
                   EqualityComparer<List<Message>>.Default.Equals(this.Messages, user.Messages) &&
                   EqualityComparer<List<Post>>.Default.Equals(this.Posts, user.Posts) &&
                   this.UserRoleId == user.UserRoleId &&
                   EqualityComparer<List<Friends>>.Default.Equals(this.Friends, user.Friends);
        }

        public override int GetHashCode()
        {
            var hashCode = -307512703;
            hashCode = (hashCode * -1521134295) + this.Id.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<UserDetailInfo>.Default.GetHashCode(this.DetailInfo);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Firstname);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Surname);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Email);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Password);
            hashCode = (hashCode * -1521134295) + EqualityComparer<List<Dialog>>.Default.GetHashCode(this.Dialogs);
            hashCode = (hashCode * -1521134295) + EqualityComparer<List<Message>>.Default.GetHashCode(this.Messages);
            hashCode = (hashCode * -1521134295) + EqualityComparer<List<Post>>.Default.GetHashCode(this.Posts);
            hashCode = (hashCode * -1521134295) + this.UserRoleId.GetHashCode();
            hashCode = (hashCode * -1521134295) + EqualityComparer<List<Friends>>.Default.GetHashCode(this.Friends);
            return hashCode;
        }
    }
}
