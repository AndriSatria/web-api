using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class CreateDbUserAndTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseTransactions",
                columns: table => new
                {
                    ReceiptNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(nullable: false),
                    TransactDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTransactions", x => x.ReceiptNo);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<byte[]>(nullable: false),
                    MobileNo = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    IsEmailOptIn = table.Column<bool>(nullable: false),
                    Point = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNo = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    PurchaseTransactionReceiptNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseTransactionDetails_PurchaseTransactions_PurchaseTransactionReceiptNo",
                        column: x => x.PurchaseTransactionReceiptNo,
                        principalTable: "PurchaseTransactions",
                        principalColumn: "ReceiptNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseTransactionDetails_PurchaseTransactionReceiptNo",
                table: "PurchaseTransactionDetails",
                column: "PurchaseTransactionReceiptNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseTransactionDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PurchaseTransactions");
        }
    }
}
