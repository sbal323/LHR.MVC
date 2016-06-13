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
using LHR.BL.Core;

namespace LHR.Core
{
    public class CoreNinjectModule : NinjectModule
    {
        public AppSettings ApplicationSettings { get; set; }
        public override void Load()
        {
            Bind<IConnectionDetailsProvider>().To<SQLConnectionDetailsProvider>().InSingletonScope().WithConstructorArgument("settingsJson", Newtonsoft.Json.JsonConvert.SerializeObject(ApplicationSettings));
            Bind<ITransactionalConnectionProvider>().To<SQLConnectionProvider>().InSingletonScope();
            Bind<IDALDB>().To<DALDB>();
            Bind<IDALDI>().To<DALDI>();
            Bind<IDALGeneralSettings>().To<DALGeneralSettings>();
            Bind<IDBManager>().To<DBManager>();
            Bind<IDIManager>().To<DIManager>();
            Bind<IGeneralSettingsManager>().To<GeneralSettingsManager>();
            Type contract = null, implementation = null;
            Bind(contract).To(implementation);
        }
    }
}
