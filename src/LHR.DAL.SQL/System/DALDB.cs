using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL.SQL.System
{
    public class DALDB : DALBase
    {
        public DALDB(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        
        public void CreateTable(string tableName, string sql)
        {
            if (!TableExists(tableName))
            {
                SqlCommand cmd = new SqlCommand(sql);
                ExecuteNonQuery(cmd);
            }
        }
    }
}
