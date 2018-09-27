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

    public class MessageRepository : IMessageRepository
    {
        private const string MessageId = "MESSAGE_ID";
        private const string DialogId = "DIALOG_ID";
        private const string AuthorId = "AUTHOR_ID";
        private const string Time = "TIME";
        private const string Text = "TEXT";
        private const char Space = ' ';

        private string connectionString;
        private string connectionDbType;

        public MessageRepository()
        {
            this.connectionString = ConnectionString.GetConnectionString();
            this.connectionDbType = ConnectionString.GetConnectionDbType();
        }

        public MessageRepository(string connectionString, string connectionDbType)
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public Message Get(int id)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM dbo.M_MESSAGES " +
                                      "WHERE MESSAGE_ID = @MID";
                command.AddParameter("@MID", id);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int dialogId = (int)reader[DialogId];
                        int authorId = (int)reader[AuthorId];
                        DateTime time = DateTime.Parse(reader[Time].ToString());
                        string text = reader[Text].ToString().Trim(Space);
                        Message message = new Message(id, dialogId, authorId, time, text);

                        return message;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public List<Message> GetAll()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_MESSAGES";

                List<Message> messagesList = new List<Message>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int messageId = (int)reader[MessageId];
                        int dialogId = (int)reader[DialogId];
                        int authorId = (int)reader[AuthorId];
                        DateTime time = DateTime.Parse(reader[Time].ToString());
                        string text = reader[Text].ToString().Trim(Space);
                        Message message = new Message(messageId, dialogId, authorId, time, text);

                        messagesList.Add(message);
                    }
                }

                return messagesList;
            }
        }

        public List<Message> GetAllByUser(int userId)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_MESSAGES" +
                                      "WHERE AUTHOR_ID = @UID";
                command.AddParameter("@UID", userId);

                List<Message> messagesList = new List<Message>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int messageId = (int)reader[MessageId];
                        int dialogId = (int)reader[DialogId];
                        int authorId = (int)reader[AuthorId];
                        DateTime time = DateTime.Parse(reader[Time].ToString());
                        string text = reader[Text].ToString().Trim(Space);
                        Message message = new Message(messageId, dialogId, authorId, time, text);

                        messagesList.Add(message);
                    }
                }

                return messagesList;
            }
        }

        public List<Message> GetAllByDialog(int dialogId)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM dbo.M_MESSAGES " +
                                      "WHERE DIALOG_ID = @UID";
                command.AddParameter("@UID", dialogId);

                List<Message> messagesList = new List<Message>();
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int messageId = (int)reader[MessageId];
                        int authorId = (int)reader[AuthorId];
                        DateTime time = DateTime.Parse(reader[Time].ToString());
                        string text = reader[Text].ToString().Trim(Space);
                        Message message = new Message(messageId, dialogId, authorId, time, text);

                        messagesList.Add(message);
                    }
                }

                return messagesList;
            }
        }

        public bool Save(Message entity)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(this.connectionDbType);

            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                IDbCommand command = connection.CreateCommand();

                if (this.Get(entity.Id) != null)
                {
                    command.CommandText = "UPDATE dbo.M_MESSAGES SET " +
                                          "TEXT = @TEXT " +
                                          "WHERE MESSAGE_ID = @MID";
                    command.AddParameter("@MID", entity.Id);
                    command.AddParameter("@TEXT", entity.Text);

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
                    command.CommandText = "INSERT INTO dbo.M_MESSAGES " +
                                          "(DIALOG_ID, AUTHOR_ID, TIME, TEXT) " +
                                          "VALUES (@DID, @AID, @TIME, @TEXT)";
                    command.AddParameter("@DID", entity.DialogId);
                    command.AddParameter("@AID", entity.AuthorId);
                    command.AddParameter("@TIME", entity.Time.ToString());
                    command.AddParameter("@TEXT", entity.Text);

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
                    command.CommandText = "DELETE FROM dbo.M_MESSAGES " +
                                          "WHERE MESSAGE_ID = @ID";
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
    }
}
