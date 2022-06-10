using bll.Interfaces;
using bll.Services;
using Microsoft.Extensions.DependencyInjection;

namespace bll
{
    public static class DependencyInjection
    {
        public static IServiceCollection InitializeBll(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
