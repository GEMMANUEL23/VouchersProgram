namespace VouchersProgram.Data.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public int SupplierAccCode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPayToName { get; set; }
        public string SupplierContact { get; set; }
        public int SupplierCurrencyID { get; set; }
        public int SupplierExpenseAccID { get; set; }
        public int SupplierBankID { get; set; }
        public int SupplierPayMethID { get; set; }
        public string SupplierBankAccount { get; set; }
        public bool SupplierIsArchived { get; set; }

        public string SupplierCurrencySymbol { get; set; }
        public string SupplierBankName {  get; set; }
        
    }
}
