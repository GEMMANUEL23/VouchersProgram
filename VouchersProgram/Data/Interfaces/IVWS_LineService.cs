using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IVWS_LineService
    {
        Task<IEnumerable<VWS_Line>> VWSLine_List();
        Task<bool> VWSLine_Insert(VWS_Line vwsline);
        Task<VWS_Line> VWSLine_GetOne(int @VSLineID);
        Task<bool> VWSLine_Update(VWS_Line vwsline);
        Task<IEnumerable<VWS_Line>> VWSLine_GetbyVSHeader(int @VSHeaderID);
        Task<bool> VSLineDeleteOne(int @VSLineID);
    }
}
