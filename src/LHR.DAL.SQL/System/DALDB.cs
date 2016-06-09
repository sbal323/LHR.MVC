using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL.SQL.System
{
    public class DALDB : DALBase, IDALDB
    {
        public DALDB(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        
        void IDALDB.CreateTable(string tableName, string sql)
        {
            if (!TableExists(tableName))
            {
                SqlCommand cmd = new SqlCommand(sql);
                ExecuteNonQuery(cmd);
            }
        }
    }
}
