using bll.DTO.User;
using bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var result = await accountService.Login(model);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Registration.
        /// </summary>
        // POST: api/Login 
        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await accountService.Register(model);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }

        /// <summary>
        /// Registers admin.
        /// </summary>
        // POST: api/Login 
        [HttpPost("adminregistration")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            try
            {
                await accountService.RegisterAdmin(model);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
    }
}
