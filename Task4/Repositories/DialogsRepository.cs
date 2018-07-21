namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Task4.Repositories;

    public class DialogsRepository : IBaseRepository<Dialog>
    {
        private static volatile DialogsRepository instance;
        private static object syncRoot = new object();

        private List<Dialog> dialogs = new List<Dialog>();

        private DialogsRepository()
        {
        }

        public static DialogsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DialogsRepository();
                        }
                    }
                }

                return instance;
            }
        }

        public Dialog Get(int id)
        {
            Dialog dialog = this.dialogs.Find(a => a.Id == id);
            return dialog;
        }

        public List<Dialog> GetAll()
        {
            return this.dialogs;
        }

        public bool Save(Dialog entity)
        {
            if (entity == null)
            {
                return false;
            }

            Dialog dialog = this.dialogs.Find(a => a.Id == entity.Id);
            if (dialog == null)
            {
                this.dialogs.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Dialog dialog = this.dialogs.Find(a => a.Id == id);
            if (dialog == null)
            {
                return false;
            }

            this.dialogs.Remove(dialog);
            return true;
        }
    }
}