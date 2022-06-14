using bll.DTO.Post;
using dal.Models;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface IPostService
    {
        Task<PagedList<GetPostsDto>> GetPostListAsync(PostParameters postParameters);

        Task<GetPostByIdDto> GetPostByIdAsync(int id);

        Task<bool> UpdatePostAsync(UpdatePostDto post);

        Task<bool> AddPostAsync(AddPostViewModel post, string userName);

        Task DeletePostAsync(int id);
    }
}
