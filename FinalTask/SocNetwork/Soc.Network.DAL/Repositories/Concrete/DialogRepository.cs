namespace DAL.Repositories.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.ConnectionStrings;
    using DAL.Entities.Dialogues;
    using DAL.Helpers;
    using DAL.Repositories.Abstract;

    public class DialogRepository : IDialogRepository
    {
        private const string UserId = "USER_ID";
        private const string InterlocutorId = "INTERLOCUTOR_ID";
        private const string DialogId = "DIALOG_ID";

        private string connectionString;
        private string connectionDbType;

        public DialogRepository()
        {
            this.connectionString = ConnectionString.GetConnectionString();
            this.connectionDbType = ConnectionString.GetConnectionDbType();
        }

        public DialogRepository(string connectionString, string connectionDbType)
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public Dialog Get(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_DIALOGUES " +
                                      "WHERE DIALOG_ID = @DID";
                command.AddParameter("@DID", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userId = (int)reader[UserId];
                        int interlocutorId = (int)reader[InterlocutorId];
                        Dialog dialog = new Dialog(id, userId, interlocutorId);

                        return dialog;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public Dialog Get(int userId, int interlocutorId)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_DIALOGUES " +
                                      "WHERE (USER_ID = @UID AND INTERLOCUTOR_ID = @IID) " +
                                         "OR (USER_ID = @IID AND INTERLOCUTOR_ID = @UID)";
                command.AddParameter("@UID", userId);
                command.AddParameter("@IID", interlocutorId);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int id = (int)reader[DialogId];
                        Dialog dialog = new Dialog(id, userId, interlocutorId);

                        return dialog;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Dialog> GetAll()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_DIALOGUES";

                List<Dialog> dialoguesList = new List<Dialog>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[DialogId];
                        int userId = (int)reader[UserId];
                        int interlocutorId = (int)reader[InterlocutorId];
                        Dialog dialog = new Dialog(id, userId, interlocutorId);

                        dialoguesList.Add(dialog);
                    }
                }

                return dialoguesList;
            }
        }

        public List<Dialog> GetAll(int userId)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_DIALOGUES " +
                                      "WHERE USER_ID = @UID OR INTERLOCUTOR_ID = @UID";
                command.AddParameter("@UID", userId);

                List<Dialog> dialoguesList = new List<Dialog>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = (int)reader[DialogId];
                        int uId = (int)reader[UserId];
                        int interlocutorId = (int)reader[InterlocutorId];
                        Dialog dialog = new Dialog(id, uId, interlocutorId);

                        dialoguesList.Add(dialog);
                    }
                }

                return dialoguesList;
            }
        }

        public bool Save(Dialog entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.UserId, entity.InterlocutorId) != null)
                {
                    return true;
                }
                else
                {
                    command.CommandText = "INSERT INTO dbo.M_DIALOGUES" +
                                          "(USER_ID, INTERLOCUTOR_ID) " +
                                          "VALUES (@UID, @IID)";
                    command.AddParameter("@UID", entity.UserId);
                    command.AddParameter("@IID", entity.InterlocutorId);

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
                    command.CommandText = "DELETE FROM dbo.M_DIALOGUES " +
                                          "WHERE DIALOG_ID = @ID";
                    command.AddParameter("@ID", id);

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

        public bool Delete(int userId, int interlocutorId)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(userId, interlocutorId) != null)
                {
                    command.CommandText = "DELETE FROM dbo.M_DIALOGUES " +
                                          "WHERE (USER_ID = @UID AND INTERLOCUTOR_ID = @IID) " +
                                             "OR (USER_ID = @IID AND INTERLOCUTOR_ID = @UID )";
                    command.AddParameter("@UID", userId);
                    command.AddParameter("@IID", interlocutorId);

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
