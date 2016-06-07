using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.CoreHR;

namespace LHR.BL
{
    public interface IBLEmployee
    {
        Employee Get(int id);
        List<Employee> GetManagers(int employeeId);
        Employee GetByDepartment(int departmentId);
        Employee GetByCountry(int countryId);
    }
}
