using LHR.Types.ORM;
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

        [FieldName("Id")]
        public Guid Id { get; set; }
        [FieldName("ContractAssemblyName")]
        public string ContractAssemblyName { get; set; }
        [FieldName("ContractTypeName")]
        public string ContractTypeName { get; set; }
        [FieldName("ImplementationAssemblyName")]
        public string ImplementationAssemblyName { get; set; }
        [FieldName("ImplementationTypeName")]
        public string ImplementationTypeName { get; set; }
        [FieldName("Scope")]
        public DIScope Scope { get; set; }
        [FieldName("ContractLibraryReferenceType")]
        public DILibraryReferenceType ContractLibraryReferenceType { get; set; }
        [FieldName("ImplementationLibraryReferenceType")]
        public DILibraryReferenceType ImplementationLibraryReferenceType { get; set; }

    }
}
