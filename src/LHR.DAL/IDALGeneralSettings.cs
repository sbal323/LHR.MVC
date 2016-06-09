using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL
{
    public interface IDALGeneralSettings
    {
        GeneralSetting GetSetting(Guid Id);
        void AddSetting(GeneralSetting setting);
    }
}
