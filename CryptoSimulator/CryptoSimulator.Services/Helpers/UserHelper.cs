using CryptoSimulator.DataModels.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CryptoSimulator.Services.Helpers
{
    public static class UserHelper
    {
        public static bool IsValidUsername(string usernameFromDb, string newUsername)
        {
            return !usernameFromDb.Equals(newUsername, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string HashPassword(string password)
        {
            var hashedPassword = BCryptNet.HashPassword(password);
            return hashedPassword;
            
            //using var hmac = new HMACSHA256();
            //var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            //return Encoding.ASCII.GetString(hashedPassword);

            //var md5 = new MD5CryptoServiceProvider();
            //var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            //return Encoding.ASCII.GetString(md5data);
        }
    }
}
