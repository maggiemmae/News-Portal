using System.Threading.Tasks;

namespace dal.Interface
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T item);

        Task<T> CreateAsync(T item);

        Task DeleteAsync(int id);
    }
}
