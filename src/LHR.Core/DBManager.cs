using Lhr.Bl.Core;
using Lhr.Dal;
using Lhr.Dal.Sql;
using Lhr.Dal.Sql.System;
using Lhr.Types.System;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Core
{
    public class DbManager: IDbManager
    {
        IDalDb dal;
        public DbManager(IDalDb dalDB)
        {
            dal = dalDB;
        }
        void IDbManager.CreateTable(string tableName, string sql)
        {
            dal.CreateTable(tableName, sql);
        }
    }
}
