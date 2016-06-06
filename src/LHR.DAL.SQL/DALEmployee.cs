using System;
using System.Linq;
using System.Threading.Tasks;
using LHR.DAL;
using LHR.Types;
//using System.Data.SqlClient;

namespace LHR.DAL.SQL
{
    public class DALEmployee : DALBase, IDALEmployee
    {
        public DALEmployee(ITransactionalConnectionProvider provider): base(provider)
        {
        }
        Employee IDALEmployee.Get(int id)
        {
                return new Employee()
                {
                    FullName = "Sergey Balog",
                    FirstName = "Sergey",
                    LastName = "Balog",
                    Id = 1,
                    Created = new DateTime(2005, 1, 1),
                    Author = "System Account"
                };
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
