using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> SupplierList();

        Task<int> Supplier_Insert(int SupplierAccCode, string SupplierName, string SupplierPayToName,
        string SupplierBankAccount, int SupplierCurrencyID, string SupplierContact, int SupplierExpenseAccID,
        int SupplierBankID, int SupplierPayMethID);

        Task<Supplier> Supplier_GetOne(int @SupplierID);

        Task<int> Supplier_Update(int SupplierID, int SupplierAccCode, string SupplierName, string SupplierPayToName,
        string SupplierBankAccount, int SupplierCurrencyID, string SupplierContact, int SupplierExpenseAccID, int SupplierBankID, int SupplierPayMethID,
        bool SupplierIsArchived);
    }
}
