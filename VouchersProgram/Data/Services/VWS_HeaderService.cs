using Dapper;
using Humanizer;
using System.Data;
using System.Security.Cryptography.Xml;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class VWS_HeaderService : IVWS_HeaderService
    {
        ISqlConnectionConfiguration _configuration;
        public VWS_HeaderService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<VWS_Header>> VWSHeader_List()
        {
            IEnumerable<VWS_Header> vwsheader;
            using var conn = _configuration.GetConnection();

            vwsheader = await conn.QueryAsync<VWS_Header>("spVWSHeader_List", commandType: CommandType.StoredProcedure);
            return vwsheader;

        }

           public async Task<int> VWSHeader_Insert(DateTime VSHeaderVEDate, int VSHeaderDeparmentID, int VSHeaderTeamID, int VSHeaderSupplierID,
           string SupplierPayToName, int VSHeaderSupplierAccCode, int VSHeaderSuppPayMethodID, int VSHeaderSupplierBankID, string VSHeaderSupplierBankAcc,
           int VSHeaderSupplierCID,string VSHeaderSupplierCSymbol, string VSHeaderSupplierContact, string VSHeaderRequestedBy)

          {
            int newvoucherHeaderID = 0;
            var parameters = new DynamicParameters();
            parameters.Add("VSHeaderVEDate", VSHeaderVEDate, DbType.DateTime);
            parameters.Add("VSHeaderDeparmentID", VSHeaderDeparmentID, DbType.Int32);
            parameters.Add("VSHeaderTeamID", VSHeaderTeamID, DbType.Int32);
            parameters.Add("VSHeaderSupplierID", VSHeaderSupplierID, DbType.Int32);
            parameters.Add("VSHeaderSupplierPayToName", SupplierPayToName, DbType.String);
            parameters.Add("VSHeaderSupplierAccCode", VSHeaderSupplierAccCode, DbType.Int32);
            parameters.Add("VSHeaderSuppPayMethodID", VSHeaderSuppPayMethodID, DbType.Int32);
            parameters.Add("VSHeaderSupplierBankID", VSHeaderSupplierBankID, DbType.Int32);
            parameters.Add("VSHeaderSupplierBankAcc", VSHeaderSupplierBankAcc, DbType.String);
            parameters.Add("VSHeaderSupplierCID", VSHeaderSupplierCID, DbType.Int32);
            parameters.Add("VSHeaderSupplierCSymbol", VSHeaderSupplierCSymbol, DbType.String);
            parameters.Add("VSHeaderSupplierContact", VSHeaderSupplierContact, DbType.String);
            parameters.Add("VSHeaderRequestedBy", VSHeaderRequestedBy, DbType.String);
            parameters.Add("@Output", DbType.Int32, direction: ParameterDirection.Output);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spVWSHeader_Insert", parameters, commandType: CommandType.StoredProcedure);
            newvoucherHeaderID = parameters.Get<int>("@Output");
            return newvoucherHeaderID;
          }

        public async Task<VWS_Header> VWSHeader_GetOne(int @VSHeaderID)
        {
            VWS_Header vwsHeader = new VWS_Header();
            var parameters = new DynamicParameters();
            parameters.Add("VSHeaderID", VSHeaderID, DbType.Int32);

            using var conn = _configuration.GetConnection();
            vwsHeader = await conn.QueryFirstOrDefaultAsync<VWS_Header>("spVWSHeader_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return vwsHeader;

        }

        public async Task<bool> VWSHeader_Update(VWS_Header vwsheader)
        {
            var parameters = new DynamicParameters();
            parameters.Add("VSHeaderID", vwsheader.VSHeaderID, DbType.Int32);
            parameters.Add("VSHeaderVoucherNumber", vwsheader.VSHeaderVoucherNumber, DbType.Int32);
            parameters.Add("VSHeaderVEDate", vwsheader.VSHeaderVEDate, DbType.DateTime);
            parameters.Add("VSHeaderDeparmentID", vwsheader.VSHeaderDeparmentID, DbType.String);
            parameters.Add("VSHeaderTeamID", vwsheader.VSHeaderTeamID, DbType.String);
            parameters.Add("VSHeaderSupplierID", vwsheader.VSHeaderSupplierID, DbType.Int32);
            parameters.Add("VSHeaderSupplierPayToName", vwsheader.SupplierPayToName, DbType.String);
            parameters.Add("VSHeaderSupplierAccCode", vwsheader.VSHeaderSupplierAccCode, DbType.Int32);
            parameters.Add("VSHeaderSuppPayMethodID", vwsheader.VSHeaderSuppPayMethodID, DbType.String);
            parameters.Add("VSHeaderSupplierBankID", vwsheader.VSHeaderSupplierBankID, DbType.String);
            parameters.Add("VSHeaderSupplierBankAcc", vwsheader.VSHeaderSupplierBankAcc, DbType.String);
            parameters.Add("VSHeaderSupplierCID", vwsheader.VSHeaderSupplierCID, DbType.Int32);
            parameters.Add("VSHeaderSupplierCSymbol", vwsheader.VSHeaderSupplierCSymbol, DbType.String);
            parameters.Add("VSHeaderSupplierContact", vwsheader.VSHeaderSupplierContact, DbType.String);
            parameters.Add("VSHeaderRequestedBy", vwsheader.VSHeaderRequestedBy, DbType.String);
            parameters.Add("VSHeaderIsArchived", vwsheader.VSHeaderIsArchived, DbType.Boolean);
            parameters.Add("VSHeaderGuid", vwsheader.VSHeaderGuid, DbType.Guid);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spVWSHeader_Update", parameters, commandType: CommandType.StoredProcedure);
            return true;

        }
    }
}
