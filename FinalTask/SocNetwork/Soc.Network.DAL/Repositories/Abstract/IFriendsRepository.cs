namespace DAL.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Friends;

    public interface IFriendsRepository
    {
        Friends Get(int uid, int fid);

        List<Friends> GetAllFriends(int uid);

        List<Friends> GetAllFriends(int uid, int num);

        List<Friends> GetFriendsByRelation(int uid, int rel);

        bool Save(Friends entity);

        bool Delete(int uid, int fid);
    }
}
