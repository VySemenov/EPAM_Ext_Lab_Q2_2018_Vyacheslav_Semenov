namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Task4.Repositories;

    public class AttachmentsRepository : IBaseRepository<Attachment>
    {
        private static volatile AttachmentsRepository instance;
        private static object syncRoot = new object();

        private List<Attachment> attachments = new List<Attachment>();

        private AttachmentsRepository()
        {
        }

        public static AttachmentsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new AttachmentsRepository();
                        }
                    }
                }

                return instance;
            }
        }

        public Attachment Get(int id)
        {
            Attachment attachment = this.attachments.Find(a => a.Id == id);
            return attachment;
        }

        public List<Attachment> GetAll()
        {
            return this.attachments;
        }

        public bool Save(Attachment entity)
        {
            if (entity == null)
            {
                return false;
            }

            Attachment attachment = this.attachments.Find(a => a.Id == entity.Id);
            if (attachment == null)
            {
                this.attachments.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Attachment attachment = this.attachments.Find(a => a.Id == id);
            if (attachment == null)
            {
                return false;
            }

            this.attachments.Remove(attachment);
            return true;
        }
    }
}