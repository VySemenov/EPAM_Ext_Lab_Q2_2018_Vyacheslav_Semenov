namespace DAL.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Entities.Posts;

    public interface IPostRepository : IBaseRepository<Post>
    {
        List<Post> GetPostsByUserId(int id);

        List<Post> GetPostsByPageId(int id);
    }
}
