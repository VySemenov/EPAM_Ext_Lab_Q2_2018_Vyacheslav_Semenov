namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Users;
    using DAL.Helpers;

    public class UserDetailInfoRepository : IUserDetailInfoRepository
    {
        private string connectionString;
        private string connectionDbType;

        public UserDetailInfoRepository(string connectionString, string connectionDbType = "System.Data.SqlClient")
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
                            UserId = (int)reader["UID"],
                            City = (string)reader["City"],
                            DateOfBirth = DateTime.Parse(reader["DOB"].ToString()),
                            Interests = (string)reader["INTERESTS"]
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
                            UserId = (int)reader["UID"],
                            City = (string)reader["City"],
                            DateOfBirth = DateTime.Parse(reader["DOB"].ToString()),
                            Interests = (string)reader["INTERESTS"]
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
                                                        "CITY = @CITY, DOB = @DOB, INTERESTS = @INTERESTS " +
                                                        "WHERE UID = @UID";
                    command.AddParameter("@CITY", entity.City);
                    command.AddParameter("@DOB", entity.DateOfBirth.ToString("yyyy-MM-dd"));
                    command.AddParameter("@INTERESTS", entity.Interests);
                    command.AddParameter("@UID", entity.UserId);

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
                                            "(UID, CITY, DOB, INTERESTS) " +
                                          "VALUES (@UID, @CITY, @DOB, @INTERESTS)";
                    command.AddParameter("@CITY", entity.City);
                    command.AddParameter("@DOB", entity.DateOfBirth.ToString("yyyy-MM-dd"));
                    command.AddParameter("@INTERESTS", entity.Interests);
                    command.AddParameter("@UID", entity.UserId);

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
