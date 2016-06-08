using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.Constants.Entities
{
    public static class DIDefaultImplementation
    {
        //Assembly names
        public const string DALSQLAssemblyName = "LHR.DAL.SQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        public const string BLBaseAssemblyName = "LHR.BL.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        //Type names
        public const string DALEmployeeSQL = "LHR.DAL.SQL.DALEmployee";
        public const string BLEmployeeBase = "LHR.BL.Base.BLEmployeeBase";
        public const string SQLConnectionDetailsProvider = "LHR.DAL.SQL.SQLConnectionDetailsProvider";
        public const string SQLConnectionProvider = "LHR.DAL.SQL.SQLConnectionProvider";
    }
}
