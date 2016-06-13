using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Lhr.Dal
{
    public interface IConnectionProvider
    {
        IDbConnection GetConnection();
    }
}
