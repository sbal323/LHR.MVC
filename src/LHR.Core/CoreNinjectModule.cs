using Lhr.Dal;
using Lhr.Dal.Sql;
using Lhr.Types.System;
using Ninject.Modules;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ninject.Web.Common;
using Ninject.Parameters;
using Lhr.Dal.Sql.System;
using System.Web;
using Lhr.Bl.Core;

namespace Lhr.Core
{
    public class CoreNinjectModule : NinjectModule
    {
        public AppSettings ApplicationSettings { get; set; }
        public override void Load()
        {
            Bind<IConnectionDetailsProvider>().To<SqlConnectionDetailsProvider>().InSingletonScope().WithConstructorArgument("settingsJson", Newtonsoft.Json.JsonConvert.SerializeObject(ApplicationSettings));
            Bind<ITransactionalConnectionProvider>().To<SqlConnectionProvider>().InSingletonScope();
            Bind<IDalDb>().To<SqlDalDb>();
            Bind<IDalDi>().To<SqlDalDi>();
            Bind<IDalGeneralSettings>().To<SqlDalGeneralSettings>();
            Bind<IDbManager>().To<DbManager>();
            Bind<IDiManager>().To<DiManager>();
            Bind<IGeneralSettingsManager>().To<GeneralSettingsManager>();
            //Type contract = null, implementation = null;
            //Bind(contract).To(implementation);
        }
    }
}
