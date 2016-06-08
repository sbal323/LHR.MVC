using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.System
{
    /// <summary>
    /// Application settings for MVC project
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Path to the Folder with Addons (from the root of the application)
        /// </summary>
        public string AddonsFolderName { get; set; }
        /// <summary>
        /// Path to the Folder with Views (from the root of the application)
        /// </summary>
        public string PathToCoreViewsDirectory { get; set; }
        /// <summary>
        /// Path to the Folder with dynamically loaded binary libraries for dependency injecttion (from the root of the application)
        /// </summary>
        public string LibsFolderName { get; set; }
        /// <summary>
        /// Connection string to DataBase
        /// </summary>
        public string DBConnectionString { get; set; }
    }
}
