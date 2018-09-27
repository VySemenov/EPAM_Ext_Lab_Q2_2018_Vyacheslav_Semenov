namespace DAL.Repositories.Abstract
{
    using System.Collections.Generic;
    using DAL.Entities.Dialogues;

    public interface IDialogRepository : IBaseRepository<Dialog>
    {
        Dialog Get(int userId, int interlocutorId);

        List<Dialog> GetAll(int userId);

        bool Delete(int userId, int interlocutorId);
    }
}
