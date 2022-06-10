using dal.Models;
using System.Threading.Tasks;

namespace dal.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<GetPostsPaged> GetPostsAsync(int page);
    }
}
