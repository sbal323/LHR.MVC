using LHR.MVC.Modules.Application;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LHR.MVC
{
    public class LHRPluginAssemblyProvider : IAssemblyProvider
    {
        private readonly IFileProvider _fileProvider;
        private readonly IAssemblyLoadContextAccessor _loadContextAccessor;
        private readonly IAssemblyLoaderContainer _assemblyLoaderContainer;
        private readonly AppSettings _settings;

        public LHRPluginAssemblyProvider(
                IFileProvider fileProvider,
                IAssemblyLoadContextAccessor loadContextAccessor,
                IAssemblyLoaderContainer assemblyLoaderContainer,
                IOptions<AppSettings> settings)
        {
            _fileProvider = fileProvider;
            _loadContextAccessor = loadContextAccessor;
            _assemblyLoaderContainer = assemblyLoaderContainer;
            _settings = settings.Value;
        }

        /// <summary>
        /// Returns assemblies from /bin folders inside plugins in /Addons folder
        /// Examples:   /Addons/LHR.MVC.Plugin1/bin
        ///             /Addons/LHR.MVC.Plugin2/bin
        /// </summary>
        public IEnumerable<Assembly> CandidateAssemblies
        {
            get
            {
                var content = _fileProvider.GetDirectoryContents(_settings.AddonsFolderName);//LHRSystem.GetInstance().ApplicationSettings.AddonsFolderName);
                if (!content.Exists) yield break;
                foreach (var pluginDir in content.Where(x => x.IsDirectory))
                {
                    var binDir = new DirectoryInfo(Path.Combine(pluginDir.PhysicalPath, "bin"));
                    if (!binDir.Exists) continue;
                    foreach (var assembly in GetAssembliesInFolder(binDir))
                    {
                        yield return assembly;
                    }
                }
            }
        }

        /// <summary>
        /// Returns assemblies loaded from folder
        /// </summary>
        /// <param name="binPath">Path to assemblies folder</param>
        /// <returns></returns>
        private IEnumerable<Assembly> GetAssembliesInFolder(DirectoryInfo binPath)
        {
            // Use the default load context
            var loadContext = _loadContextAccessor.Default;

            // Add the loader to the container so that any call to Assembly.Load 
            // will call the load context back (if it's not already loaded)
            using (_assemblyLoaderContainer.AddLoader(
                new LHRDirectoryLoader(binPath, loadContext)))
            {
                foreach (var fileSystemInfo in binPath.GetFileSystemInfos("*.dll"))
                {
                    var assembly2 = loadContext.LoadFile(fileSystemInfo.FullName);
                    yield return assembly2;
                }
            }
        }
    }
}
