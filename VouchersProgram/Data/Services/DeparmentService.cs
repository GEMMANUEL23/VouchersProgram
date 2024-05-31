using Dapper;
using Syncfusion.Blazor.Charts.Internal;
using System.Data;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class DeparmentService : IDeparmentService
    {
        ISqlConnectionConfiguration _configuration;
        public DeparmentService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Deparment>> DeparmentList()
        {
            IEnumerable<Deparment> deparments;
            using var conn = _configuration.GetConnection();
            deparments = await conn.QueryAsync<Deparment>("spDeparmet_List", commandType: System.Data.CommandType.StoredProcedure);
            return deparments;
        }

        public async Task<bool> Deparment_Insert(Deparment deparment)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DeparmentName", deparment.DeparmentName, DbType.String);
            parameters.Add("DepIsArchived", deparment.DepIsArchived, DbType.Boolean);
             using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spDeparmet_Insert", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<Deparment> Deparment_GetOne(int @DeparmentID)
        { 
            Deparment deparment = new Deparment();
            var parameters = new DynamicParameters();
            parameters.Add("@DeparmentID", DeparmentID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            deparment = await conn.QueryFirstOrDefaultAsync<Deparment>("spDeparmet_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return deparment;
        
        }

        public async Task<bool> Deparment_Update(Deparment deparment)
        { 
            var parameters = new DynamicParameters();
            parameters.Add("DeparmentID", deparment.DeparmentID, DbType.Int32);
            parameters.Add("DeparmentName", deparment.DeparmentName, DbType.String);
            parameters.Add("DepIsArchived", deparment.DepIsArchived, DbType.Boolean);
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spDeparmet_Update", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }
    }
}
