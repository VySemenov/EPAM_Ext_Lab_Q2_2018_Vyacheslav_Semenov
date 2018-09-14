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

    public class RoleRepository : IRoleRepository
    {
        private string connectionString;
        private string connectionDbType;

        public RoleRepository(string connectionString, string connectionDbType = "System.Data.SqlClient")
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

                command.CommandText = string.Format("SELECT * FROM dbo.M_USERROLE WHERE UID = {0}", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserRole role = (UserRole)((short)reader["RID"]);

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
                        UserRole role = (UserRole)((short)reader["RID"]);

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
                    command.CommandText = string.Format("UPDATE dbo.M_USERROLE SET " +
                    "RID = '{0}', " +
                    "ROLE_NAME = '{1}' " +
                    "WHERE RID = '{2}'",
                    (int)entity,
                    entity.ToString());

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
                    command.CommandText = string.Format("INSERT INTO dbo.M_USERROLE" +
                    "(RID, " +
                    "ROLE_NAME " +
                    "VALUES ('{0}', '{1}')",
                    (int)entity,
                    entity.ToString());

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
                    command.CommandText = string.Format("DELETE FROM dbo.M_USERROLE WHERE RID = '{0}'", id);

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