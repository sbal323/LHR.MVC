using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.System
{
    public class AppSettings
    {
        public string AddonsFolderName { get; set; }
        public string PathToCoreViewsDirectory { get; set; }
        public string LibsFolderName { get; set; }
        public string DBConnectionString { get; set; }
    }
}
