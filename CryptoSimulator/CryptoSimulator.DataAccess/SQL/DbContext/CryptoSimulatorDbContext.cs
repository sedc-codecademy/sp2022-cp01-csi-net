using CryptoSimulator.DataAccess.SQL.Data;
using CryptoSimulator.DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoSimulator.DataAccess.Data
{
    public class CryptoSimulatorDbContext : DbContext
    {
        public CryptoSimulatorDbContext(DbContextOptions options)
            : base(options)
        {

        }

        DbSet<User> Users { get; set; }
        DbSet<Wallet> Wallets { get; set; }
        DbSet<Coin> Coins { get; set; }
        DbSet<Transaction> UserTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .HasOne(u => u.Wallet)
              .WithOne(w => w.User)
              .HasForeignKey<Wallet>(w => w.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Wallet>()
               .HasMany(w => w.Coins)
               .WithOne(c => c.Wallet)
               .HasForeignKey(w => w.WalletId);


            DataSeed.InsertDataInDb(modelBuilder);

        }

    }
}
