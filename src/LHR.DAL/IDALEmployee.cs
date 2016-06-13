using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhr.Types.CoreHr;

namespace Lhr.Dal
{
    public interface IDalEmployee
    {
        Employee Get(int id);
        Employee GetByDepartment(int departmentId);
        Employee GetByCountry(int countryId);
    }
}
