using CryptoSimulator.Common.Validator;
using CryptoSimulator.ServiceModels.UserModels;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoSimulator.Configurations.DependencyInjection
{
    public static class InjectFluentValidation
    {
        public static IServiceCollection RegisterFluentValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RegisterUser>, RegisterUserValidator>();

            return services;
        }
    }
}
