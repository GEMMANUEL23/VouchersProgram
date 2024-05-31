using Microsoft.Identity.Client;

namespace VouchersProgram.Data.Models
{
    public class VWS_Header
    {
        public int VSHeaderID { get; set; }
        public int VSHeaderVoucherNumber { get; set; }
        public DateTime VSHeaderVEDate { get; set; }
        public int VSHeaderDeparmentID { get; set; }
        public int VSHeaderTeamID { get; set; }
        public int VSHeaderSupplierID { get; set; }
        //public string VSHeaderSupplierPayToName { get; set; }
        public int VSHeaderSupplierAccCode { get; set; }
        public int VSHeaderSuppPayMethodID { get; set; }
        public int VSHeaderSupplierBankID { get; set; }
        public string VSHeaderSupplierBankAcc { get; set; }
        public int VSHeaderSupplierCID { get; set; }
        public string VSHeaderSupplierCSymbol { get; set; }
        public string VSHeaderSupplierContact { get; set; }
        public string VSHeaderRequestedBy { get; set; }
        public bool VSHeaderIsArchived { get; set; }


        public string DeparmentName { get; set; }
        public string TeamName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPayToName { get; set; }
        public string PayMethDescription { get; set; }
        public string CurrencySymbol { get; set; }
        public string VSHeaderSupplierBankName { get; set; }
        public DateTime PayDate { get; set; }
        public decimal VoucherTotal {  get; set; }
        public Guid VSHeaderGuid { get; set; }

    }
}
