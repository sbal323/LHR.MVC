using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Lhr.Mvc
{
    public class LhrDirectoryLoader : IAssemblyLoader
    {
        private readonly IAssemblyLoadContext _context;
        private readonly DirectoryInfo _path;

        public LhrDirectoryLoader(DirectoryInfo path, IAssemblyLoadContext context)
        {
            _path = path;
            _context = context;
        }
        public Assembly Load(AssemblyName assemblyName)
        {
            return _context.LoadFile(Path.Combine(_path.FullName, assemblyName.Name + ".dll"));
        }
        public IntPtr LoadUnmanagedLibrary(string name)
        {
            //this isn't going to load any unmanaged libraries, just throw
            throw new NotImplementedException();
        }
    }
}
