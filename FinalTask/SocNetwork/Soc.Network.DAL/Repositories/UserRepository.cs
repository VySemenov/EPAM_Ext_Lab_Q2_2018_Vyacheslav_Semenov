namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Users;
    using DAL.Helpers;

    public class UserRepository : IUserRepository
    {
        private string connectionString;
        private string connectionDbType;

        public UserRepository(string connectionString, string connectionDbType = "System.Data.SqlClient")
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public User Get(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_USERS " +
                                      "WHERE UID = @UID";
                command.AddParameter("@UID", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User((int)reader["UID"],
                            (string)reader["FIRSTNAME"],
                            (string)reader["LASTNAME"],
                            (string)reader["EMAIL"],
                            (string)reader["PASSWORD"],
                            (short)reader["ROLEID"]);

                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<User> GetAll()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_USERS";

                List<User> users = new List<User>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User((int)reader["UID"],
                            (string)reader["FIRSTNAME"],
                            (string)reader["LASTNAME"],
                            (string)reader["EMAIL"],
                            (string)reader["PASSWORD"],
                            (short)reader["ROLEID"]);

                        users.Add(user);
                    }
                }

                return users;
            }
        }

        public List<User> GetAll(int num)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.AddParameter("@num", num);

                command.CommandText = "[dbo].[GetAllUsers]";

                List<User> users = new List<User>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User((int)reader["UID"],
                            (string)reader["FIRSTNAME"],
                            (string)reader["LASTNAME"],
                            (string)reader["EMAIL"],
                            (string)reader["PASSWORD"],
                            (short)reader["ROLEID"]);

                        users.Add(user);
                    }
                }

                return users;
            }
        }

        public List<User> GetOffset(int offset, int fetch)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_USERS " +
                                      "ORDER BY UID " +
                                      "OFFSET @OFFSET ROWS FETCH NEXT @FETCH ROWS ONLY";
                command.AddParameter("@OFFSET", offset);
                command.AddParameter("@FETCH", fetch);

                List<User> users = new List<User>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User((int)reader["UID"],
                             (string)reader["FIRSTNAME"],
                             (string)reader["LASTNAME"],
                             (string)reader["EMAIL"],
                             (string)reader["PASSWORD"],
                             (short)reader["ROLEID"]);

                        users.Add(user);
                    }
                }

                return users;
            }
        }

        public bool Save(User entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.Id) != null)
                {
                    command.CommandText = "UPDATE dbo.M_USERS SET " +
                                          "FIRSTNAME = @FRNAME, LASTNAME = @LNAME, EMAIL = @EMAIL, PASSWORD = @PASS, ROLEID = @RID " +
                                          "WHERE UID = @UID";
                    command.AddParameter("@FRNAME", entity.Firstname);
                    command.AddParameter("LNAME", entity.Surname);
                    command.AddParameter("@EMAIL", entity.Email);
                    command.AddParameter("@PASS", entity.Password);
                    command.AddParameter("@RID", entity.UserRoleId);
                    command.AddParameter("@UID", entity.Id);

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
                    command.CommandText = "INSERT INTO dbo.M_USERS" +
                                          "(FIRSTNAME, LASTNAME, EMAIL, PASSWORD, ROLEID) " +
                                          "VALUES (@FRNAME, @LNAME, @EMAIL, @PASS, @RID)";
                    command.AddParameter("@FRNAME", entity.Firstname);
                    command.AddParameter("LNAME", entity.Surname);
                    command.AddParameter("@EMAIL", entity.Email);
                    command.AddParameter("@PASS", entity.Password);
                    command.AddParameter("@RID", entity.UserRoleId);

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
                    command.CommandText = "DELETE FROM dbo.M_USERS WHERE UID = @UID";
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
