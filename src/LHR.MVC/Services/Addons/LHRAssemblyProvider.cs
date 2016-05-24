using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Reflection;

namespace LHR.MVC
{

    public class LHRAssemblyProvider: DefaultAssemblyProvider
    {
        private readonly IAssemblyProvider[] _additionalProviders;
        private readonly string[] _referenceAssemblies;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="libraryManager"></param>
        /// <param name="additionalProviders">
        /// If passed in will concat the assemblies returned from these 
        /// providers with the default assemblies referenced
        /// </param>
        /// <param name="referenceAssemblies">
        /// If passed in it will filter the candidate libraries to ones
        /// that reference the assembly names passed in. 
        /// (i.e. "MyProduct.Web", "MyProduct.Core" )
        /// </param>
        public LHRAssemblyProvider(
            ILibraryManager libraryManager,
            IAssemblyProvider[] additionalProviders = null,
            string[] referenceAssemblies = null) : base(libraryManager)
        {
            _additionalProviders = additionalProviders;
            _referenceAssemblies = referenceAssemblies;
        }

        /// <summary>
        /// Uses the default filter if a custom list of reference
        /// assemblies has not been provided
        /// </summary>
        protected override HashSet<string> ReferenceAssemblies
            => _referenceAssemblies == null
                ? base.ReferenceAssemblies
                : new HashSet<string>(_referenceAssemblies);

        /// <summary>
        /// Returns the base Libraries referenced along with any DLLs/Libraries
        /// returned from the custom IAssemblyProvider passed in
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Library> GetCandidateLibraries()
        {
            var baseCandidates = base.GetCandidateLibraries();
            if (_additionalProviders == null) return baseCandidates;
            //return null;
            IEnumerable<Library> libs = _additionalProviders.SelectMany(provider => provider.CandidateAssemblies.Select(
                    x => new Library(x.FullName, null, null, null, Enumerable.Empty<string>(),
                        new[] { new AssemblyName(x.FullName) })));
            return baseCandidates.Concat(libs);
        }
    }
}
