using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoSimulator.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BuyOrSell = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxCoins = table.Column<double>(type: "float", nullable: false),
                    Cash = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceBought = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coins_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1, "bob@bobsky.com", "Bob", "Bobsky", "BobBobsky123", "bobbobsky" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 2, "boba@bobsky.com", "Boba", "Bobsky", "BobaBobsky123", "bobabobsky" });

            migrationBuilder.InsertData(
                table: "UserTransactions",
                columns: new[] { "Id", "BuyOrSell", "CoinName", "DateCreated", "Price", "Quantity", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, true, "Bitcoin", new DateTime(2022, 9, 28, 15, 30, 35, 700, DateTimeKind.Local).AddTicks(3654), 19000.0, 1.0, 19000.0, 1 },
                    { 2, true, "Bitcoin", new DateTime(2022, 9, 28, 15, 30, 35, 700, DateTimeKind.Local).AddTicks(3692), 19000.0, 1.0, 19000.0, 2 },
                    { 3, true, "Ethereum", new DateTime(2022, 9, 28, 15, 30, 35, 700, DateTimeKind.Local).AddTicks(3695), 2000.0, 2.0, 4000.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Cash", "MaxCoins", "UserId" },
                values: new object[,]
                {
                    { new Guid("291354c5-212c-44ba-bcdb-dc8f12b174aa"), 100000.0, 10.0, 2 },
                    { new Guid("51aee8e1-c129-4042-b9c0-c1fd13a8ac51"), 100000.0, 10.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "CoinId", "Name", "PriceBought", "Quantity", "WalletId" },
                values: new object[] { 1, "btc", "Bitcoin", 19000.0, 1.0, new Guid("51aee8e1-c129-4042-b9c0-c1fd13a8ac51") });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "CoinId", "Name", "PriceBought", "Quantity", "WalletId" },
                values: new object[] { 2, "btc", "Bitcoin", 19000.0, 1.0, new Guid("291354c5-212c-44ba-bcdb-dc8f12b174aa") });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "CoinId", "Name", "PriceBought", "Quantity", "WalletId" },
                values: new object[] { 3, "eth", "Ethereum", 4000.0, 2.0, new Guid("291354c5-212c-44ba-bcdb-dc8f12b174aa") });

            migrationBuilder.CreateIndex(
                name: "IX_Coins_WalletId",
                table: "Coins",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactions_UserId",
                table: "UserTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "UserTransactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
