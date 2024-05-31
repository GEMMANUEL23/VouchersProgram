using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IPayMethodService
    {
        Task<IEnumerable<PayMethod>> PayMethList();
        Task<int> PayMethod_Insert(string PayMethDescription);
        Task<PayMethod> PayMethod_GetOne(int @PayMethodID);
        Task<int> PayMethod_Update(int PayMethodID, string PayMethDescription, bool PayMethIsArchived);

    }
}
