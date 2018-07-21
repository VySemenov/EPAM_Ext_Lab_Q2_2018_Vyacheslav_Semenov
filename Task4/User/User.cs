namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class User
    {
        public int Id { get; set; }

        public UserDetailInfo DetailInfo { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public List<Dialog> Dialogs { get; set; }

        public List<Message> Messages { get; set; }

        public List<Post> Posts { get; set; }

        public UserRole UserRole { get; set; }

        public List<User> Friends { get; set; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   Id == user.Id &&
                   EqualityComparer<UserDetailInfo>.Default.Equals(DetailInfo, user.DetailInfo) &&
                   Firstname == user.Firstname &&
                   Surname == user.Surname &&
                   Email == user.Email &&
                   Phone == user.Phone &&
                   Password == user.Password &&
                   EqualityComparer<List<Dialog>>.Default.Equals(Dialogs, user.Dialogs) &&
                   EqualityComparer<List<Message>>.Default.Equals(Messages, user.Messages) &&
                   EqualityComparer<List<Post>>.Default.Equals(Posts, user.Posts) &&
                   UserRole == user.UserRole &&
                   EqualityComparer<List<User>>.Default.Equals(Friends, user.Friends);
        }

        public override int GetHashCode()
        {
            var hashCode = -307512703;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<UserDetailInfo>.Default.GetHashCode(DetailInfo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Firstname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Dialog>>.Default.GetHashCode(Dialogs);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Message>>.Default.GetHashCode(Messages);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Post>>.Default.GetHashCode(Posts);
            hashCode = hashCode * -1521134295 + UserRole.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<User>>.Default.GetHashCode(Friends);
            return hashCode;
        }
    }
}