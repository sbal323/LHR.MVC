using LHR.BL.Core;
using LHR.DAL;
using LHR.DAL.SQL;
using LHR.DAL.SQL.System;
using LHR.Types.Constants.Entities;
using LHR.Types.System;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Core
{
    public class GeneralSettingsManager: IGeneralSettingsManager
    {
        IDALGeneralSettings dal;
        GeneralSetting currentSystemVersion;
        public GeneralSettingsManager(IDALGeneralSettings dalGeneralSettings)
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
                var vers = dal.GetSetting(GeleralSettingsGUIDs.SystemVersion);
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
