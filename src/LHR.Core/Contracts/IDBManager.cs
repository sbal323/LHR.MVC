using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Core.Contracts
{
    public interface IDBManager
    {
        void CreateTable(string tableName, string sql);
    }
}
