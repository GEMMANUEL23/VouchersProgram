using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface ITeamsService
    {
        Task<IEnumerable<Teams>> TeamsList();
        Task<bool> Team_Insert(Teams team);
        Task<Teams> Team_GetOne(int TeamID);
        Task<bool> Team_Update(Teams team);

    }
}
