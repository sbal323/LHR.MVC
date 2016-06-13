using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using LHR.Types.System;
using LHR.Core;
using LHR.BL.Core;

namespace LHR.MVC.Services.DI
{
    public class DIProvider
    {
        List<Assembly> loadedAssemblies = new List<Assembly>();
        AppSettings settings;
        PhysicalFileProvider rootFileProvider;
        IServiceCollection services;
        IDIManager coreDIManager;
        // Load libraries for dynamic dependencies
        public DIProvider(AppSettings appSettings, PhysicalFileProvider fileProvider, IServiceCollection serviceCollection, IDIManager diManager)
        {
            settings = appSettings;
            rootFileProvider = fileProvider;
            services = serviceCollection;
            coreDIManager = diManager;
        }

        public void LoadLibraries()
        {
            var libsFolder = new DirectoryInfo(rootFileProvider.Root + settings.LibsFolderName);
            if (libsFolder.Exists)
            {
                foreach (var fileSystemInfo in libsFolder.GetFileSystemInfos("*.dll"))
                {
                    loadedAssemblies.Add(PlatformServices.Default.AssemblyLoadContextAccessor.Default.LoadFile(fileSystemInfo.FullName));
                }
            }
        }
        private List<DISetting> LoadSettings()
        {
            return coreDIManager.GetSettings();
        }
        public void RegisterDependencies()
        {
            List<DISetting> loadedSettings = LoadSettings();
            Type contract, implementation;
            loadedSettings.ForEach(setting => {
                contract = GetDIType(setting.ContractLibraryReferenceType, setting.ContractAssemblyName, setting.ContractTypeName);
                implementation = GetDIType(setting.ImplementationLibraryReferenceType, setting.ImplementationAssemblyName, setting.ImplementationTypeName);
                RegisterService(setting.Scope, contract, implementation);
            });
        }
        private void RegisterService(DISetting.DIScope scope, Type contract, Type implementation)
        {
            if(DISetting.DIScope.Transient == scope)
            {
                services.AddTransient(contract, implementation);
            }
            else if (DISetting.DIScope.Instance == scope)
            {
                services.AddInstance(contract, Activator.CreateInstance(implementation, new object[] { Newtonsoft.Json.JsonConvert.SerializeObject(settings) }));
            }
            else if (DISetting.DIScope.Scoped == scope)
            {
                services.AddScoped(contract, implementation);
            }
        }

        private Type GetDIType(DISetting.DILibraryReferenceType contractLibraryReferenceType, string contractAssemblyName, string contractTypeName)
        {
            Type ret = null;
            if(DISetting.DILibraryReferenceType.Static == contractLibraryReferenceType)
            {
                ret = Assembly.Load(new AssemblyName(contractAssemblyName)).GetType(contractTypeName);
            }
            else if (DISetting.DILibraryReferenceType.Dynamic == contractLibraryReferenceType)
            {
                ret = loadedAssemblies.Where(x => x.FullName == contractAssemblyName).First().GetType(contractTypeName);
            }
            return ret;
        }
    }
}
