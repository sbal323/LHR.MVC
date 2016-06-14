using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhr.Types.System;
using System.Data.SqlClient;
using Lhr.Dal.Sql.Orm;
using System.Data;
using Lhr.Types.Constants;

namespace Lhr.Dal.Sql.System
{
    public class SqlDalDi : DalBase, IDalDi
    {
        public SqlDalDi(ITransactionalConnectionProvider provider) : base(provider)
        {
        }
        List<DiSetting> IDalDi.GetAllSettings()
        {
            List<DiSetting> result;
            OrmManager orm = new OrmManager();
            string commandSQL = $"SELECT * From {TableNames.DISettings}";
            SqlCommand cmd = new SqlCommand(commandSQL);
            SqlDataReader rdr = ExecuteReader(cmd);
            result = orm.MapDataToBusinessEntityCollection<DiSetting>(rdr);
            cmd.Dispose();
            return result;
        }
        void IDalDi.AddSetting(DiSetting setting)
        {
            OrmManager orm = new OrmManager();
            string commandSQL = $"SELECT count(*) From {TableNames.DISettings} Where Id = @Id" ;
            SqlCommand cmd = new SqlCommand(commandSQL);
            cmd.Parameters.AddWithValue("@Id", setting.Id);
            if (!RecordExists(cmd))
            {
                string commandSQLInsert = $@"INSERT INTO {TableNames.DISettings}
                   ([Id]
                    ,[ContractAssemblyName]
                    ,[ContractTypeName]
                    ,[ContractLibraryReferenceType]
                    ,[ImplementationAssemblyName]
                    ,[ImplementationTypeName]
                    ,[ImplementationLibraryReferenceType]
                    ,[Scope])
                    VALUES
                    (@Id,
                    @ContractAssemblyName,
                    @ContractTypeName,
                    @ContractLibraryReferenceType,
                    @ImplementationAssemblyName,
                    @ImplementationTypeName,
                    @ImplementationLibraryReferenceType,
                    @Scope)";
                SqlCommand cmdInsert = new SqlCommand(commandSQLInsert);
                cmdInsert.Parameters.Add("@Id", SqlDbType.UniqueIdentifier);
                cmdInsert.Parameters.Add("@ContractAssemblyName", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@ContractTypeName", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@ContractLibraryReferenceType", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@ImplementationAssemblyName", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@ImplementationTypeName", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@ImplementationLibraryReferenceType", SqlDbType.VarChar);
                cmdInsert.Parameters.Add("@Scope", SqlDbType.VarChar);
                orm.MapEntityToSQLParameters<DiSetting>(cmdInsert.Parameters, setting);
                ExecuteNonQuery(cmdInsert);
                cmdInsert.Dispose();
            }
            cmd.Dispose();
        }
    }
}
