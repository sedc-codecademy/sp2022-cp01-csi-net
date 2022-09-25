using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text.Encodings;

namespace CryptoSimulator.Services.Helpers
{
    public class UserHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidUsername(string usernameFromDb, string newUsername)
        {
            return !usernameFromDb.Equals(newUsername, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsValidPassword(string password)
        {
            var passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            var match = passwordRegex.Match(password);
            return match.Success;
        }

        //public static string HashPassword(string password)
        //{
        //    var hmac = new HMACSHA256();
        //    var hashedPassword = hmac.

        //}
    }
}
