using dal.Context;
using dal.Interface;
using dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                throw new KeyNotFoundException("Post not found");
            }
            dbContext.Posts.Remove(post); await dbContext.SaveChangesAsync();
        }

        public async Task<PostsPaged> GetPostsAsync(int page)
        {
            if (dbContext.Posts == null) {
                throw new NullReferenceException("Posts not found");
            }

            var pageResults = 2f;
            var pageCount = (int)Math.Ceiling(dbContext.Posts.Count() / pageResults);

            var posts = await dbContext.Posts
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var result = new PostsPaged(pageCount, posts);

            return result;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await dbContext.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.PostId == id);
        }

        public async Task UpdateAsync(Post post)
        {
            var item = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == post.PostId);
            if (item == null) {
                throw new KeyNotFoundException("Post not found");
            }
            dbContext.Posts.Update(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
