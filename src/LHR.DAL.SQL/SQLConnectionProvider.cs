using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LHR.DAL.SQL
{
    public class SQLConnectionProvider: IConnectionProvider
    {
        public IConnectionDetailsProvider ConnectionDetails { get; set; }
        private SqlConnection connection;

        public SQLConnectionProvider(IConnectionDetailsProvider connectionDetails)
        {
            ConnectionDetails = connectionDetails;
        }

        public IDbConnection GetConnection()
        {
            if(null == connection)
            {
                connection = new SqlConnection(ConnectionDetails.GetConnectionString());
            }
            return connection;
        }
    }
}
