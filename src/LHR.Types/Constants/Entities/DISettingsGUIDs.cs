using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhr.Types.Constants.Entities
{
    public static class DiSettingsGuids
    {
        #region Lhr.Dal
        public static Guid IDalEmployee
        {
            get
            {
                return new Guid("6EAB12CA-41D4-4152-87B1-9FCAF2ECBA70");
            }
        }
        public static Guid IConnectionDetailsProvider
        {
            get
            {
                return new Guid("4277E2C7-D5B2-4A16-833C-17B27CD575C9");
            }
        }
        public static Guid IConnectionProvider
        {
            get
            {
                return new Guid("B2994C90-3F44-4ED1-A0E3-B3EF664F1D8E");
            }
        }
        #endregion

        #region Lhr.Bl
        public static Guid IBlEmployee
        {
            get
            {
                return new Guid("ABBB03E3-8B5A-402B-BFD0-2DBE96F5CD4B");
            }
        }
        #endregion

    }
}
