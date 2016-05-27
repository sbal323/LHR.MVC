using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LHR.DAL.SQL
{
    public class DALBase
    {
        protected IConnectionDetailsProvider ConnectionDetailsProvider { get; set; }
        protected SqlConnection Connection { get; set; }
        public DALBase(IConnectionDetailsProvider provider)
        {
            ConnectionDetailsProvider = provider;
            Connection = new SqlConnection(ConnectionDetailsProvider.GetConnectionString());
            //Connection.Open();
        }
    }
}
