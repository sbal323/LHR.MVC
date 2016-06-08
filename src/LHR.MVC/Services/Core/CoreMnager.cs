using LHR.Core;
using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.MVC.Services.Core
{
    public class CoreMnager
    {
        public DIManager CoreDIManager { get; set; }
        public DBManager CoreDDBManager { get; set; }
        public GeneralSettingsManager CoreGeneralSettingsManager { get; set; }
        public CoreMnager(AppSettings appSettings)
        {
            CoreDIManager = new DIManager(appSettings);
            CoreDDBManager = new DBManager(appSettings);
            CoreGeneralSettingsManager = new GeneralSettingsManager(appSettings);
        }
    }
}
