using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IVWS_HeaderService
    {
        Task<IEnumerable<VWS_Header>> VWSHeader_List();
        Task<int> VWSHeader_Insert(DateTime VSHeaderVEDate, int VSHeaderDeparmentID, int VSHeaderTeamID, int VSHeaderSupplierID,
           string SupplierPayToName, int VSHeaderSupplierAccCode, int VSHeaderSuppPayMethodID, int VSHeaderSupplierBankID, string VSHeaderSupplierBankAcc,
           int VSHeaderSupplierCID, string VSHeaderSupplierCSymbol, string VSHeaderSupplierContact, string VSHeaderRequestedBy);
        Task<VWS_Header> VWSHeader_GetOne(int @VSHeaderID);
        Task<bool> VWSHeader_Update(VWS_Header vwsheader);

        
    }
}
