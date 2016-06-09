using LHR.Core;
using LHR.Core.Contracts;
using LHR.Types.System;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.MVC.Services.Core
{
    public class CoreMnager
    {
        public IDIManager CoreDIManager { get; set; }
        public IDBManager CoreDBBManager { get; set; }
        public IGeneralSettingsManager CoreGeneralSettingsManager { get; set; }
        public CoreMnager(AppSettings appSettings)
        {
            NinjectKernelProvider ninject = new NinjectKernelProvider(appSettings);
            CoreDIManager = ninject.Kernel.Get<IDIManager>();
            CoreDBBManager = ninject.Kernel.Get<IDBManager>();
            CoreGeneralSettingsManager = ninject.Kernel.Get<IGeneralSettingsManager>();
        }
    }
}
