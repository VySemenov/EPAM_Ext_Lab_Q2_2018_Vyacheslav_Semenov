namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Posts;
    using DAL.Entities.Users;
    using DAL.Helpers;

    public class PostRepository : IPostRepository
    {
        private string connectionString;
        private string connectionDbType;

        public PostRepository(string connectionString, string connectionDbType = "System.Data.SqlClient")
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public Post Get(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_POSTS WHERE PID = @PID";
                command.AddParameter("@PID", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int pid = (int)reader["PID"];
                        int uid = (int)reader["AUTHOR_ID"];
                        int pageid = (int)reader["PAGE_ID"];
                        DateTime pdate = DateTime.Parse(reader["PDATE"].ToString());
                        string text = reader["CONTENT"].ToString();

                        return new Post(pid, uid, pageid, pdate, text);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Post> GetAll()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_POSTS";

                List<Post> posts = new List<Post>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int pid = (int)reader["PID"];
                        int uid = (int)reader["AUTHOR_ID"];
                        int pageid = (int)reader["PAGE_ID"];
                        DateTime pdate = DateTime.Parse(reader["PDATE"].ToString());
                        string text = reader["CONTENT"].ToString();

                        posts.Add(new Post(pid, uid, pageid, pdate, text));
                    }
                }

                return posts;
            }
        }

        public List<Post> GetPostsByUserId(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_POSTS WHERE AUTHOR_ID = @AUTHOR_ID";
                command.AddParameter("@AUTHOR_ID", id);

                List<Post> posts = new List<Post>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int pid = (int)reader["PID"];
                        int uid = (int)reader["AUTHOR_ID"];
                        int pageid = (int)reader["PAGE_ID"];
                        DateTime pdate = DateTime.Parse(reader["PDATE"].ToString());
                        string text = reader["CONTENT"].ToString();

                        posts.Add(new Post(pid, uid, pageid, pdate, text));
                    }
                }

                return posts;
            }
        }

        public List<Post> GetPostsByPageId(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_POSTS WHERE PAGE_ID = @PAGE_ID";
                command.AddParameter("@PAGE_ID", id);

                List<Post> posts = new List<Post>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int pid = (int)reader["PID"];
                        int uid = (int)reader["AUTHOR_ID"];
                        int pageid = (int)reader["PAGE_ID"];
                        DateTime pdate = DateTime.Parse(reader["PDATE"].ToString());
                        string text = reader["CONTENT"].ToString();

                        posts.Add(new Post(pid, uid, pageid, pdate, text));
                    }
                }

                return posts;
            }
        }

        public bool Save(Post entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.Id) != null)
                {
                    command.CommandText = "UPDATE dbo.M_POSTS SET " +
                                            "AUTHOR_ID = @AUTHOR_ID, " +
                                            "PAGE_ID = @PAGE_ID, " +
                                            "PDATE = @PDATE, " +
                                            "CONTENT = @CONTENT " +
                                          "WHERE PID = @PID";

                    command.AddParameter("@AUTHOR_ID", entity.AuthorId);
                    command.AddParameter("@PAGE_ID", entity.PageId);
                    command.AddParameter("@CONTENT", entity.Text);
                    command.AddParameter("@PDATE", entity.PublicationDate.ToString());
                    command.AddParameter("@PID", entity.Id);

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
                    command.CommandText = "INSERT INTO dbo.M_POSTS " +
                                          "(AUTHOR_ID, PAGE_ID, PDATE, CONTENT) " +
                                          "VALUES (@AUTHOR_ID, @PAGE_ID, @PDATE, @CONTENT)";

                    command.AddParameter("@AUTHOR_ID", entity.AuthorId);
                    command.AddParameter("@PAGE_ID", entity.PageId);
                    command.AddParameter("@CONTENT", entity.Text);
                    command.AddParameter("@PDATE", entity.PublicationDate.ToString());

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
                    command.CommandText = "DELETE FROM dbo.M_POSTS " +
                                          "WHERE PID = @PID";
                    command.AddParameter("@PID", id);

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
