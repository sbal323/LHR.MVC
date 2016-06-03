using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace LHR.DAL
{
    public interface IConnectionProvider
    {
        IDbConnection GetConnection();
    }
}
