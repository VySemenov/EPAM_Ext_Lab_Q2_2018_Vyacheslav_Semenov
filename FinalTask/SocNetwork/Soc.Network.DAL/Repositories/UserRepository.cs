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

                command.CommandText = string.Format("SELECT * FROM dbo.M_USERS WHERE UID = {0}", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["UID"];//todo pn лучше все же именование колонок вынести куда-нибудь в константы, поскольку ты их используешь здесь неоднократно
                        user.Firstname = (string)reader["FIRSTNAME"];
                        user.Surname = (string)reader["LASTNAME"];
                        user.Email = (string)reader["EMAIL"];
                        user.Password = (string)reader["PASSWORD"];
                        user.UserRole = (UserRole)((short)reader["ROLEID"]);

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
                        User user = new User();
                        user.Id = (int)reader["UID"];
                        user.Firstname = (string)reader["FIRSTNAME"];
                        user.Surname = (string)reader["LASTNAME"];
                        user.Email = (string)reader["EMAIL"];
                        user.Password = (string)reader["PASSWORD"];
                        user.UserRole = (UserRole)((short)reader["ROLEID"]);

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
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@num";
                param.Value = num;
                command.Parameters.Add(param);

                command.CommandText = "[dbo].[GetAllUsers]";

                List<User> users = new List<User>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = (int)reader["UID"];
                        user.Firstname = (string)reader["FIRSTNAME"];
                        user.Surname = (string)reader["LASTNAME"];
                        user.Email = (string)reader["EMAIL"];
                        user.Password = (string)reader["PASSWORD"];
                        user.UserRole = (UserRole)((short)reader["ROLEID"]);

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
                    command.CommandText = string.Format("UPDATE dbo.M_USERS SET " +
                    "FIRSTNAME = '{0}', " +
                    "LASTNAME = '{1}', " +
                    "EMAIL = '{2}', " +
                    "PASSWORD = '{3}', " +
                    "ROLEID = '{4}' " +
                    "WHERE UID = '{5}'",
                    entity.Firstname,
                    entity.Surname,
                    entity.Email,
                    entity.Password,
                    entity.UserRole,
                    entity.Id);

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
                    command.CommandText = string.Format("INSERT INTO dbo.M_USERS" +
                    "(FIRSTNAME, " +
                    "LASTNAME, " +
                    "EMAIL, " +
                    "PASSWORD, " +
                    "ROLEID) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                    entity.Firstname,
                    entity.Surname,
                    entity.Email,
                    entity.Password,
                    (int)entity.UserRole);

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
                    command.CommandText = string.Format("DELETE FROM dbo.M_USERS WHERE UID = '{0}'", id);

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
