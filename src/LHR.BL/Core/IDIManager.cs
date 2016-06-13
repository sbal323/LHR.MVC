using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.BL.Core
{
    public interface IDIManager
    {
        void AddSetting(DISetting setting);
        List<DISetting> GetSettings();
    }
}
