using CryptoSimulator.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoSimulator.Configurations.DependencyInjection
{
    public static class InjectDbContext
    {
        public static IServiceCollection InjectAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CryptoSimulatorDbContext>(opt => opt.UseSqlServer(connectionString));
            return services;
        }
    }
}
