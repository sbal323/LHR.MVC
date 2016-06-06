using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LHR.DAL.SQL
{
    public class DALBase
    {
        protected SqlConnection Connection { get; }
        //protected SqlTransaction Transaction { get; set; }
        ITransactionalConnectionProvider connectionProvider;
        public DALBase(ITransactionalConnectionProvider provider)
        {
            Connection = (SqlConnection)provider.GetConnection();
            connectionProvider = provider;
        }

        #region Transactions
        protected void InitCommand(SqlCommand command)
        {
            command.Connection = Connection;
            if (null != connectionProvider.GetTransaction())
            {
                command.Transaction = (SqlTransaction)connectionProvider.GetTransaction();
            }
            if(Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }
        protected void BeginTransaction()
        {
            connectionProvider.BeginTransaction();
        }
        protected void BeginTransaction(IsolationLevel isoLevel)
        {
            connectionProvider.BeginTransaction(isoLevel);
        }
        protected void CommitTransaction()
        {
            connectionProvider.CommitTransaction();
        }
        protected void RollbackTransaction()
        {
            connectionProvider.RollbackTransaction();
        }
        #endregion

        #region Readers
        protected int ExecuteNonQuery(SqlCommand command)
        {
            InitCommand(command);
            return command.ExecuteNonQuery();
        }
        protected object ExecuteScalar(SqlCommand command)
        {
            InitCommand(command);
            return command.ExecuteScalar();
        }
        protected SqlDataReader ExecuteReader(SqlCommand command)
        {
            InitCommand(command);
            return command.ExecuteReader();
        }
        #endregion

    }
}
