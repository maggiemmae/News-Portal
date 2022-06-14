using dal.Context;
using dal.Interface;
using dal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace dal.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext dbContext;

        public CommentRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
            if (comment == null) {
                throw new NotFoundException(nameof(Comment), id);
            }

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await dbContext.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await dbContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            dbContext.Comments.Update(comment);
            await dbContext.SaveChangesAsync();
        }
    }
}
