using CryptoSimulator.DataAccess.Repositories;
using CryptoSimulator.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoSimulator.Configurations.DependencyInjection
{
    public static class InjectRepositories
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();

            return services;
        }
    }
}
