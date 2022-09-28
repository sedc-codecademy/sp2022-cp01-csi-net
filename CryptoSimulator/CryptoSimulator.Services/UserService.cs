using AutoMapper;
using CryptoSimulator.Common.Models;
using CryptoSimulator.DataAccess;
using CryptoSimulator.DataModels.Models;
using CryptoSimulator.ServiceModels.UserModels;
using CryptoSimulator.Services.Helpers;
using CryptoSimulator.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CryptoSimulator.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterUser> _registerUserValidator;
        private readonly IValidator<LoginModel> _loginModelValidator;
        private readonly string _secret;


        public UserService(IMapper mapper, IValidator<RegisterUser> registerUserValidator, IValidator<LoginModel> loginModelValidator, IOptions<AppSettings> options)
        {
            _mapper = mapper;
            _registerUserValidator = registerUserValidator;
            _loginModelValidator = loginModelValidator;
            _secret = options.Value.Secret;
        }

        public void Register(RegisterUser request)
        {
            _registerUserValidator.ValidateAndThrow(request);

            // ova da se smeni so repository
            var userFromDb = StaticDB.Users.FirstOrDefault(x => x.Username == request.Username);

            if (userFromDb != null)
            {
                throw new Exception("User with that username already exist");
            }

            request.Password = UserHelper.HashPassword(request.Password);

            var user = _mapper.Map<User>(request);

            // da se napravi wallet i activity log

            // ova da se smeni so repository
            StaticDB.Users.Add(user);
        }

        public UserLoginDto Login(LoginModel request)
        {
            _loginModelValidator.ValidateAndThrow(request);

            // ova da se smeni so repository
            var userFromDb = StaticDB.Users.FirstOrDefault(x => x.Username == request.Username);

            if (userFromDb is null)
            {
                throw new Exception("User with that username does not exists");
            }

            var hashedPassword = UserHelper.HashPassword(request.Password);

            if (!BCryptNet.Verify(request.Password, userFromDb.Password))
            {
                throw new Exception("Password does not match!");
            }

            // create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{userFromDb.FirstName} {userFromDb.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString())
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var loginDto = new UserLoginDto
            {
                Id = userFromDb.Id,
                Username = userFromDb.Username,
                Token = tokenHandler.WriteToken(token)
            };

            return loginDto;
        }
    }
}
