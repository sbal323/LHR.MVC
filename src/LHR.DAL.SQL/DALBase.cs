using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LHR.DAL.SQL
{
    public class DALBase:IDisposable
    {
        protected SqlConnection Connection { get; }
        protected SqlTransaction Transaction { get; set; }
        public DALBase(IConnectionProvider provider)
        {
            Connection = (SqlConnection)provider.GetConnection();
        }

        #region Transactions
        protected void InitCommand(SqlCommand command)
        {
            command.Connection = Connection;
            if (null != Transaction)
            {
                command.Transaction = Transaction;
            }
            if(Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
        }
        protected void BeginTransaction()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }
            Transaction = Connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
        }
        protected void BeginTransaction(System.Data.IsolationLevel isoLevel)
        {
            Transaction = Connection.BeginTransaction(isoLevel);
        }
        protected void CommitTransaction()
        {
            Transaction.Commit();
            Transaction = null;
        }
        protected void RollbackTransaction()
        {
            Transaction.Rollback();
            Transaction = null;
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    if(null!= Transaction)
                    {
                        CommitTransaction();
                    }
                    if (System.Data.ConnectionState.Open == Connection.State)
                    {
                        Connection.Close();
                    }
                }
                if (null != Transaction)
                {
                    Transaction.Dispose();
                }
                if (null != Connection)
                {
                    Connection.Dispose();
                }
                disposedValue = true;
            }
        }
        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DALBase()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }
        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
