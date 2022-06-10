using bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using bll.DTO.Comment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Program.Controllers
{
    
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        /// <summary>
        /// Creates a comment.
        /// </summary>
        // POST: api/Post/1
        [Route("api/Post/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> AddCommentAsync([FromBody] AddCommentDto comment, [FromRoute] int id)
        {
            try
            {
                var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await commentService.AddCommentAsync(comment, userName, id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a Comment by Id.
        /// </summary>
        //DELETE: api/Comment/1
        [Route("api/Comment/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            try
            {
                await commentService.DeleteCommentAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
