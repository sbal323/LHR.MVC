using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Types.Constants.Entities
{
    public static class DiDefaultImplementation
    {
        //Assembly names
        public const string DALSQLAssemblyName = "Lhr.Dal.Sql, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        public const string BLBaseAssemblyName = "Lhr.Bl.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        //Type names
        public const string DALEmployeeSQL = "Lhr.Dal.Sql.SqlDalEmployee";
        public const string BLEmployeeBase = "Lhr.Bl.Base.BaseBlEmployee";
        public const string SQLConnectionDetailsProvider = "Lhr.Dal.Sql.SqlConnectionDetailsProvider";
        public const string SQLConnectionProvider = "Lhr.Dal.Sql.SqlConnectionProvider";
    }
}
