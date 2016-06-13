using Lhr.Types.Constants.Entities;
using Lhr.Types.Orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Types.System
{
    /// <summary>
    /// Setting for dependency injection
    /// </summary>
    public class DiSetting
    {
        /// <summary>
        /// Dependency injection scope
        /// Transient - create new object every time
        /// Scoped - create one object per HTTP request
        /// Instance - create one object per application
        /// </summary>
        public enum DiScope
        {
            Transient = 1,
            Scoped = 2,
            Instance = 3
        }
        /// <summary>
        /// Library reference type. 
        /// Static - fos statically linked libraries (typically interface libraries). 
        /// Dynamic - for 
        /// </summary>
        public enum DiLibraryReferenceType
        {
            Static = 1,
            Dynamic = 2
        }
        /// <summary>
        /// Setting Identifier for future reference
        /// </summary>
        [FieldName(DiSettingFieldNames.Id)]
        public Guid Id { get; set; }
        /// <summary>
        /// Assembly name of the contract
        /// </summary>
        [FieldName(DiSettingFieldNames.ContractAssemblyName)]
        public string ContractAssemblyName { get; set; }
        /// <summary>
        /// Type name of the contract
        /// </summary>
        [FieldName(DiSettingFieldNames.ContractTypeName)]
        public string ContractTypeName { get; set; }
        /// <summary>
        /// Assembly name of the implementation
        /// </summary>
        [FieldName(DiSettingFieldNames.ImplementationAssemblyName)]
        public string ImplementationAssemblyName { get; set; }
        /// <summary>
        /// Type name of the implementation
        /// </summary>
        [FieldName(DiSettingFieldNames.ImplementationTypeName)]
        public string ImplementationTypeName { get; set; }
        /// <summary>
        /// Dependency injection scope
        /// </summary>
        [FieldName(DiSettingFieldNames.Scope)]
        public DiScope Scope { get; set; }
        /// <summary>
        /// Library reference type of the contract
        /// </summary>
        [FieldName(DiSettingFieldNames.ContractLibraryReferenceType)]
        public DiLibraryReferenceType ContractLibraryReferenceType { get; set; }
        /// <summary>
        /// Library reference type of the implementation
        /// </summary>
        [FieldName(DiSettingFieldNames.ImplementationLibraryReferenceType)]
        public DiLibraryReferenceType ImplementationLibraryReferenceType { get; set; }

    }
}
