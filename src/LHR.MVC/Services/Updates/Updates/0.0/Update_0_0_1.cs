using Lhr.Bl;
using Lhr.Dal;
using Lhr.Types.Constants;
using Lhr.Types.Constants.Entities;
using Lhr.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Mvc.Services.Updates.Updates
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
            manager.Core.CoreDBBManager.CreateTable(tableName,sql);
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
            manager.Core.CoreDBBManager.CreateTable(tableName, sql);
            //Register DI Components
            DiSetting setting;
            setting = new DiSetting
            {
                Id = DiSettingsGuids.IDalEmployee,
                Scope = DiSetting.DiScope.Transient,
                ContractAssemblyName = typeof(IDalEmployee).Assembly.FullName,
                ContractTypeName = typeof(IDalEmployee).FullName,
                ContractLibraryReferenceType = DiSetting.DiLibraryReferenceType.Static,
                ImplementationAssemblyName = DiDefaultImplementation.DALSQLAssemblyName,
                ImplementationTypeName = DiDefaultImplementation.DALEmployeeSQL,
                ImplementationLibraryReferenceType = DiSetting.DiLibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            setting = new DiSetting
            {
                Id = DiSettingsGuids.IBlEmployee,
                Scope = DiSetting.DiScope.Transient,
                ContractAssemblyName = typeof(IBlEmployee).Assembly.FullName,
                ContractTypeName = typeof(IBlEmployee).FullName,
                ContractLibraryReferenceType = DiSetting.DiLibraryReferenceType.Static,
                ImplementationAssemblyName = DiDefaultImplementation.BLBaseAssemblyName,
                ImplementationTypeName = DiDefaultImplementation.BLEmployeeBase,
                ImplementationLibraryReferenceType = DiSetting.DiLibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            setting = new DiSetting
            {
                Id = DiSettingsGuids.IConnectionDetailsProvider,
                Scope = DiSetting.DiScope.Instance,
                ContractAssemblyName = typeof(IConnectionDetailsProvider).Assembly.FullName,
                ContractTypeName = typeof(IConnectionDetailsProvider).FullName,
                ContractLibraryReferenceType = DiSetting.DiLibraryReferenceType.Static,
                ImplementationAssemblyName = DiDefaultImplementation.DALSQLAssemblyName,
                ImplementationTypeName = DiDefaultImplementation.SQLConnectionDetailsProvider,
                ImplementationLibraryReferenceType = DiSetting.DiLibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            setting = new DiSetting
            {
                Id = DiSettingsGuids.IConnectionProvider,
                Scope = DiSetting.DiScope.Scoped,
                ContractAssemblyName = typeof(ITransactionalConnectionProvider).Assembly.FullName,
                ContractTypeName = typeof(ITransactionalConnectionProvider).FullName,
                ContractLibraryReferenceType = DiSetting.DiLibraryReferenceType.Static,
                ImplementationAssemblyName = DiDefaultImplementation.DALSQLAssemblyName,
                ImplementationTypeName = DiDefaultImplementation.SQLConnectionProvider,
                ImplementationLibraryReferenceType = DiSetting.DiLibraryReferenceType.Dynamic
            };
            manager.Core.CoreDIManager.AddSetting(setting);
            //Add settings
            GeneralSetting gs = new GeneralSetting
            {
				Id = GeleralSettingsGuids.SystemVersion,
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
