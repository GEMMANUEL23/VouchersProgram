using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> CurrencyList();
        Task<bool> Currency_Insert(Currency currency);
        Task<Currency> Currency_GetOne(int @CurrencyID);
        Task<bool> Currency_Update(Currency currency);

    }
}
