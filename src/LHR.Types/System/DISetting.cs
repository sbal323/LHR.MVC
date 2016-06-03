using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.System
{
    public class DISetting
    {
        public enum DIScope
        {
            Transient = 1,
            Scoped = 2,
            Instance = 3
        }
        public enum DILibraryReferenceType
        {
            Static = 1,
            Dynamic = 2
        }

        public string ContractAssemblyName { get; set; }
        public string ContractTypeName { get; set; }
        public string ImplementationAssemblyName { get; set; }
        public string ImplementationTypeName { get; set; }
        public DIScope Scope { get; set; }
        public DILibraryReferenceType ContractLibraryReferenceType { get; set; }
        public DILibraryReferenceType ImplementationLibraryReferenceType { get; set; }

    }
}
