using Dapper;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;
using System.Data;
using Microsoft.Identity.Client;

namespace VouchersProgram.Data.Services
{
    public class BanksService : IBanksService
    {
        ISqlConnectionConfiguration _configuration;
            public BanksService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Banks>> BankList()
        {
            IEnumerable<Banks> banks;
            using var conn = _configuration.GetConnection();
            banks = await conn.QueryAsync<Banks>("spBank_List", commandType: CommandType.StoredProcedure);
            return banks;
        }

        public async Task<int> Bank_Insert(string BankDescription)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("BankDescription", BankDescription, DbType.String);
            parameters.Add("@ReturnValue", dbType: DbType.String, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spBank_Insert", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
        }

        public async Task<Banks> Bank_GetOne(int @BankID)
        {
            Banks banks = new Banks();
            var parameters = new DynamicParameters();
            parameters.Add("BankID", BankID, DbType.Int32);
            
            using var conn = _configuration.GetConnection();
            banks = await conn.QueryFirstOrDefaultAsync<Banks>("spBank_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return banks;
        }

        public async Task<int> Bank_Update(int BankID, string BankDescription, bool BankIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("BankID", BankID, DbType.Int32);
            parameters.Add("BankDescription", BankDescription, DbType.String);
            parameters.Add("BankIsArchived", BankIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spBank_Update", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
        
        }
    }
}
