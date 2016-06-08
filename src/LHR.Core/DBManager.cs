using LHR.DAL.SQL;
using LHR.DAL.SQL.System;
using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Core
{
    public class DBManager
    {
        AppSettings settings;
        DALDB dal;
        SQLConnectionProvider cnnProvider;
        public DBManager(AppSettings appSettings)
        {
            settings = appSettings;
            //TODO: replace with DI
            SQLConnectionDetailsProvider cdProvider = new SQLConnectionDetailsProvider(Newtonsoft.Json.JsonConvert.SerializeObject(settings));
            cnnProvider = new SQLConnectionProvider(cdProvider);
            dal = new DALDB(cnnProvider);
        }
        public void CreateTable(string tableName, string sql)
        {
            dal.CreateTable(tableName, sql);
        }
    }
}
