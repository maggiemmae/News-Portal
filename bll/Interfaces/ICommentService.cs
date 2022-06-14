using bll.DTO.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<GetCommentDto>> GetCommentListAsync();

        Task<GetCommentDto> GetCommentByIdAsync(int id);

        Task<bool> AddCommentAsync(AddCommentDto comment, string userName, int postId);

        Task DeleteCommentAsync(int id);

    }
}
