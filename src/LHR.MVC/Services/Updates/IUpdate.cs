using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.MVC.Services.Updates
{
    public interface IUpdate
    {
        UpdateVersion Version { get;}
        void ApplyChanges(UpdateManager manager);
    }
}
