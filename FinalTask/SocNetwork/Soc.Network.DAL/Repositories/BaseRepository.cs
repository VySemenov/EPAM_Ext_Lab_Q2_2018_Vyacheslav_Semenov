namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private string connectionString;
        private string connectionDbType;

        public BaseRepository(string connectionString, string connectionDbType = "System.Data.SqlClient")
        {
            this.connectionString = connectionString;
            this.connectionDbType = connectionDbType;
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Save(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
