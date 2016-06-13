using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Lhr.Types.System;

namespace Lhr.Dal.Sql
{
    public class SqlConnectionDetailsProvider : IConnectionDetailsProvider
    {
        string connectionString;
        public SqlConnectionDetailsProvider(string settingsJson)
        {
            connectionString = JsonConvert.DeserializeObject<AppSettings>(settingsJson).DbConnectionString;
        }
        string IConnectionDetailsProvider.GetConnectionString()
        {
            return connectionString;
        }
    }
}
