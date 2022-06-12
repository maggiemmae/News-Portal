using bll.DTO.User;
using bll.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Program.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Gets the list of all Users.
        /// </summary>
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUserListAsync()
        {
            return Ok(await userService.GetUserListAsync());
        }

        /// <summary>
        /// Deletes a User by Id.
        /// </summary>
        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await userService.DeleteUserAsync(id);
            return Ok();
        }

        /// <summary>
        /// Updates a User.
        /// </summary>
        // PUT: api/User 
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto user)
        {
            await userService.UpdateUserAsync(user);
            return Ok();
        }

        /// <summary>
        /// Gets a User by Id.
        /// </summary>
        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
        {
            return Ok(await userService.GetUserByIdAsync(id));
        }

        /// <summary>
        /// Blocks a User.
        /// </summary>
        // PUT: api/User 
        [HttpPut("{id}")]
        public async Task<IActionResult> BlockUserAsync(int id, [FromBody] int hours)
        {
            await userService.BlockUserAsync(id, hours);
            return Ok();
        }
    }
}
