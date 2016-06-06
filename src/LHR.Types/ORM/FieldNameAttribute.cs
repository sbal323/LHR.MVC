using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.ORM
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : Attribute
    {
        public string FieldName { get; set; }
        public FieldNameAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
