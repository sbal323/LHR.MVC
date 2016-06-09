using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.System;
using LHR.DAL.SQL.System;
using LHR.DAL.SQL;
using LHR.DAL;
using Ninject;
using LHR.Core.Contracts;

namespace LHR.Core
{
    public class DIManager: IDIManager
    {
        IDALDI dal;
        public DIManager(NinjectKernelProvider ninject)
        {
            dal = ninject.Kernel.Get<IDALDI>();
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
