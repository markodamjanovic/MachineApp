using System.Collections.Generic;
using System.Threading.Tasks;

namespace MachineApp.Models
{
    public interface IDatabaseRepository
    {
        Task<IEnumerable<T>> GetAll<T>() where T: class;
        Task<T> Get<T>(int id) where T: class;
        Task<int> Insert<T>(T model) where T: class;
        Task<T> Update<T>(T model) where T: class;
        Task<T> Delete<T>(T model) where T: class;
        Task<IEnumerable<T>> DeleteList<T>(IEnumerable<T> models) where T: class;
    }
}