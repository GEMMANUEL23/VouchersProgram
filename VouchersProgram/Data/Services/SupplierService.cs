using Dapper;
using System.Data;
using Humanizer.Localisation.TimeToClockNotation;
using VouchersProgram.Data.Models;
using VouchersProgram.Data.Interfaces;

namespace VouchersProgram.Data.Services
{
    public class SupplierService : ISupplierService
    {
        ISqlConnectionConfiguration _configuration;
        public SupplierService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Supplier>> SupplierList()
        {
            IEnumerable<Supplier> suppliers;
            using var conn = _configuration.GetConnection();
            suppliers = await conn.QueryAsync<Supplier>("spSupplier_List", commandType: CommandType.StoredProcedure);
            return suppliers;
        }


        public async Task<int> Supplier_Insert(int SupplierAccCode, string SupplierName, string SupplierPayToName,
        string SupplierBankAccount, int SupplierCurrencyID, string SupplierContact, int SupplierExpenseAccID,
        int SupplierBankID, int SupplierPayMethID)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("SupplierAccCode", SupplierAccCode, DbType.Int32);
            parameters.Add("SupplierName", SupplierName, DbType.String);
            parameters.Add("SupplierPayToName", SupplierPayToName, DbType.String);
            parameters.Add("SupplierBankAccount", SupplierBankAccount, DbType.String);
            parameters.Add("SupplierCurrencyID", SupplierCurrencyID, DbType.Int32);
            parameters.Add("SupplierContact", SupplierContact, DbType.String);
            parameters.Add("SupplierExpenseAccID", SupplierExpenseAccID, DbType.Int32);
            parameters.Add("SupplierBankID", SupplierBankID, DbType.Int32);
            parameters.Add("SupplierPayMethID", SupplierPayMethID, DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spSupplier_Insert", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;

        }


        public async Task<Supplier> Supplier_GetOne(int @SupplierID)
        {
            Supplier supplier = new Supplier();
            var parameters = new DynamicParameters();
            parameters.Add("SupplierID", SupplierID, DbType.Int32);

            using var conn = _configuration.GetConnection();
            supplier = await conn.QueryFirstOrDefaultAsync<Supplier>("spSupplier_GetOne", parameters, commandType: CommandType.StoredProcedure);

            return supplier;
        }


        public async Task<int> Supplier_Update(int SupplierID, int SupplierAccCode, string SupplierName, string SupplierPayToName,
        string SupplierBankAccount, int SupplierCurrencyID, string SupplierContact, int SupplierExpenseAccID, int SupplierBankID, int SupplierPayMethID,
        bool SupplierIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("SupplierID", SupplierID, DbType.Int32);
            parameters.Add("SupplierAccCode", SupplierAccCode, DbType.Int32);
            parameters.Add("SupplierName", SupplierName, DbType.String);
            parameters.Add("SupplierPayToName", SupplierPayToName, DbType.String);
            parameters.Add("SupplierBankAccount", SupplierBankAccount, DbType.String);
            parameters.Add("SupplierCurrencyID", SupplierCurrencyID, DbType.Int32);
            parameters.Add("SupplierContact", SupplierContact, DbType.String);
            parameters.Add("SupplierExpenseAccID", SupplierExpenseAccID, DbType.Int32);
            parameters.Add("SupplierBankID", SupplierBankID, DbType.Int32);
            parameters.Add("SupplierPayMethID", SupplierPayMethID, DbType.Int32);
            parameters.Add("SupplierIsArchived", SupplierIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spSupplier_Update", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");

            return Success;
        }
    }
}
