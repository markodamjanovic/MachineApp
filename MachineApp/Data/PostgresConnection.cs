using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MachineApp.Data
{
    public class PostgresConnection : IDatabaseConnection
    {   
        private readonly IConfiguration _configuration;
        
        public PostgresConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetDbConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}