using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhr.Types.Base;

namespace Lhr.Types.CoreHr
{
    /// <summary>
    /// Entiry representing Employee
    /// </summary>
    public class Employee: EntityBase
    {
        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }
        public int OrgUnitId { get; set; }
        public int JobRoleId { get; set; }
        public int CountryId { get; set; }
        public int LocationId { get; set; }
    }
}
