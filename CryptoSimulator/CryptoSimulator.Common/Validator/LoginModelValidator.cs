using CryptoSimulator.ServiceModels.UserModels;
using FluentValidation;

namespace CryptoSimulator.Common.Validator
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
