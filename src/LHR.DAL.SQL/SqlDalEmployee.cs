using System;
using System.Linq;
using System.Threading.Tasks;
using Lhr.Dal;
using Lhr.Types.CoreHr;
using Lhr.Types.Base;
//using System.Data.SqlClient;
using System.Collections.Generic;

namespace Lhr.Dal.Sql
{
    public class SqlDalEmployee : DalBase, IDalEmployee
    {
        public SqlDalEmployee(ITransactionalConnectionProvider provider): base(provider)
        {
        }
        Employee IDalEmployee.Get(int id)
        {
            Employee empl = new Employee()
            {
                FullName = "Sergey Balog",
                FirstName = "Sergey",
                LastName = "Balog",
                Id = 1,
                Created = new DateTime(2005, 1, 1),
                Author = "System Account",
            };
            empl.CustomFieldsValues.Add(new LhrFieldValue()
            {
                FieldName = "Bank Details",
                Value = "U56478239 Banka Strausse NNTM",
                Type = typeof(string).ToString()
            });
            empl.CustomFieldsValues.Add(new LhrFieldValue()
            {
                FieldName = "Annual bonus",
                Value = 1890.ToString(),
                Type = typeof(decimal).ToString()
            });
            empl.CustomFieldsValues.Add(new LhrFieldValue()
            {
                FieldName = "External ID",
                Value = new Guid("{C0809003-C4CE-4EA6-9ED5-61A4E45B2703}").ToString(),
                Type = typeof(Guid).ToString()
            });
            return empl;
        }
        
        Employee IDalEmployee.GetByCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        Employee IDalEmployee.GetByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
