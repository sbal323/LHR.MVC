using LHR.DAL.SQL.ORM;
using LHR.Types.Constants;
using LHR.Types.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.DAL.SQL.System
{
    public class DALGeneralSettings : DALBase
    {
        public DALGeneralSettings(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        public GeneralSetting GetSetting(Guid Id)
        {
            if (!TableExists(TableNames.GeneralSettings))
            {
                return null;
            }
            ORMManager orm = new ORMManager();
            string commandSQL = $"SELECT * From {TableNames.GeneralSettings} Where Id = @Id";
            SqlCommand cmd = new SqlCommand(commandSQL);
            cmd.Parameters.AddWithValue("@Id", Id);
            var rdr = ExecuteReader(cmd);
            if (rdr.HasRows)
                return orm.MapDataToBusinessEntity<GeneralSetting>(rdr);
            else
                return null;    
        }
        public void AddSetting(GeneralSetting setting)
        {
            ORMManager orm = new ORMManager();
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
