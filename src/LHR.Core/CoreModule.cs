using LHR.DAL;
using LHR.DAL.SQL;
using LHR.Types.System;
using Ninject.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject.Web.Common;
using Ninject.Parameters;
using LHR.DAL.SQL.System;
using System.Web;
using LHR.Core.Contracts;

namespace LHR.Core
{
    public class CoreModule : NinjectModule
    {
        public AppSettings ApplicationSettings { get; set; }
        public NinjectKernelProvider KernelManager { get; set; }
        public override void Load()
        {
            Bind<IConnectionDetailsProvider>().To<SQLConnectionDetailsProvider>();
            Bind<ITransactionalConnectionProvider>().To<SQLConnectionProvider>().
                InScope(c => System.Threading.Thread.CurrentThread).
                WithConstructorArgument("connectionDetails",
                    ctx => ctx.Kernel.Get<IConnectionDetailsProvider>(new ConstructorArgument("settingsJson", Newtonsoft.Json.JsonConvert.SerializeObject(ApplicationSettings))));
            Bind<IDALDB>().To<DALDB>();
            Bind<IDALDI>().To<DALDI>();
            Bind<IDALGeneralSettings>().To<DALGeneralSettings>();
            Bind<IDBManager>().To<DBManager>().WithConstructorArgument("ninject", KernelManager);
            Bind<IDIManager>().To<DIManager>().WithConstructorArgument("ninject", KernelManager);
            Bind<IGeneralSettingsManager>().To<GeneralSettingsManager>().WithConstructorArgument("ninject", KernelManager);
        }
    }
}
