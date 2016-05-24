using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types
{
    public class TypeBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
        public DateTime Modified { get; set; }
        public string Editor { get; set; }

    }
}
