using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Mvc.Services.Updates
{
    /// <summary>
    /// Version for update framework
    /// </summary>
    public class UpdateVersion
    {
        /// <summary>
        /// Version for update
        /// </summary>
        public System.Version Version { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="version">Version as string</param>
        public UpdateVersion(string version)
        {
            Version = new System.Version(version);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="major">Major number</param>
        /// <param name="minor">Minor number</param>
        /// <param name="build">Build number</param>
        public UpdateVersion(int major, int minor, int build)
        {
            Version = new System.Version($"{major}.{minor}.{build}");
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="major">Major number</param>
        /// <param name="minor">Minor number</param>
        /// <param name="build">Build number</param>
        /// <param name="revision">Revision number</param>
        public UpdateVersion(int major, int minor, int build, int revision)
        {
            Version = new System.Version($"{major}.{minor}.{build}.{revision}");
        }
        /// <summary>
        /// Check if current version in the range of 2 versions
        /// </summary>
        /// <param name="fromVersion">Start version in range</param>
        /// <param name="toVersion">End version in range</param>
        /// <returns></returns>
        public bool InRange(UpdateVersion fromVersion, UpdateVersion toVersion)
        {
            if (fromVersion.Version.CompareTo(this.Version) < 0 && this.Version.CompareTo(toVersion.Version) <= 0)
                return true;
            return false;
        }
        /// <summary>
        /// Overrige of ToString() function
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Version.ToString();
        }
    }
}
