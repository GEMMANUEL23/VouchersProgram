using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Interfaces
{
    public interface IAuthorizersService
    {
        Task<IEnumerable<Authorizers>> Authorizers_List();
        Task<int> Authorizer_Insert(string AuthorizerName);
        Task<Authorizers> Authorizers_GetOne(int @AuthorizerID);
        Task<int> Authorizer_Update(int AuthorizerID, string AuthorizerName, bool AuthorizerIsArchived);

    }
}
