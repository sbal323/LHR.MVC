using LHR.BL;
using LHR.DAL;
using LHR.Types.Constants;
using LHR.Types.Constants.Entities;
using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.MVC.Services.Updates.Updates
{
    public class Update_0_0_1 : IUpdate
    {
        UpdateVersion IUpdate.Version
        {
            get
            {
                return new UpdateVersion(0, 0, 1);
            }
        }
        /// <summary>
        /// Create DISettings, General Settings tables.
        /// Add base settings for dependency injection.
        /// Add Version setting
        /// </summary>
        /// <param name="manager"></param>
        void IUpdate.ApplyChanges(UpdateManager manager)
        {
			//Add tables
            string tableName = TableNames.DISettings;
            string sql = $@"CREATE TABLE {tableName}(
					[Id] [uniqueidentifier] NOT NULL,
					[ContractAssemblyName] [nvarchar](max) NOT NULL,
					[ContractTypeName] [nvarchar](max) NOT NULL,
					[ContractLibraryReferenceType] [nvarchar](max) NOT NULL,
					[ImplementationAssemblyName] [nvarchar](max) NOT NULL,
					[ImplementationTypeName] [nvarchar](max) NOT NULL,
					[ImplementationLibraryReferenceType] [nvarchar](max) NOT NULL,
					[Scope] [nvarchar](max) NOT NULL,
					CONSTRAINT [PK_DISettings] PRIMARY KEY CLUSTERED 
					(
					[Id] ASC
					))";
            manager.Core.CoreDDBManager.CreateTable(tableName,sql);
            tableName = TableNames.GeneralSettings;
            sql = $@"CREATE TABLE {tableName}(
					[Id] [uniqueidentifier] NOT NULL,
					[Name] [nvarchar](max) NOT NULL,
					[Description] [nvarchar](max) NULL,
					[Value] [nvarchar](max) NOT NULL,
					[DefaultValue] [nvarchar](max) NOT NULL,
					[Custom] [bit] NOT NULL,
					[AddonName] [nvarchar](max) NULL,
					CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
					(
					[Id] ASC
					))";
            manager.Core.CoreDDBManager.CreateTable(tableName, sql);
            //Register DI Components
            DISetting setting;
            setting = new DISetting
            {
                Id = DISettingsGUIDs.IDALEmployee,
                Scope = DISetting.DIScope.Transient,
                ContractAssemblyName = typeof(IDALEmployee).Assembly.FullName,
                ContractTypeName = typeof(IDALEmployee).FullName,
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = DIDefaultImplementation.DALSQLAssemblyName,
                ImplementationTypeName = DIDefaultImplementation.DALEmployeeSQL,
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            setting = new DISetting
            {
                Id = DISettingsGUIDs.IBLEmployee,
                Scope = DISetting.DIScope.Transient,
                ContractAssemblyName = typeof(IBLEmployee).Assembly.FullName,
                ContractTypeName = typeof(IBLEmployee).FullName,
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = DIDefaultImplementation.BLBaseAssemblyName,
                ImplementationTypeName = DIDefaultImplementation.BLEmployeeBase,
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            setting = new DISetting
            {
                Id = DISettingsGUIDs.IConnectionDetailsProvider,
                Scope = DISetting.DIScope.Instance,
                ContractAssemblyName = typeof(IConnectionDetailsProvider).Assembly.FullName,
                ContractTypeName = typeof(IConnectionDetailsProvider).FullName,
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = DIDefaultImplementation.DALSQLAssemblyName,
                ImplementationTypeName = DIDefaultImplementation.SQLConnectionDetailsProvider,
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            setting = new DISetting
            {
                Id = DISettingsGUIDs.IConnectionProvider,
                Scope = DISetting.DIScope.Scoped,
                ContractAssemblyName = typeof(IConnectionProvider).Assembly.FullName,
                ContractTypeName = typeof(IConnectionProvider).FullName,
                ContractLibraryReferenceType = DISetting.DILibraryReferenceType.Static,
                ImplementationAssemblyName = DIDefaultImplementation.DALSQLAssemblyName,
                ImplementationTypeName = DIDefaultImplementation.SQLConnectionProvider,
                ImplementationLibraryReferenceType = DISetting.DILibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            //Add settings
            GeneralSetting gs = new GeneralSetting
            {
				Id = GeleralSettingsGUIDs.SystemVersion,
				Name = "System Version",
				Value = "0.0.1",
				DefaultValue = "0.0.0",
				Custom = false,
				Description = "Lanteria HR System version"				
            };
            manager.Core.CoreGeneralSettingsManager.AddSetting(gs);
        }
    }
}
