using dal.Context;
using dal.Interface;
using dal.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace dal.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext dbContext;

        public UserRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await dbContext.Users.Include(x => x.Comments).Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) {
                throw new NotFoundException(nameof(User), id);
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PagedList<User>> GetAllAsync(UserParameters userParameters)
        {
            if (dbContext.Users == null) {
                throw new NotFoundException("Users not found");
            }

            var users = await PagedList<User>.ToPagedListAsync(
                dbContext.Users,
                userParameters.PageNumber,
                userParameters.PageSize);

            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            var item = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (item == null) {
                throw new NotFoundException(nameof(User), user.Id);
            }

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
