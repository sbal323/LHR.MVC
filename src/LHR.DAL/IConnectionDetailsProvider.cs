using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Dal
{
    public interface IConnectionDetailsProvider
    {
        string GetConnectionString();
    }
}
