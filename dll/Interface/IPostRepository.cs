using dal.Models;
using System.Threading.Tasks;

namespace dal.Interface
{
    public interface IPostRepository
    {
        Task<PostsPaged> GetPostsAsync(int page);

        Task<Post> GetByIdAsync(int id);

        Task UpdateAsync(Post item);

        Task<Post> CreateAsync(Post item);

        Task DeleteAsync(int id);
    }
}
