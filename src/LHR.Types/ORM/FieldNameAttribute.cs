using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.ORM
{
    /// <summary>
    /// Attribut to map entity object with database resultset's field name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : Attribute
    {
        /// <summary>
        /// Database Field Name
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="fieldName">Database Field Name</param>
        public FieldNameAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
