using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL
{
    public interface ITransactionalConnectionProvider : IConnectionProvider
    {
        IDbTransaction GetTransaction();
        void BeginTransaction();
        void BeginTransaction(IsolationLevel isoLevel);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
