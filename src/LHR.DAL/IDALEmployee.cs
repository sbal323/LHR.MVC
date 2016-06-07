using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.CoreHR;

namespace LHR.DAL
{
    public interface IDALEmployee
    {
        Employee Get(int id);
        Employee GetByDepartment(int departmentId);
        Employee GetByCountry(int countryId);
    }
}
