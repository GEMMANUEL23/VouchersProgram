using Dapper;
using Microsoft.Identity.Client;
using System.Data;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class AuthorizersService : IAuthorizersService
    {
        ISqlConnectionConfiguration _configuration;
        public AuthorizersService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Authorizers>> Authorizers_List()
        {
            IEnumerable<Authorizers> authorizers;
            using var conn = _configuration.GetConnection();
            authorizers = await conn.QueryAsync<Authorizers>("spAuthorizers_List", commandType: CommandType.StoredProcedure);
            return authorizers;
        }

        public async Task<int> Authorizer_Insert(string AuthorizerName)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("AuthorizerName", AuthorizerName, DbType.String);
            parameters.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.QueryAsync("spAuthorizers_Insert", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");
            return Success;

        }

        public async Task<Authorizers> Authorizers_GetOne(int @AuthorizerID)
        {
            Authorizers authorizers = new Authorizers();
            var parameters = new DynamicParameters();
            parameters.Add("AuthorizerID", AuthorizerID, DbType.Int32);

            using var conn = _configuration.GetConnection();
            authorizers = await conn.QueryFirstOrDefaultAsync<Authorizers>("spAuthorizers_GetOne", parameters, commandType: CommandType.StoredProcedure);
            return authorizers;

        }

        public async Task<int> Authorizer_Update(int AuthorizerID, string AuthorizerName, bool AuthorizerIsArchived)
        {
            int Success = 0;
            var parameters = new DynamicParameters();
            parameters.Add("AuthorizerID", AuthorizerID, DbType.Int32);
            parameters.Add("AuthorizerName", AuthorizerName, DbType.String);
            parameters.Add("AuthorizerIsArchived", AuthorizerIsArchived, DbType.Boolean);
            parameters.Add("@ReturnValue", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spAuthorizers_Update", parameters, commandType: CommandType.StoredProcedure);
            Success = parameters.Get<int>("@ReturnValue");

            return Success;
        }

    }
}
