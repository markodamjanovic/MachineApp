using System.Data;

namespace MachineApp.Data
{
    public interface IDatabaseConnection
    {
        IDbConnection GetDbConnection();
    }
}