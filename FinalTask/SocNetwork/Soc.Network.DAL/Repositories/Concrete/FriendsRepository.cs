namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.ConnectionStrings;
    using DAL.Entities.Friends;
    using DAL.Helpers;
    using DAL.Repositories.Abstract;

    public class FriendsRepository : IFriendsRepository
    {
        private const string UserId = "USER_ID";
        private const string FriendId = "FRIEND_ID";
        private const string Relation = "RELATION";

        private string connectionString;
        private string connectionDbType;

        public FriendsRepository()
        {
            this.connectionString = ConnectionString.GetConnectionString();
            this.connectionDbType = ConnectionString.GetConnectionDbType();
        }

        public FriendsRepository(string connectionString, string connectionDbType)
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public Friends Get(int uid, int fid)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_FRIENDS " +
                                      "WHERE (USER_ID = @UID AND FRIEND_ID = @FID) " +
                                         "OR (USER_ID = @FID AND FRIEND_ID = @UID)";
                command.AddParameter("@UID", uid);
                command.AddParameter("@FID", fid);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int relation = (int)reader[Relation];
                        int userId = (int)reader[UserId];
                        int friendId = (int)reader[FriendId];
                        Friends friends = new Friends(userId, friendId, relation);

                        return friends;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Friends> GetAllFriends(int uid)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_FRIENDS " +
                                      "WHERE USER_ID = @UID OR FRIEND_ID = @UID";
                command.AddParameter("@UID", uid);

                List<Friends> friendsList = new List<Friends>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int relation = (int)reader[Relation];
                        int userId = (int)reader[UserId];
                        int friendId = (int)reader[FriendId];
                        Friends friends = new Friends(userId, friendId, relation);

                        friendsList.Add(friends);
                    }
                }

                return friendsList;
            }
        }

        public List<Friends> GetAllFriends(int uid, int num)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT TOP(@NUM) * FROM dbo.M_FRIENDS " +
                                      "WHERE USER_ID = @UID OR FRIEND_ID = @UID";
                command.AddParameter("@NUM", num);
                command.AddParameter("@UID", uid);

                List<Friends> friendsList = new List<Friends>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int relation = (int)reader[Relation];
                        int userId = (int)reader[UserId];
                        int friendId = (int)reader[FriendId];
                        Friends friends = new Friends(userId, friendId, relation);

                        friendsList.Add(friends);
                    }
                }

                return friendsList;
            }
        }

        public List<Friends> GetFriendsByRelation(int uid, int rel)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_FRIENDS " +
                                      "WHERE (USER_ID = @UID AND RELATION = @REL) " +
                                         "OR (FRIEND_ID = @UID AND RELATION = @IREL)";
                command.AddParameter("@UID", uid);
                command.AddParameter("@REL", rel);
                command.AddParameter("IREL", -rel);

                List<Friends> friendsList = new List<Friends>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int relation = (int)reader[Relation];
                        int userId = (int)reader[UserId];
                        int friendId = (int)reader[FriendId];
                        Friends friends = new Friends(userId, friendId, relation);

                        friendsList.Add(friends);
                    }
                }

                return friendsList;
            }
        }

        public bool Save(Friends entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.UserId, entity.FriendId) != null)
                {
                    command.CommandText = "UPDATE dbo.M_FRIENDS SET " +
                                          "RELATION = @REL " +
                                          "WHERE (USER_ID = @UID AND FRIEND_ID = @FID) " + 
                                             "OR (USER_ID = @FID AND FRIEND_ID = @UID)";
                    command.AddParameter("@UID", entity.UserId);
                    command.AddParameter("@FID", entity.FriendId);
                    command.AddParameter("@REL", entity.RelationId);

                    var result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    command.CommandText = "INSERT INTO dbo.M_FRIENDS" +
                                          "(USER_ID, FRIEND_ID, RELATION) " +
                                          "VALUES (@UID, @FID, @REL)";
                    command.AddParameter("@UID", entity.UserId);
                    command.AddParameter("@FID", entity.FriendId);
                    command.AddParameter("@REL", entity.RelationId);

                    var result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public bool Delete(int uid, int fid)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(uid, fid) != null)
                {
                    command.CommandText = "DELETE FROM dbo.M_FRIENDS " + 
                                          "WHERE (USER_ID = @UID AND FRIEND_ID = @FID) " +
                                          "OR (USER_ID = @FID AND FRIEND_ID = @UID)";
                    command.AddParameter("@UID", uid);
                    command.AddParameter("@FID", fid);

                    var result = command.ExecuteNonQuery();

                    if (result == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
