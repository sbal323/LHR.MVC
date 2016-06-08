using System;
using System.Linq;
using System.Threading.Tasks;
using LHR.DAL;
using LHR.Types.CoreHR;
using LHR.Types.Base;
//using System.Data.SqlClient;
using System.Collections.Generic;

namespace LHR.DAL.SQL
{
    public class DALEmployee : DALBase, IDALEmployee
    {
        public DALEmployee(ITransactionalConnectionProvider provider): base(provider)
        {
        }
        Employee IDALEmployee.Get(int id)
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
            empl.CustomFieldsValues.Add(new LHRFieldValue()
            {
                FieldName = "Bank Details",
                Value = "U56478239 Banka Strausse NNTM",
                Type = typeof(string).ToString()
            });
            empl.CustomFieldsValues.Add(new LHRFieldValue()
            {
                FieldName = "Annual bonus",
                Value = 1890.ToString(),
                Type = typeof(decimal).ToString()
            });
            empl.CustomFieldsValues.Add(new LHRFieldValue()
            {
                FieldName = "External ID",
                Value = new Guid("{C0809003-C4CE-4EA6-9ED5-61A4E45B2703}").ToString(),
                Type = typeof(Guid).ToString()
            });
            return empl;
        }
        
        Employee IDALEmployee.GetByCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        Employee IDALEmployee.GetByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
