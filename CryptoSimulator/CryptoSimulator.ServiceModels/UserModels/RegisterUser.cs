using System.ComponentModel.DataAnnotations;

namespace CryptoSimulator.ServiceModels.UserModels
{
    public class RegisterUser
    {
        [Required]
        public string? Firstname { get; set; }
        [Required]
        public string? Lastname { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password does not match!")]
        public string? ConfirmPassword { get; set; }
    }
}
