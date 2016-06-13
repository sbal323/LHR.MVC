using LHR.BL.Core;
using LHR.DAL;
using LHR.DAL.SQL;
using LHR.DAL.SQL.System;
using LHR.Types.System;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Core
{
    public class DBManager: IDBManager
    {
        IDALDB dal;
        public DBManager(IDALDB dalDB)
        {
            dal = dalDB;
        }
        void IDBManager.CreateTable(string tableName, string sql)
        {
            dal.CreateTable(tableName, sql);
        }
    }
}
