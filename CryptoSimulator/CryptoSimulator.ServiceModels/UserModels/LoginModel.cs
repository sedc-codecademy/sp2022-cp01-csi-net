using System.ComponentModel.DataAnnotations;

namespace CryptoSimulator.ServiceModels.UserModels
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}