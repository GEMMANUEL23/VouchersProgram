namespace VouchersProgram.Data.Models
{
    public class DiscountType
    {
        public int DiscTypeID { get; set; }
        public string DiscDescription { get; set; }
        public decimal DiscRate { get; set; }
        public decimal DiscIsArchived { get; set; }
    }
}
