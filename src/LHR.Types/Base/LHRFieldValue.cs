using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.Base
{
    /// <summary>
    /// Field value for custom fields
    /// </summary>
    public class LHRFieldValue
    {
        /// <summary>
        /// Field name
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Field value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Field Type
        /// </summary>
        public string Type { get; set; }
    }
}
