using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Reflection;

namespace LHR.Types.Base
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public DateTime Modified { get; set; }
        public string Editor { get; set; }
        public List<LHRFieldValue> CustomFields { get; }
        public EntityBase()
        {
            CustomFields = new List<LHRFieldValue>();
        }
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
            foreach (LHRFieldValue fv in this.CustomFields)
            {
                sb.Append($"Custom field {fv.FieldName} [{fv.Type.ToString()}]: {fv.Value.ToString() + Environment.NewLine}");
            }
            return sb.ToString();
        }
    }
}
