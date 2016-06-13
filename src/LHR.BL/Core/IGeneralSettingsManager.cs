﻿using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.BL.Core
{
    public interface IGeneralSettingsManager
    {
        void AddSetting(GeneralSetting gs);
        GeneralSetting GetCurrentSystemVersion();
    }
}