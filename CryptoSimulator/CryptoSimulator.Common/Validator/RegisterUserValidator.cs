using CryptoSimulator.ServiceModels.UserModels;
using FluentValidation;

namespace CryptoSimulator.Common.Validator
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(3, 15);//staveno e so 3 za test!
            RuleFor(x => x.Password).NotEmpty().Matches("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            RuleFor(x => x.Firstname).NotEmpty().Length(3, 15);//staveno e so 3 za test!
            RuleFor(x => x.Lastname).NotEmpty().Length(3, 15);//staveno e so 3 za test!
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
        }
    }
}
