using bll.DTO.User;
using dal.Models;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<UserDto>> GetUserListAsync(UserParameters userParameters);

        Task<UserDto> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(UpdateUserDto user);

        Task DeleteUserAsync(int id);

        Task<bool> BlockUserAsync(int id, int hours);

    }
}

