using Lhr.Bl.Core;
using Lhr.Dal;
using Lhr.Dal.Sql;
using Lhr.Dal.Sql.System;
using Lhr.Types.Constants.Entities;
using Lhr.Types.System;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Core
{
    public class GeneralSettingsManager: IGeneralSettingsManager
    {
        IDalGeneralSettings dal;
        GeneralSetting currentSystemVersion;
        public GeneralSettingsManager(IDalGeneralSettings dalGeneralSettings)
        {
            dal = dalGeneralSettings;
        }
        void IGeneralSettingsManager.AddSetting(GeneralSetting gs)
        {
            dal.AddSetting(gs);
        }
        GeneralSetting IGeneralSettingsManager.GetCurrentSystemVersion()
        {
            if (null == currentSystemVersion)
            {
                var vers = dal.GetSetting(GeleralSettingsGuids.SystemVersion);
                if (null != vers)
                    currentSystemVersion = vers;
                else
                {
                    currentSystemVersion = new GeneralSetting
                    {
                        Value = "0.0.0"
                    };
                }
            }
            return currentSystemVersion;
        }
    }
}
