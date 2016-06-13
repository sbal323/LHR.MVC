using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Lhr.Types.System;
using Lhr.Core;
using Lhr.Bl.Core;

namespace Lhr.Mvc.Services.Di
{
    public class DiProvider
    {
        List<Assembly> loadedAssemblies = new List<Assembly>();
        AppSettings settings;
        PhysicalFileProvider rootFileProvider;
        IServiceCollection services;
        IDiManager coreDIManager;
        // Load libraries for dynamic dependencies
        public DiProvider(AppSettings appSettings, PhysicalFileProvider fileProvider, IServiceCollection serviceCollection, IDiManager diManager)
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
        private List<DiSetting> LoadSettings()
        {
            return coreDIManager.GetSettings();
        }
        public void RegisterDependencies()
        {
            List<DiSetting> loadedSettings = LoadSettings();
            Type contract, implementation;
            loadedSettings.ForEach(setting => {
                contract = GetDIType(setting.ContractLibraryReferenceType, setting.ContractAssemblyName, setting.ContractTypeName);
                implementation = GetDIType(setting.ImplementationLibraryReferenceType, setting.ImplementationAssemblyName, setting.ImplementationTypeName);
                RegisterService(setting.Scope, contract, implementation);
            });
        }
        private void RegisterService(DiSetting.DiScope scope, Type contract, Type implementation)
        {
            if(DiSetting.DiScope.Transient == scope)
            {
                services.AddTransient(contract, implementation);
            }
            else if (DiSetting.DiScope.Instance == scope)
            {
                services.AddInstance(contract, Activator.CreateInstance(implementation, new object[] { Newtonsoft.Json.JsonConvert.SerializeObject(settings) }));
            }
            else if (DiSetting.DiScope.Scoped == scope)
            {
                services.AddScoped(contract, implementation);
            }
        }

        private Type GetDIType(DiSetting.DiLibraryReferenceType contractLibraryReferenceType, string contractAssemblyName, string contractTypeName)
        {
            Type ret = null;
            if(DiSetting.DiLibraryReferenceType.Static == contractLibraryReferenceType)
            {
                ret = Assembly.Load(new AssemblyName(contractAssemblyName)).GetType(contractTypeName);
            }
            else if (DiSetting.DiLibraryReferenceType.Dynamic == contractLibraryReferenceType)
            {
                ret = loadedAssemblies.Where(x => x.FullName == contractAssemblyName).First().GetType(contractTypeName);
            }
            return ret;
        }
    }
}
