using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.BL.Core
{
    public interface IDBManager
    {
        void CreateTable(string tableName, string sql);
    }
}
