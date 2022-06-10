using bll.DTO.User;
using bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Program.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
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
            try
            {
                return Ok(await userService.GetUserListAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a User by Id.
        /// </summary>
        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                await userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates a User.
        /// </summary>
        // PUT: api/User 
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto user)
        {
            try
            {
                await userService.UpdateUserAsync(user);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }

        /// <summary>
        /// Gets a User by Id.
        /// </summary>
        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute]int id)
        {
            try
            {
                return Ok(await userService.GetUserByIdAsync(id));
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }

        /// <summary>
        /// Blocks a User.
        /// </summary>
        // PUT: api/User 
        [HttpPut("{id}")]
        public async Task<IActionResult> BlockUserAsync(int id, [FromBody] int hours)
        {
            try
            {
                await userService.BlockUserAsync(id, hours);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
    }
}
