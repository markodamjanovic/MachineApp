using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineApp.Models
{
    public interface IDatabaseRepository
    {
        Task<IEnumerable<Machine>> GetMachines();
        Task<IEnumerable<Malfunctions>> GetMalfunctions();
    }
}