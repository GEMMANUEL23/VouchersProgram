using Microsoft.Data.SqlClient;
using System.Data;

namespace VouchersProgram.Data.Interfaces
{
    public interface ISqlConnectionConfiguration
    {
        IDbConnection GetConnection();
    }

    public class SqlConnectionConfiguration : ISqlConnectionConfiguration
    {
        private readonly IConfiguration _configuration;
        public SqlConnectionConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SqlDBContext"));
        }

    }
}
