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
            services.AddScoped<IValidator<RegisterUser>, RegisterUserValidator>();
            services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();

            return services;
        }
    }
}
