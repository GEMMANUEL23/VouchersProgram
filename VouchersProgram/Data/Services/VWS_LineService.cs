using Dapper;
using Syncfusion.PdfExport;
using System.Data;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class VWS_LineService : IVWS_LineService
    {
        ISqlConnectionConfiguration _configuration;
        public VWS_LineService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<VWS_Line>> VWSLine_List()
        {
            IEnumerable<VWS_Line> vwslines;
            using var conn = _configuration.GetConnection();
            vwslines = await conn.QueryAsync<VWS_Line>("spVWSLine_List", commandType: CommandType.StoredProcedure);
            return vwslines;
        }

        public async Task<bool> VWSLine_Insert(VWS_Line vwsline)
        {

            var parameters = new DynamicParameters();
            parameters.Add("VSLineHeaderID", vwsline.VSLineHeaderID, DbType.Int32);
            parameters.Add("VSLineInvoiceID", vwsline.VSLineInvoiceID, DbType.Int32);
            parameters.Add("VSLineInvRNumber", vwsline.VSLineInvRNumber, DbType.Int32);
            parameters.Add("VSLineInvNumber", vwsline.VSLineInvNumber, DbType.String);
            parameters.Add("VSLineInvoiceBuyer", vwsline.VSLineInvoiceBuyer, DbType.String);
            parameters.Add("VSLineInvoiceFile", vwsline.VSLineInvoiceFile, DbType.String);
            parameters.Add("VSLineDescription", vwsline.VSLineDescription, DbType.String);
            parameters.Add("VSLineInvoiceQuantity", vwsline.VSLineInvoiceQuantity, DbType.Int32);
            parameters.Add("VSLineInvoiceUnitPrice", vwsline.VSLineInvoiceUnitPrice, DbType.Decimal);
            parameters.Add("VSLineInvoiceTotal", vwsline.VSLineInvoiceTotal, DbType.Decimal);
            parameters.Add("VSLineDiscountTypeID", vwsline.VSLineDiscountTypeID, DbType.Int32);

            var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spVWSLine_Insert", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }
      
        public async Task<VWS_Line> VWSLine_GetOne(int @VSLineID)
        {
            VWS_Line vwsline = new VWS_Line();
            var parameters = new DynamicParameters();
            parameters.Add("VSLineID", VSLineID, DbType.Int32);


            using var conn = _configuration.GetConnection();
            vwsline = await conn.QueryFirstOrDefaultAsync<VWS_Line>("spVWSLine_GetOne", parameters, commandType: CommandType.StoredProcedure);

            return vwsline;

        }

        public async Task<bool> VWSLine_Update(VWS_Line vwsline)
        {
            var parameters = new DynamicParameters();
            parameters.Add("VSLineID", vwsline.VSLineID, DbType.Int32);
            parameters.Add("VSLineHeaderID", vwsline.VSLineHeaderID, DbType.Int32);
            parameters.Add("VSLineInvoiceID", vwsline.VSLineInvoiceID, DbType.Int32);
            parameters.Add("VSLineInvRNumber", vwsline.VSLineInvRNumber, DbType.Int32);
            parameters.Add("VSLineInvNumber", vwsline.VSLineInvNumber, DbType.String);
            parameters.Add("VSLineInvoiceBuyer", vwsline.VSLineInvoiceBuyer, DbType.String);
            parameters.Add("VSLineInvoiceFile", vwsline.VSLineInvoiceFile, DbType.String);
            parameters.Add("VSLineDescription", vwsline.VSLineDescription, DbType.String);
            parameters.Add("VSLineInvoiceQuantity", vwsline.VSLineInvoiceQuantity, DbType.Int32);
            parameters.Add("VSLineInvoiceUnitPrice", vwsline.VSLineInvoiceUnitPrice, DbType.Decimal);
            parameters.Add("VSLineInvoiceTotal", vwsline.VSLineInvoiceTotal, DbType.Decimal);
            parameters.Add("VSLineDiscountTypeID", vwsline.VSLineDiscountTypeID, DbType.Int32);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spVWSLine_Update", parameters, commandType: CommandType.StoredProcedure);
            return true;

        }

        public async Task<IEnumerable<VWS_Line>> VWSLine_GetbyVSHeader(int @VSHeaderID)
        {
            IEnumerable<VWS_Line> vslines;
            var parameters = new DynamicParameters();
            parameters.Add("@VSHeaderID", VSHeaderID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            vslines = await conn.QueryAsync<VWS_Line>("spVWSLine_GetbyVSHeader", parameters, commandType: CommandType.StoredProcedure);
            return vslines;
        }

        public async Task<bool> VSLineDeleteOne(int @VSLineID)
        { 
          var parameters = new DynamicParameters();
            parameters.Add("@VSLineID", VSLineID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spVSLine_DeleteOne", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }

    }
}
