using Microsoft.EntityFrameworkCore;

namespace ProductList.Data.Interfaces
{

    public interface IRepository<Tmodel>:ICustomConnection<Tmodel>
    where Tmodel : class
    {
        Task<IEnumerable<Tmodel>> GetAllAsync(string search, int page, int size);
        Task<int> GetCountAsync(string search);
        Task<Tmodel> GetByIdAsync(object id);
        Task AddAndSaveAsync(Tmodel model);
        Task UpdateAndSaveAsync(Tmodel model);
        Task DeleteAndSaveAsync(int id);
    }

}