using Dapper;
using System.Data;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class InvoiceService : IInvoiceService
    {
        ISqlConnectionConfiguration _configuration;
        public InvoiceService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Invoice>> InvoiceList()
        {
            IEnumerable<Invoice> invoices;
            using var conn = _configuration.GetConnection();
            invoices = await conn.QueryAsync<Invoice>("spInvoice_List", commandType: CommandType.StoredProcedure);
            return invoices;

        }

        public async Task<int> Invoice_Insert(int InvoiceRecordNumber, int InvoiceSupplierID,
        int InvoiceSupplierAccCode, DateTime InvoiceEmisionDate, string InvoiceNumber, string IvoiceFile, int InvoicePQty,
        decimal InvoicePUnitPrice, decimal InvoiceTotal, int InvoiceSupplierCurrencyID, string InvoiceBuyer,
        int InvoiceExpenseAccountID)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("InvoiceRecordNumber", InvoiceRecordNumber, DbType.Int32);
            parameters.Add("InvoiceSupplierID", InvoiceSupplierID, DbType.Int32);
            parameters.Add("InvoiceSupplierAccCode", InvoiceSupplierAccCode, DbType.Int32);
            parameters.Add("InvoiceEmisionDate", InvoiceEmisionDate, DbType.DateTime);
            parameters.Add("InvoiceNumber", InvoiceNumber, DbType.String);
            parameters.Add("IvoiceFile", IvoiceFile, DbType.String);
            parameters.Add("InvoicePQty", InvoicePQty, DbType.Int32);
            parameters.Add("InvoicePUnitPrice", InvoicePUnitPrice, DbType.Decimal);
            parameters.Add("InvoiceTotal", InvoiceTotal, DbType.Decimal);
            parameters.Add("InvoiceSupplierCurrencyID", InvoiceSupplierCurrencyID, DbType.Int32);
            parameters.Add("InvoiceBuyer", InvoiceBuyer, DbType.String);
            parameters.Add("InvoiceExpenseAccountID", InvoiceExpenseAccountID, DbType.Int32);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spInvoice_Insert", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
        }

        public async Task<Invoice> Invoice_GetOne(int @InvoiceID)
        {
            Invoice invoice = new Invoice();
            var parameters = new DynamicParameters();
            parameters.Add("InvoiceID", InvoiceID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            invoice = await conn.QueryFirstOrDefaultAsync<Invoice>("spInvoice_GetOne", parameters, commandType: CommandType.StoredProcedure);

            return invoice;
        }

        public async Task<int> Invoice_Update(int InvoiceID, int InvoiceRecordNumber, int InvoiceSupplierID,
        int InvoiceSupplierAccCodeID, DateTime InvoiceEmisionDate, string IvoiceFile, int InvoicePQty,
        decimal InvoicePUnitPrice, decimal InvoiceTotal, int InvoiceSupplierCurrencyID, string InvoiceBuyer,
        int InvoiceExpenseAccountID, bool InvoiceIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("InvoiceID", InvoiceID, DbType.Int32);
            parameters.Add("InvoiceRecordNumber", InvoiceRecordNumber, DbType.Int32);
            parameters.Add("InvoiceSupplierID", InvoiceSupplierID, DbType.Int32);
            parameters.Add("InvoiceSupplierAccCodeID", InvoiceSupplierAccCodeID, DbType.Int32);
            parameters.Add("InvoiceEmisionDate", InvoiceEmisionDate, DbType.DateTime);
            parameters.Add("IvoiceFile", IvoiceFile, DbType.String);
            parameters.Add("InvoicePQty", InvoicePQty, DbType.Int32);
            parameters.Add("InvoicePUnitPrice", InvoicePUnitPrice, DbType.Decimal);
            parameters.Add("InvoiceTotal", InvoiceTotal, DbType.Decimal);
            parameters.Add("InvoiceSupplierCurrencyID", InvoiceSupplierCurrencyID, DbType.Int32);
            parameters.Add("InvoiceBuyer", InvoiceBuyer, DbType.String);
            parameters.Add("InvoiceExpenseAccountID", InvoiceExpenseAccountID, DbType.Int32);
            parameters.Add("InvoiceIsArchived", InvoiceIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spInvoice_Update", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;
        }

        public async Task<IEnumerable<Invoice>> InvoicesBySupplier(int @SupplierID)
        {
            IEnumerable<Invoice> invoices;
            var parameters = new DynamicParameters();
            parameters.Add("@SupplierID", SupplierID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            invoices = await conn.QueryAsync<Invoice>("spInvoice_ListBySupplier", parameters, commandType: CommandType.StoredProcedure);
            
            return invoices;

        }

    }
}
