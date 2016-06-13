using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Bl.Core
{
    public interface IDbManager
    {
        void CreateTable(string tableName, string sql);
    }
}
