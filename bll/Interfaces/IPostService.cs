using bll.DTO.Post;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface IPostService
    {
        Task<GetPostsPaged> GetPostListAsync(int page);

        Task<GetPostByIdDto> GetPostByIdAsync(int id);

        Task<bool> UpdatePostAsync(UpdatePostDto post);

        Task<bool> AddPostAsync(AddPostViewModel post, string userName);

        Task DeletePostAsync(int id);
    }
}
