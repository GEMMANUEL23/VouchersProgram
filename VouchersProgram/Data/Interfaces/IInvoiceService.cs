using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> InvoiceList();
        Task<int> Invoice_Insert(int InvoiceRecordNumber, int InvoiceSupplierID,
        int InvoiceSupplierAccCode, DateTime InvoiceEmisionDate, string InvoiceNumber, string IvoiceFile, int InvoicePQty,
        decimal InvoicePUnitPrice, decimal InvoiceTotal, int InvoiceSupplierCurrencyID, string InvoiceBuyer,
        int InvoiceExpenseAccountID);
        Task<Invoice> Invoice_GetOne(int @InvoiceID);
        Task<int> Invoice_Update(int InvoiceID, int InvoiceRecordNumber, int InvoiceSupplierID,
        int InvoiceSupplierAccCodeID, DateTime InvoiceEmisionDate, string IvoiceFile, int InvoicePQty,
        decimal InvoicePUnitPrice, decimal InvoiceTotal, int InvoiceSupplierCurrencyID, string InvoiceBuyer,
        int InvoiceExpenseAccountID, bool InvoiceIsArchived);

        Task<IEnumerable<Invoice>> InvoicesBySupplier(int @SupplierID);
         //Task inovice(Invoide invo)
    }
}
