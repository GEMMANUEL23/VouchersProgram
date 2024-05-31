using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IDeparmentService
    {
        Task<IEnumerable<Deparment>> DeparmentList();
        Task<bool> Deparment_Insert(Deparment deparment);
        Task<Deparment> Deparment_GetOne(int @DeparmentID);
        Task<bool> Deparment_Update(Deparment deparment);
    }
}
