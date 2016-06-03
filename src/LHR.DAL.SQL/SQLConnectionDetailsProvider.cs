using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LHR.Types.System;

namespace LHR.DAL.SQL
{
    public class SQLConnectionDetailsProvider : IConnectionDetailsProvider
    {
        string connectionString;
        public SQLConnectionDetailsProvider(string settingsJson)
        {
            connectionString = JsonConvert.DeserializeObject<AppSettings>(settingsJson).DBConnectionString;
        }
        string IConnectionDetailsProvider.GetConnectionString()
        {
            return connectionString;
        }
    }
}
