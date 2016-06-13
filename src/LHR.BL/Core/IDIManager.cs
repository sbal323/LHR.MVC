using Lhr.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Bl.Core
{
    public interface IDiManager
    {
        void AddSetting(DiSetting setting);
        List<DiSetting> GetSettings();
    }
}
