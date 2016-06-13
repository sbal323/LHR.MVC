using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhr.Bl;
using Lhr.Types.CoreHr;
using Lhr.Dal;

namespace Lhr.Bl.Base
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class BaseBLEmployee : BLBase, IBlEmployee
    {
        protected IDalEmployee DalEmployee { get; set; }
        public BaseBLEmployee(IDalEmployee dalEmployee)
        {
            DalEmployee = dalEmployee;
        }

        Employee IBlEmployee.Get(int id)
        {
            return DalEmployee.Get(id);
        }

        Employee IBlEmployee.GetByCountry(int countryId)
        {
            throw new NotImplementedException();
        }

        Employee IBlEmployee.GetByDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        List<Employee> IBlEmployee.GetManagers(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
