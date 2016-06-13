using LHR.Types.System;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Core
{
    public class NinjectKernelProvider
    {
        public IKernel Kernel { get; set; }
        public NinjectKernelProvider(AppSettings appSettings)
        {
             Kernel = new StandardKernel(new CoreNinjectModule() { ApplicationSettings = appSettings});
        }
    }
}
