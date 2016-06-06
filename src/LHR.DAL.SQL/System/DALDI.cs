using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.System;
using System.Data.SqlClient;
using LHR.DAL.SQL.ORM;

namespace LHR.DAL.SQL.System
{
    public class DALDI : DALBase
    {
        public DALDI(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        public List<DISetting> GetAllSettings()
        {
            List<DISetting> result;
            ORMManager orm = new ORMManager();
            string commandSQL = "SELECT * From DISettings";
            SqlCommand cmd = new SqlCommand(commandSQL);
            SqlDataReader rdr = base.ExecuteReader(cmd);
            result = orm.MapDataToBusinessEntityCollection<DISetting>(rdr);
            cmd.Dispose();
            return result;
        }
    }
}
