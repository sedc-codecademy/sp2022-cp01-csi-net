using CryptoSimulator.DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoSimulator.DataAccess.SQL.Data
{
    internal static class DataSeed
    {
        internal static void InsertDataInDb(ModelBuilder modelBuilder)
        {
            Guid walletOneId = Guid.NewGuid();
            Guid walletTwoId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Email = "bob@bobsky.com",
                    Username = "bobbobsky",
                    Password = "BobBobsky123",
                },
                 new User
                 {
                     Id = 2,
                     FirstName = "Boba",
                     LastName = "Bobsky",
                     Email = "boba@bobsky.com",
                     Username = "bobabobsky",
                     Password = "BobaBobsky123",
                 });

            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = walletTwoId,
                    Cash = 100_000,
                    MaxCoins = 10,
                    UserId = 2,
                },
                new Wallet
                {
                    Id = walletOneId,
                    Cash = 100_000,
                    MaxCoins = 10,
                    UserId = 1,
                });

            modelBuilder.Entity<Coin>().HasData(
                new Coin
                {
                    Id = 1,
                    CoinId = "btc",
                    Name = "Bitcoin",
                    PriceBought = 19_000,
                    Quantity = 1,
                    WalletId = walletOneId,
                },
              new Coin
              {
                  Id = 2,
                  CoinId = "btc",
                  Name = "Bitcoin",
                  PriceBought = 19_000,
                  Quantity = 1,
                  WalletId = walletTwoId,
              },
               new Coin
               {
                   Id = 3,
                   CoinId = "eth",
                   Name = "Ethereum",
                   PriceBought = 4_000,
                   Quantity = 2,
                   WalletId = walletTwoId,
               });


            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    UserId = 1,
                    CoinName = "Bitcoin",
                    Price = 19_000,
                    TotalPrice = 19_000,
                    BuyOrSell = true,
                    DateCreated = DateTime.Now.ToLocalTime(),
                    Quantity = 1,
                },
                 new Transaction
                 {
                     Id = 2,
                     UserId = 2,
                     CoinName = "Bitcoin",
                     Price = 19_000,
                     TotalPrice = 19_000,
                     BuyOrSell = true,
                     DateCreated = DateTime.Now.ToLocalTime(),
                     Quantity = 1,
                 },
                 new Transaction
                 {
                     Id = 3,
                     UserId = 2,
                     CoinName = "Ethereum",
                     Price = 2_000,
                     TotalPrice = 4_000,
                     BuyOrSell = true,
                     DateCreated = DateTime.Now.ToLocalTime(),
                     Quantity = 2,
                 });

        }
    }
}
