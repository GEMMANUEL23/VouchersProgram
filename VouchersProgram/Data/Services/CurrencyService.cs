using Dapper;
using System.Data;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class CurrencyService : ICurrencyService
    {
        ISqlConnectionConfiguration _configuration;
        public CurrencyService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Currency>> CurrencyList()
        {
            IEnumerable<Currency> currency;
            using var conn = _configuration.GetConnection();
            currency = await conn.QueryAsync<Currency>("spCurrency_List", commandType: System.Data.CommandType.StoredProcedure);
            return currency;
        }

        public async Task<bool> Currency_Insert(Currency currency)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CurrencySymbol", currency.CurrencySymbol, DbType.String);
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spCurrency_Insert", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<Currency> Currency_GetOne(int @CurrencyID)
        {
            Currency currency = new Currency();
            var parameters = new DynamicParameters();
            parameters.Add("@CurrencyID", CurrencyID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            currency = await conn.QueryFirstOrDefaultAsync<Currency>("spCurrency_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return currency;

        }


        public async Task<bool> Currency_Update(Currency currency)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CurrencyID", currency.CurrencyID, DbType.Int32);
            parameters.Add("CurrencySymbol", currency.CurrencySymbol, DbType.String);
            parameters.Add("CurrencyIsArchived", currency.CurrencyIsArchived, DbType.Boolean);
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spCurrency_Update", parameters, commandType: CommandType.StoredProcedure); 
            return true;

        }
    }
}
