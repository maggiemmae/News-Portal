using dal.Models;
using System.Threading.Tasks;

namespace dal.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedList<User>> GetAllAsync(UserParameters userParameters);
    }
}
