using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL
{
    public interface IDALDI
    {
        List<DISetting> GetAllSettings();
        void AddSetting(DISetting setting);
    }
}
