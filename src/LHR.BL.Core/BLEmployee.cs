using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.BL;
using LHR.Types;
using LHR.DAL;

namespace LHR.BL.Core
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BLEmployee : IBLEmployee
    {
        protected IDALEmployee DALEmployee { get; set; }
        public BLEmployee(IDALEmployee dalEmployee)
        {
            DALEmployee = dalEmployee;
        }

        public Employee Get(int id)
        {
            //throw new NotImplementedException();
            return DALEmployee.Get(id);
        }

        public Employee GetByCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        public Employee GetByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetManagers(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
