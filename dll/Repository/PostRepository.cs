using dal.Context;
using dal.Interface;
using dal.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace dal.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext dbContext;

        public PostRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Post> CreateAsync(Post post)
        {
            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();
            return post;
        }

        public async Task DeleteAsync(int id)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null) {
                throw new NotFoundException(nameof(Post), id);
            }

            dbContext.Posts.Remove(post); await dbContext.SaveChangesAsync();
        }

        public async Task<PagedList<Post>> GetPostsAsync(PostParameters postParameters)
        {
            if (dbContext.Posts == null) {
                throw new NotFoundException("Posts not found");
            }

            var posts = await PagedList<Post>.ToPagedListAsync(
                dbContext.Posts,
                postParameters.PageNumber,
                postParameters.PageSize);

            return posts;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await dbContext.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.PostId == id);
        }

        public async Task UpdateAsync(Post post)
        {
            var item = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == post.PostId);
            if (item == null) {
                throw new NotFoundException(nameof(Post), post.PostId);
            }

            dbContext.Posts.Update(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
