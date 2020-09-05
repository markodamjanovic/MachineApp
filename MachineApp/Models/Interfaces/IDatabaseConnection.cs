using System.Data;

namespace MachineApp.Models
{
    public interface IDatabaseConnection
    {
        IDbConnection GetDbConnection();
    }
}