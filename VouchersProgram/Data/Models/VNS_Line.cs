namespace VouchersProgram.Data.Models
{
    public class VNS_Line
    {
        public int VLineID { get; set; }
        public int VLineHeaderID { get; set; }
        public DateTime VLineInvoiceEDate { get; set; }
        public string VLineInvoiceNumber { get; set; }
        public int VLineExpenseAccID { get; set; }
        public string VLineDescription { get; set; }
    }
}
