using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL
{
    public interface IDALDB
    {
        void CreateTable(string tableName, string sql);
    }
}
