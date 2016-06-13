using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Dal.Sql.System
{
    public class SqlDalDb : DALBase, IDalDb
    {
        public SqlDalDb(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        
        void IDalDb.CreateTable(string tableName, string sql)
        {
            if (!TableExists(tableName))
            {
                SqlCommand cmd = new SqlCommand(sql);
                ExecuteNonQuery(cmd);
            }
        }
    }
}
