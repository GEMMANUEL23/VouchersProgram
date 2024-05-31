using System.ComponentModel.DataAnnotations;

namespace VouchersProgram.Data.Models
{
    public class VWS_Line
    {
        public int VSLineID { get; set; }
        public int VSLineHeaderID { get; set; }
        public int VSLineInvoiceID { get; set; }
        public int VSLineInvRNumber { get; set; }
        public string VSLineInvNumber { get; set; }
        public string VSLineInvoiceBuyer { get; set; }
        public string VSLineInvoiceFile { get; set; }

        [Required(ErrorMessage = "Description is compulsory.")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "Description must be between 4 and 25 characters.")]
        public string VSLineDescription { get; set; }
        public int VSLineInvoiceQuantity { get; set; }
        public decimal VSLineInvoiceUnitPrice { get; set; }
        public decimal VSLineInvoiceTotal { get; set; }
        public int VSLineDiscountTypeID { get; set; }
    }
}
