using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.BL;
using LHR.Types.CoreHR;
using LHR.DAL;

namespace LHR.BL.Base
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BLEmployeeBase : BLBase, IBLEmployee
    {
        protected IDALEmployee DALEmployee { get; set; }
        public BLEmployeeBase(IDALEmployee dalEmployee)
        {
            DALEmployee = dalEmployee;
        }

        Employee IBLEmployee.Get(int id)
        {
            return DALEmployee.Get(id);
        }

        Employee IBLEmployee.GetByCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        Employee IBLEmployee.GetByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        List<Employee> IBLEmployee.GetManagers(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
