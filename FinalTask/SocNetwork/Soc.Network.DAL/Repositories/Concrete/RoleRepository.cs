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
    using DAL.ConnectionStrings;
    using DAL.Entities.Users;
    using DAL.Helpers;
    using DAL.Repositories.Abstract;

    public class RoleRepository : IRoleRepository
    {
        private const string RoleId = "RID";
        private const string RoleName = "ROLE_NAME";

        private string connectionString;
        private string connectionDbType;

        public RoleRepository()
        {
            this.connectionString = ConnectionString.GetConnectionString();
            this.connectionDbType = ConnectionString.GetConnectionDbType();
        }

        public RoleRepository(string connectionString, string connectionDbType)
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public UserRole Get(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_USERROLE " +
                                      "WHERE RID = @RID";
                command.AddParameter("@RID", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserRole role = (UserRole)((short)reader[RoleId]);

                        return role;
                    }
                    else
                    {
                        return UserRole.None;
                    }
                }
            }
        }

        public List<UserRole> GetAll()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_USERROLE";

                List<UserRole> roles = new List<UserRole>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserRole role = (UserRole)((short)reader[RoleId]);

                        roles.Add(role);
                    }
                }

                return roles;
            }
        }

        public bool Save(UserRole entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get((int)entity) != UserRole.None)
                {
                    command.CommandText = "UPDATE dbo.M_USERROLE SET " +
                                            "RID = @RID, ROLE_NAME = @ROLE " +
                                          "WHERE RID = @RID";
                    command.AddParameter("@RID", (int)entity);
                    command.AddParameter("@ROLE", entity.ToString());

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
                    command.CommandText = "INSERT INTO dbo.M_USERROLE" +
                                            "(RID, ROLE_NAME) " +
                                          "VALUES (@RID, @ROLE)";
                    command.AddParameter("@RID", (int)entity);
                    command.AddParameter("@ROLE", entity.ToString());

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

                if (this.Get(id) != UserRole.None)
                {
                    command.CommandText = "DELETE FROM dbo.M_USERROLE " +
                                          "WHERE RID = @RID";
                    command.AddParameter("@RID", id);

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