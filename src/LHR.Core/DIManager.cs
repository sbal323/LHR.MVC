using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.System;
using LHR.DAL.SQL.System;
using LHR.DAL.SQL;

namespace LHR.Core
{
    public class DIManager
    {
        //List<DISetting> diSettings;
        AppSettings settings;
        DALDI dal;
        SQLConnectionProvider cnnProvider;
        public DIManager(AppSettings appSettings)
        {
            //diSettings = new List<DISetting>();
            settings = appSettings;
            //TODO: replace with DI
            SQLConnectionDetailsProvider cdProvider = new SQLConnectionDetailsProvider(Newtonsoft.Json.JsonConvert.SerializeObject(settings));
            cnnProvider = new SQLConnectionProvider(cdProvider);
            dal = new DALDI(cnnProvider);
        }
        public void AddSetting(DISetting setting)
        {
            dal.AddSetting(setting);
        }
        public List<DISetting> GetSettings()
        {
            return dal.GetAllSettings();
        }
    }
}
