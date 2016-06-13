using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.System;
using LHR.DAL.SQL.System;
using LHR.DAL.SQL;
using LHR.DAL;
using Ninject;
using LHR.BL.Core;

namespace LHR.Core
{
    public class DIManager: IDIManager
    {
        IDALDI dal;
        public DIManager(IDALDI dalDI)
        {
            dal = dalDI;
        }
        void IDIManager.AddSetting(DISetting setting)
        {
            dal.AddSetting(setting);
        }
        List<DISetting> IDIManager.GetSettings()
        {
            return dal.GetAllSettings();
        }
    }
}
