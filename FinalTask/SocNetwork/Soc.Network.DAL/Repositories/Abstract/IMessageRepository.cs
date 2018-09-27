namespace DAL.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Dialogues;

    public interface IMessageRepository : IBaseRepository<Message>
    {
        List<Message> GetAllByUser(int userId);

        List<Message> GetAllByDialog(int dialogId);
    }
}
