using bll.DTO.User;
using bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Program.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Login.
        /// </summary>
        // POST: api/Login 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await accountService.Login(model);
            return Ok(result);
        }

        /// <summary>
        /// Registration.
        /// </summary>
        // POST: api/Login 
        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await accountService.Register(model);
            return Ok(result);
        }
    }
}
