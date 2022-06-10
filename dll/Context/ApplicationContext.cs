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
            modelBuilder.Entity<User>().HasIndex(b => b.UserName).IsUnique();
            modelBuilder.Entity<Post>().HasOne(a => a.User).WithMany(b => b.Posts).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Post>().HasIndex(b => b.Title).IsUnique();
            modelBuilder.Entity<Comment>().HasOne(a => a.User).WithMany(b => b.Comments).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasOne(a => a.Post).WithMany(b => b.Comments).OnDelete(DeleteBehavior.Cascade);

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
