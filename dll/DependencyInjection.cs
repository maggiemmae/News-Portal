using dal.Context;
using dal.Interface;
using dal.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dal
{
    public static class DependencyInjection
    {
        public static IServiceCollection InitializeDal(this IServiceCollection services, IConfiguration conf)
        {
            var connectionString = conf.GetConnectionString("DefaultConnection");

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString, config => config.MigrationsAssembly("Migrations")));
            return services;
        }
    }
}
