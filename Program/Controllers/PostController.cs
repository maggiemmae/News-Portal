using bll.DTO.Post;
using bll.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Program.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        /// <summary>
        /// Gets all Posts.
        /// </summary>
        // GET: api/Post
        [HttpGet("{page}")]

        public async Task<IActionResult> GetPostListAsync(int page)
        {
            try
            {
                var result = await postService.GetPostListAsync(page);
                return Ok(result);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets a Post by Id.
        /// </summary>
        // GET: api/Post/1
        [HttpGet("page/{id}")]
        public async Task<IActionResult> GetPostByIdAsync(int id)
        {
            try
            {
                var result = await postService.GetPostByIdAsync(id);
                return Ok(result);
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }

        /// <summary>
        /// Deletes a Post by Id.
        /// </summary>
        // DELETE: api/Post/1
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(int id)
        {
            try
            {
                await postService.DeletePostAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates a Post.
        /// </summary>
        // PUT: api/Post
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostDto post)
        {
            try
            {
                await postService.UpdatePostAsync(post);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }

        /// <summary>
        /// Creates a Post.
        /// </summary>
        // POST: api/Post
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddPostAsync([FromBody] AddPostViewModel post)
        {
            try
            {
                var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await postService.AddPostAsync(post, userName);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
