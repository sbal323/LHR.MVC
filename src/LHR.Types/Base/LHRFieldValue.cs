﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.Base
{
    public class LHRFieldValue
    {
        public string FieldName { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
    }
}
