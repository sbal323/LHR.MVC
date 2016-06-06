using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LHR.DAL.SQL
{
    public class SQLConnectionProvider: ITransactionalConnectionProvider, IDisposable
    {
        public IConnectionDetailsProvider ConnectionDetails { get; set; }
        private SqlConnection connection;
        private SqlTransaction transaction;

        public SQLConnectionProvider(IConnectionDetailsProvider connectionDetails)
        {
            ConnectionDetails = connectionDetails;
        }

        IDbConnection IConnectionProvider.GetConnection()
        {
            if(null == connection)
            {
                connection = new SqlConnection(ConnectionDetails.GetConnectionString());
            }
            return connection;
        }
        IDbTransaction ITransactionalConnectionProvider.GetTransaction()
        {
            return transaction;
        }

        #region Transaction
        void ITransactionalConnectionProvider.BeginTransaction()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }
        void ITransactionalConnectionProvider.BeginTransaction(IsolationLevel isoLevel)
        {
            transaction = connection.BeginTransaction(isoLevel);
        }
        void ITransactionalConnectionProvider.CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
        }
        void ITransactionalConnectionProvider.RollbackTransaction()
        {
            transaction.Rollback();
            transaction = null;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    if (null != transaction)
                    {
                        transaction.Commit();
                    }
                    if (ConnectionState.Open == connection.State)
                    {
                        connection.Close();
                    }
                    if (null != transaction)
                    {
                        transaction.Dispose();
                    }
                    if (null != connection)
                    {
                        connection.Dispose();
                    }
                }
                // dispose unmanaged objects
                disposedValue = true;
            }
        }
        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~SQLConnectionProvider()
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
