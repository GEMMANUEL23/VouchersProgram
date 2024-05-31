using Dapper;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;
using System.Data;
using System.Runtime.CompilerServices;
using Humanizer.Localisation.TimeToClockNotation;

namespace VouchersProgram.Data.Services
{
    public class PayMethodService : IPayMethodService
    {
        ISqlConnectionConfiguration _configuration;
        public PayMethodService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<PayMethod>> PayMethList()
        {
            IEnumerable<PayMethod> payMethods;
            using var conn = _configuration.GetConnection();
            payMethods = await conn.QueryAsync<PayMethod>("spPayMethod_List", commandType: CommandType.StoredProcedure);
            return payMethods;
        }

        public async Task<int> PayMethod_Insert(string PayMethDescription)
        { 
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("PayMethDescription", PayMethDescription, DbType.String);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spPayMethod_Insert", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return  Success;
        }


        public async Task<PayMethod> PayMethod_GetOne(int @PayMethodID)
        {  
            PayMethod payMethod = new PayMethod();
            var parameters = new DynamicParameters();
            parameters.Add("PayMethodID", PayMethodID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            payMethod = await conn.QueryFirstOrDefaultAsync<PayMethod>("spPayMethod_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return payMethod;
        }

        public async Task<int> PayMethod_Update(int PayMethodID, string PayMethDescription, bool PayMethIsArchived)
        { 
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("PayMethodID", PayMethodID, DbType.Int32);
            parameters.Add("PayMethDescription", PayMethDescription, DbType.String);
            parameters.Add("PayMethIsArchived", PayMethIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spPayMethod_Update", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
        }
    }
}
