using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Reflection;
using Lhr.Types.Orm;
using Lhr.Types.Constants.Entities;

namespace Lhr.Types.Base
{
    /// <summary>
    /// Base class for Entity objects
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        [FieldName(EntityBaseFieldNames.Id)]
        public int Id { get; set; }
        /// <summary>
        /// Creation Date
        /// </summary>
        [FieldName(EntityBaseFieldNames.Created)]
        public DateTime Created { get; set; }
        /// <summary>
        /// Entity creator
        /// </summary>
        [FieldName(EntityBaseFieldNames.Author)]
        public string Author { get; set; }
        /// <summary>
        /// Last Modification date
        /// </summary>
        [FieldName(EntityBaseFieldNames.Modified)]
        public DateTime Modified { get; set; }
        /// <summary>
        /// Last editor
        /// </summary>
        [FieldName(EntityBaseFieldNames.Editor)]
        public string Editor { get; set; }
        /// <summary>
        /// List of Custom fields values
        /// </summary>
        public List<LhrFieldValue> CustomFieldsValues { get; }
        /// <summary>
        /// List of All fields values
        /// </summary>
        public List<LhrFieldValue> AllFieldValues
        {
            get
            {
                List<LhrFieldValue> res = new List<LhrFieldValue>(CustomFieldsValues);
                foreach (PropertyInfo property in this.GetType().GetTypeInfo().DeclaredProperties)
                {
                    if (property.GetIndexParameters().Length > 0)
                    {
                        continue;
                    }
                    object val = property.GetValue(this, null);
                    LhrFieldValue stdField = new LhrFieldValue
                    {
                        FieldName = property.Name,
                        Value = null == val ? string.Empty : property.GetValue(this, null).ToString(),
                        Type = property.PropertyType.ToString()
                    };
                    res.Add(stdField);
                }
                return res.OrderBy(x => x.FieldName).ToList() ;
            }
        }
        /// <summary>
        /// Public constructor
        /// </summary>
        public EntityBase()
        {
            CustomFieldsValues = new List<LhrFieldValue>();
        }
        /// <summary>
        /// Overrided ToString() function which print the entity properties and values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo property in this.GetType().GetTypeInfo().DeclaredProperties)
            {
                sb.Append($"{property.Name}: ");
                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    sb.Append(property.GetValue(this, null));
                }
                sb.Append(Environment.NewLine);
            }
            foreach (LhrFieldValue fv in this.CustomFieldsValues)
            {
                sb.Append($"{fv.FieldName} [{fv.Type.ToString()}]: {fv.Value.ToString() + Environment.NewLine}");
            }
            //sb.Append(new String('-', 10) + Environment.NewLine);
            //List<LHRFieldValue> allfields = this.AllFieldValues;
            //foreach (LHRFieldValue fv in allfields)
            //{
            //    sb.Append($"ALL {fv.FieldName} [{fv.Type.ToString()}]: {fv.Value.ToString() + Environment.NewLine}");
            //}
            return sb.ToString();
        }
        /// <summary>
        /// Return LHRFieldValue based on field name
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns></returns>
        public LhrFieldValue GetFieldValue(string fieldName)
        {
            LhrFieldValue res; 
            res = this.AllFieldValues.Where(x => x.FieldName == fieldName).FirstOrDefault();
            if(null == res)
                res = new LhrFieldValue();
            return res;
        }
    }
}
