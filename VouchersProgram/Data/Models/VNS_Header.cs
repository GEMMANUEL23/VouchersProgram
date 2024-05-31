namespace VouchersProgram.Data.Models
{
    public class VNS_Header
    {
        public int VHeaderID { get; set; }
        public int VHeaderVoucherNumber { get; set; }
        public DateTime VHeaderVEDate { get; set; }
        public string VHeaderDeparment { get; set; }
        public string VHeaderDepTeam { get; set; }
        public string VHeaderPayToName { get; set; }
        public int VHeaderPayMethodID { get; set; }
        public string VHeaderPayToBankName { get; set; }
        public string VHeaderPayToBankAcc { get; set; }
        public int VHeaderPayToCID { get; set; }
        public string VHeaderRequestedBy { get; set; }
        public bool VHeaderIsArchived { get; set; }
        public Guid VHeaderGuid { get; set; }
    }
}
