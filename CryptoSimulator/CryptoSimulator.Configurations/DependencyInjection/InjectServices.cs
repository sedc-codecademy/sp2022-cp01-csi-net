using CryptoSimulator.Services;
using CryptoSimulator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoSimulator.Configurations.DependencyInjection
{
    public static class InjectServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
