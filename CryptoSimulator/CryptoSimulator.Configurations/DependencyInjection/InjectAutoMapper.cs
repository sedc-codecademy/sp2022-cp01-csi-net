using CryptoSimulator.Common.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoSimulator.Configurations.DependencyInjection
{
    public static class InjectAutoMapper
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapper));

            return services;
        }
    }
}
