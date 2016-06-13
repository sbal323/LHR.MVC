using Lhr.Bl.Core;
using Lhr.Core;
using Lhr.Types.System;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Mvc.Services.Core
{
    public class CoreMnager
    {
        public IDiManager CoreDIManager { get; set; }
        public IDbManager CoreDBBManager { get; set; }
        public IGeneralSettingsManager CoreGeneralSettingsManager { get; set; }
        public CoreMnager(AppSettings appSettings)
        {
            NinjectKernelProvider ninject = new NinjectKernelProvider(appSettings);
            CoreDIManager = ninject.Kernel.Get<IDiManager>();
            CoreDBBManager = ninject.Kernel.Get<IDbManager>();
            CoreGeneralSettingsManager = ninject.Kernel.Get<IGeneralSettingsManager>();
        }
    }
}
