using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHR.Types.System;
using System.Data.SqlClient;
using LHR.DAL.SQL.ORM;
using System.Data;
using LHR.Types.Constants;

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
            string commandSQL = $"SELECT * From {TableNames.DISettings}";
            SqlCommand cmd = new SqlCommand(commandSQL);
            SqlDataReader rdr = ExecuteReader(cmd);
            result = orm.MapDataToBusinessEntityCollection<DISetting>(rdr);
            cmd.Dispose();
            return result;
        }
        public void AddSetting(DISetting setting)
        {
            ORMManager orm = new ORMManager();
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
                orm.MapEntityToSQLParameters<DISetting>(cmdInsert.Parameters, setting);
                ExecuteNonQuery(cmdInsert);
                cmdInsert.Dispose();
            }
            cmd.Dispose();
        }
    }
}
