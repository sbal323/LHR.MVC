using Lhr.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Dal
{
    public interface IDalGeneralSettings
    {
        GeneralSetting GetSetting(Guid Id);
        void AddSetting(GeneralSetting setting);
    }
}
