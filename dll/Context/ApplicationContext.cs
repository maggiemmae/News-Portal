using dal.Maps;
using dal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dal.Context
{
    public class ApplicationContext : IdentityDbContext<User, ApplicationRole, int>
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserMap().Build(modelBuilder.Entity<User>());
            new PostMap().Build(modelBuilder.Entity<Post>());
            new CommentMap().Build(modelBuilder.Entity<Comment>());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base()
        {
        }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
