using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace MachineApp.Models
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public DatabaseRepository(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        public async Task<IEnumerable<Machine>> GetMachines()
        {   
            using (var connection = _databaseConnection.GetDbConnection())
            {
                return await connection.GetAllAsync<Machine>();
            }
        }

        public async Task<IEnumerable<Malfunctions>> GetMalfunctions()
        {
            using(var connection = _databaseConnection.GetDbConnection())
            {
                return await connection.GetAllAsync<Malfunctions>();
            }
        }
    }
}