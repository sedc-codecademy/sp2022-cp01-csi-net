using CryptoSimulator.DataModels.Models;

namespace CryptoSimulator.DataAccess
{
    public static class StaticDB
    {
        public static List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Bobsky",
                Password = "abc123",
                Email = "bob@gmail.com",
                Wallet = new Wallet{Id = Guid.Empty, UserId = 1, Cash = 100_000,},
                Username = "bobsky",
            }
        };
    }
}
