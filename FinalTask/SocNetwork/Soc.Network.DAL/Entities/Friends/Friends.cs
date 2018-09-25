namespace DAL.Entities.Friends
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Friends
    {
        public Friends(int uid, int fid, int rel)
        {
            this.UserId = uid;
            this.FriendId = fid;
            this.RelationId = rel;
        }

        public int UserId { get; set; }

        public int FriendId { get; set; }

        public int RelationId { get; set; }

        public override bool Equals(object obj)
        {
            var friends = obj as Friends;
            return friends != null &&
                   this.UserId == friends.UserId &&
                   this.FriendId == friends.FriendId &&
                   this.RelationId == friends.RelationId;
        }

        public override int GetHashCode()
        {
            var hashCode = 62427208;
            hashCode = (hashCode * -1521134295) + this.UserId.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.FriendId.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.RelationId.GetHashCode();
            return hashCode;
        }
    }
}
