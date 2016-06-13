using Lhr.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Dal
{
    public interface IDalDi
    {
        List<DiSetting> GetAllSettings();
        void AddSetting(DiSetting setting);
    }
}
