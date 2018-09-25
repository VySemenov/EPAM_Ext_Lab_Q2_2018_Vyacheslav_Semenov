namespace DAL.ConnectionStrings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ConnectionString
    {
        public static string GetConnectionString()
        {
            return @"Data Source=BUG\EPAM_SQL17;Initial Catalog=SocNetwork;Integrated Security=True";
        }
    }
}
