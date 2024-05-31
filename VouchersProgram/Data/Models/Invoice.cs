namespace VouchersProgram.Data.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int InvoiceRecordNumber { get; set; }
        public int InvoiceSupplierID { get; set; }
        public int InvoiceSupplierAccCode { get; set; }
        public DateTime InvoiceEmisionDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string IvoiceFile { get; set; }
        public int InvoicePQty { get; set; }
        public decimal InvoicePUnitPrice { get; set; }
        public decimal InvoiceTotal { get; set; }
        public int InvoiceSupplierCurrencyID { get; set; }
        public string InvoiceBuyer { get; set; }
        public bool InvoiceIsArchived { get; set; }
        public int InvoiceExpenseAccountID { get; set; }

        public string SupplierName { get; }
        public string CurrencySymbol { get; set; }
        public int SupplierAccCode { get; set; }
        public string ExpenseAccountDescription { get; set; }

    }
}
