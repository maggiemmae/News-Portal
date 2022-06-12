using bll.DTO.Comment;
using bll.DTO.User;
using bll.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Program.Controllers
{
    [Route("api/[controller]")]
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
        // POST: api/Comment/1
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("{postId}")]
        public async Task<IActionResult> AddCommentAsync([FromBody] AddCommentDto comment, [FromRoute] int postId)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await commentService.AddCommentAsync(comment, userName, postId);
            return Ok();
        }

        /// <summary>
        /// Deletes a Comment by Id.
        /// </summary>
        //DELETE: api/Comment/1
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            await commentService.DeleteCommentAsync(id);
            return Ok();
        }
    }
}
