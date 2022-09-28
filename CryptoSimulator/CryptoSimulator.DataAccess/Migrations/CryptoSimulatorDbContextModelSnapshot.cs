﻿// <auto-generated />
using System;
using CryptoSimulator.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoSimulator.DataAccess.Migrations
{
    [DbContext(typeof(CryptoSimulatorDbContext))]
    partial class CryptoSimulatorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CoinId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceBought")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("Coins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoinId = "btc",
                            Name = "Bitcoin",
                            PriceBought = 19000.0,
                            Quantity = 1.0,
                            WalletId = new Guid("51aee8e1-c129-4042-b9c0-c1fd13a8ac51")
                        },
                        new
                        {
                            Id = 2,
                            CoinId = "btc",
                            Name = "Bitcoin",
                            PriceBought = 19000.0,
                            Quantity = 1.0,
                            WalletId = new Guid("291354c5-212c-44ba-bcdb-dc8f12b174aa")
                        },
                        new
                        {
                            Id = 3,
                            CoinId = "eth",
                            Name = "Ethereum",
                            PriceBought = 4000.0,
                            Quantity = 2.0,
                            WalletId = new Guid("291354c5-212c-44ba-bcdb-dc8f12b174aa")
                        });
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("BuyOrSell")
                        .HasColumnType("bit");

                    b.Property<string>("CoinName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTransactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BuyOrSell = true,
                            CoinName = "Bitcoin",
                            DateCreated = new DateTime(2022, 9, 28, 15, 30, 35, 700, DateTimeKind.Local).AddTicks(3654),
                            Price = 19000.0,
                            Quantity = 1.0,
                            TotalPrice = 19000.0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BuyOrSell = true,
                            CoinName = "Bitcoin",
                            DateCreated = new DateTime(2022, 9, 28, 15, 30, 35, 700, DateTimeKind.Local).AddTicks(3692),
                            Price = 19000.0,
                            Quantity = 1.0,
                            TotalPrice = 19000.0,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            BuyOrSell = true,
                            CoinName = "Ethereum",
                            DateCreated = new DateTime(2022, 9, 28, 15, 30, 35, 700, DateTimeKind.Local).AddTicks(3695),
                            Price = 2000.0,
                            Quantity = 2.0,
                            TotalPrice = 4000.0,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "bob@bobsky.com",
                            FirstName = "Bob",
                            LastName = "Bobsky",
                            Password = "BobBobsky123",
                            Username = "bobbobsky"
                        },
                        new
                        {
                            Id = 2,
                            Email = "boba@bobsky.com",
                            FirstName = "Boba",
                            LastName = "Bobsky",
                            Password = "BobaBobsky123",
                            Username = "bobabobsky"
                        });
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Cash")
                        .HasColumnType("float");

                    b.Property<double>("MaxCoins")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("291354c5-212c-44ba-bcdb-dc8f12b174aa"),
                            Cash = 100000.0,
                            MaxCoins = 10.0,
                            UserId = 2
                        },
                        new
                        {
                            Id = new Guid("51aee8e1-c129-4042-b9c0-c1fd13a8ac51"),
                            Cash = 100000.0,
                            MaxCoins = 10.0,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Coin", b =>
                {
                    b.HasOne("CryptoSimulator.DataModels.Models.Wallet", "Wallet")
                        .WithMany("Coins")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Transaction", b =>
                {
                    b.HasOne("CryptoSimulator.DataModels.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Wallet", b =>
                {
                    b.HasOne("CryptoSimulator.DataModels.Models.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("CryptoSimulator.DataModels.Models.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.User", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("CryptoSimulator.DataModels.Models.Wallet", b =>
                {
                    b.Navigation("Coins");
                });
#pragma warning restore 612, 618
        }
    }
}
