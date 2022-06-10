using bll.DTO.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUserListAsync();

        Task<UserDto> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(UpdateUserDto user);

        Task DeleteUserAsync(int id);

        Task<bool> BlockUserAsync(int id, int hours);

    }
}

