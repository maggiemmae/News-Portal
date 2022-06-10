using bll.DTO.User;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface IAccountService
    {
        Task<string> Login(LoginModel userLogin);

        Task<UserDto> Register(RegisterModel userRegister);

        Task<UserDto> RegisterAdmin(RegisterModel userRegister);

        string GetJwtSecurityToken(List<Claim> authClaims);
    }
}
