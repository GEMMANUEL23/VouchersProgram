using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Data.Models;

namespace VouchersProgram.Data.Services
{
    public class TeamsService : ITeamsService
    {
        ISqlConnectionConfiguration _configuration;
        public TeamsService(ISqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Teams>> TeamsList()
        {
            IEnumerable<Teams> teams;
            using var conn = _configuration.GetConnection();
            teams = await conn.QueryAsync<Teams>("spTeams_List", commandType: System.Data.CommandType.StoredProcedure);
            return teams;
        }

        public async Task<bool> Team_Insert(Teams team)
        {
            var parameters = new DynamicParameters();
            parameters.Add("DepTeamID", team.DepTeamID, DbType.Int32);
            parameters.Add("TeamName", team.TeamName, DbType.String);
            parameters.Add("TeamIsArchived", team.TeamIsArchived, DbType.Boolean);
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spTeams_Insert", parameters, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<Teams> Team_GetOne(int TeamID)
        {
            Teams team = new Teams();
            var parameters = new DynamicParameters();
            parameters.Add("@TeamID", TeamID, DbType.Int32);
            using var conn = _configuration.GetConnection();
            team = await conn.QueryFirstOrDefaultAsync<Teams>("spTeams_GetOnee", parameters, commandType: CommandType.StoredProcedure);
            return team;

        }

        public async Task<bool> Team_Update(Teams team)
        {
            var parameters = new DynamicParameters();
            parameters.Add("TeamID", team.TeamID, DbType.Int32);
            parameters.Add("DepTeamID", team.DepTeamID, DbType.Int32);
            parameters.Add("TeamName", team.TeamName, DbType.String);
            parameters.Add("TeamIsArchived", team.TeamIsArchived, DbType.Boolean);
            using var conn = _configuration.GetConnection();
            await conn.ExecuteAsync("spTeam_Update", parameters, commandType: CommandType.StoredProcedure);
            return true;

        }

    }

}
