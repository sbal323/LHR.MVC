using Lhr.Dal.Sql.Orm;
using Lhr.Types.Constants;
using Lhr.Types.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Lhr.Dal;

namespace Lhr.Dal.Sql.System
{
    public class SqlDalGeneralSettings : DALBase, IDalGeneralSettings
    {
        public SqlDalGeneralSettings(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        GeneralSetting IDalGeneralSettings.GetSetting(Guid Id)
        {
            if (!TableExists(TableNames.GeneralSettings))
            {
                return null;
            }
            OrmManager orm = new OrmManager();
            string commandSQL = $"SELECT * From {TableNames.GeneralSettings} Where Id = @Id";
            SqlCommand cmd = new SqlCommand(commandSQL);
            cmd.Parameters.AddWithValue("@Id", Id);
            var rdr = ExecuteReader(cmd);
            if (rdr.HasRows)
                return orm.MapDataToBusinessEntity<GeneralSetting>(rdr);
            else
                return null;    
        }
        void IDalGeneralSettings.AddSetting(GeneralSetting setting)
        {
            OrmManager orm = new OrmManager();
            string commandSQL = $"SELECT count(*) From {TableNames.GeneralSettings} Where Id = @Id";
            SqlCommand cmd = new SqlCommand(commandSQL);
            cmd.Parameters.AddWithValue("@Id", setting.Id);
            if (!RecordExists(cmd))
            {
                string commandSQLInsert = $@"INSERT INTO {TableNames.GeneralSettings}
                                       ([Id]
                                       ,[Name]
                                       ,[Description]
                                       ,[Value]
                                       ,[DefaultValue]
                                       ,[Custom]
                                       ,[AddonName])
                                 VALUES
                                       (@Id,
                                       @Name,
                                       @Description,
                                       @Value,
                                       @DefaultValue,
                                       @Custom,
                                       @AddonName)";
                SqlCommand cmdInsert = new SqlCommand(commandSQLInsert);
                cmdInsert.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                cmdInsert.Parameters.Add("@Name", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@Description", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@Value", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@DefaultValue", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@Custom", SqlDbType.Bit);
                cmdInsert.Parameters.Add("@AddonName", SqlDbType.VarChar);
                orm.MapEntityToSQLParameters<GeneralSetting>(cmdInsert.Parameters, setting);
                ExecuteNonQuery(cmdInsert);
                cmdInsert.Dispose();
            }
            cmd.Dispose();
        }
    }
}
