using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Types.Base
{
    /// <summary>
    /// Field value for custom fields
    /// </summary>
    public class LhrFieldValue
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
