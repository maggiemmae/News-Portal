using dal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dal.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllAsync();
    }
}
