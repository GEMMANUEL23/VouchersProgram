using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IBanksService
    {
        Task<IEnumerable<Banks>> BankList();
        Task<int> Bank_Insert(string BankDescription);
        Task<Banks> Bank_GetOne(int @BankID);
        Task<int> Bank_Update(int BankID, string BankDescription, bool BankIsArchived);
    }
}
