using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.System;

namespace LHR.Core
{
    public class DIManager
    {
        List<DISetting> diSettings;
        public DIManager()
        {
            diSettings = new List<DISetting>();
        }
        public List<DISetting> GetSettings()
        {
            DISetting setting;
            setting = new DISetting
            {
                Scope = DISetting.DIScope.Transient,
                ContractAssemblyName = "LHR.DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ContractTypeName = "LHR.DAL.IDALEmployee",
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = "LHR.DAL.SQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ImplementationTypeName = "LHR.DAL.SQL.DALEmployee",
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            diSettings.Add(setting);
            setting = new DISetting
            {
                Scope = DISetting.DIScope.Transient,
                ContractAssemblyName = "LHR.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ContractTypeName = "LHR.BL.IBLEmployee",
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = "LHR.BL.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ImplementationTypeName = "LHR.BL.Core.BLEmployee",
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            diSettings.Add(setting);
            setting = new DISetting
            {
                Scope = DISetting.DIScope.Instance,
                ContractAssemblyName = "LHR.DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ContractTypeName = "LHR.DAL.IConnectionDetailsProvider",
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = "LHR.DAL.SQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ImplementationTypeName = "LHR.DAL.SQL.SQLConnectionDetailsProvider",
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            diSettings.Add(setting);
            setting = new DISetting
            {
                Scope = DISetting.DIScope.Scoped,
                ContractAssemblyName = "LHR.DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ContractTypeName = "LHR.DAL.IConnectionProvider",
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = "LHR.DAL.SQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                ImplementationTypeName = "LHR.DAL.SQL.SQLConnectionProvider",
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            diSettings.Add(setting);
            return diSettings;
        }
    }
}
