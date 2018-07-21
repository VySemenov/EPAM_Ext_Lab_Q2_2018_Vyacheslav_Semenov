namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Task4.Repositories;

    public class MessagesRepository : IBaseRepository<Message>
    {
        private static volatile MessagesRepository instance;
        private static object syncRoot = new object();

        private List<Message> messages = new List<Message>();

        private MessagesRepository()
        {
        }

        public static MessagesRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MessagesRepository();
                        }
                    }
                }

                return instance;
            }
        }

        public Message Get(int id)
        {
            Message message = messages.Find(m => m.Id == id);

            return message;
        }

        public List<Message> GetAll()
        {
            return messages;
        }

        public bool Save(Message entity)
        {
            if (entity == null)
            {
                return false;
            }

            Message msg = messages.Find(m => m.Id == entity.Id);
            if (msg == null)
            {
                messages.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Message message = messages.Find(m => m.Id == id);
            if (message == null)
            {
                return false;
            }

            messages.Remove(message);
            return true;
        }
    }
}