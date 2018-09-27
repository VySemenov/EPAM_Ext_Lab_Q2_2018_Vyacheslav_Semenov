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
    using DAL.Entities.Users;
    using DAL.Helpers;
    using DAL.Repositories.Abstract;

    public class UserDetailInfoRepository : IUserDetailInfoRepository
    {
        private const string UserId = "UID";
        private const string City = "CITY";
        private const string DateOfBirth = "DOB";
        private const string Interests = "INTERESTS";
        private const string Avatar = "AVATARFILENAME";
        private const char Space = ' ';

        private string connectionString;
        private string connectionDbType;

        public UserDetailInfoRepository()
        {
            this.connectionString = ConnectionString.GetConnectionString();
            this.connectionDbType = ConnectionString.GetConnectionDbType();
        }

        public UserDetailInfoRepository(string connectionString, string connectionDbType)
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public UserDetailInfo Get(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_USERDETAILINFO " +
                                      "WHERE UID = @UID";
                command.AddParameter("@UID", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserDetailInfo info = new UserDetailInfo
                        {
                            UserId = (int)reader[UserId],
                            City = reader[City].ToString().Trim(Space),
                            DateOfBirth = DateTime.Parse(reader[DateOfBirth].ToString()),
                            Interests = reader[Interests].ToString().Trim(Space),
                            Avatar = reader[Avatar].ToString().Trim(Space)
                        };

                        return info;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<UserDetailInfo> GetAll()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_USERDETAILINFO";

                List<UserDetailInfo> infos = new List<UserDetailInfo>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserDetailInfo info = new UserDetailInfo
                        {
                            UserId = (int)reader[UserId],
                            City = reader[City].ToString().Trim(Space),
                            DateOfBirth = DateTime.Parse(reader[DateOfBirth].ToString()),
                            Interests = reader[Interests].ToString().Trim(Space),
                            Avatar = reader[Avatar].ToString().Trim(Space)
                        };

                        infos.Add(info);
                    }
                }

                return infos;
            }
        }

        public bool Save(UserDetailInfo entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.UserId) != null)
                {
                    command.CommandText = "UPDATE dbo.M_USERDETAILINFO SET " +
                                                        "CITY = @CITY, DOB = @DOB, INTERESTS = @INTERESTS, AVATARFILENAME = @AVATAR " +
                                                        "WHERE UID = @UID";
                    command.AddParameter("@CITY", entity.City);
                    command.AddParameter("@DOB", entity.DateOfBirth.ToString("yyyy-MM-dd"));
                    command.AddParameter("@INTERESTS", entity.Interests);
                    command.AddParameter("@UID", entity.UserId);
                    command.AddParameter("@AVATAR", entity.Avatar);

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
                    command.CommandText = "INSERT INTO dbo.M_USERDETAILINFO " +
                                            "(UID, CITY, DOB, INTERESTS, AVATARFILENAME) " +
                                          "VALUES (@UID, @CITY, @DOB, @INTERESTS, @AVATAR)";
                    command.AddParameter("@CITY", entity.City);
                    command.AddParameter("@DOB", entity.DateOfBirth.ToString("yyyy-MM-dd"));
                    command.AddParameter("@INTERESTS", entity.Interests);
                    command.AddParameter("@UID", entity.UserId);
                    command.AddParameter("@AVATAR", entity.Avatar);

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

        public bool Delete(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(id) != null)
                {
                    command.CommandText = "DELETE FROM dbo.M_USERDETAILINFO " +
                                          "WHERE UID = @UID";
                    command.AddParameter("@UID", id);

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
