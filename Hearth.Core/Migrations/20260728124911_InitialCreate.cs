using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<string>(type: "TEXT", nullable: false),
                    BankId = table.Column<string>(type: "TEXT", nullable: false),
                    Mask = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Offical_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Subtype = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Institution_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Institution_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Item_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Request_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Inital_Date_Requested = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Last_Date_Requested = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Last_Modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Balance_Available = table.Column<decimal>(type: "TEXT", nullable: true),
                    Balance_Current = table.Column<decimal>(type: "TEXT", nullable: true),
                    Account_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Is_Open = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BankId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Institution_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Access_Token = table.Column<string>(type: "TEXT", nullable: false),
                    Request_Id = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransactionId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    AccountId = table.Column<string>(type: "TEXT", nullable: false),
                    Account_Owner = table.Column<string>(type: "TEXT", nullable: true),
                    Authorized_Date = table.Column<string>(type: "TEXT", nullable: true),
                    Authorized_Datetime = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Plaid_Category_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Check_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Datetime = table.Column<string>(type: "TEXT", nullable: true),
                    Iso_Currency_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Logo_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Merchant_Entity_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Merchant_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Payment_Channel = table.Column<string>(type: "TEXT", nullable: true),
                    Pending = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pending_Transaction_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Personal_Finance_Category_Icon_Url = table.Column<string>(type: "TEXT", nullable: false),
                    Transaction_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Transaction_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Unofficial_Currency_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Personal_Finance_Category_Primary = table.Column<string>(type: "TEXT", nullable: true),
                    Personal_Finance_Category_Detailed = table.Column<string>(type: "TEXT", nullable: true),
                    Personal_Finance_Category_Confidence_Level = table.Column<string>(type: "TEXT", nullable: true),
                    Confidence_Level = table.Column<string>(type: "TEXT", nullable: false),
                    EntityId = table.Column<string>(type: "TEXT", nullable: false),
                    LogoUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Counter_Party_Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: true),
                    Lon = table.Column<double>(type: "REAL", nullable: true),
                    Postal_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    Store_Number = table.Column<string>(type: "TEXT", nullable: false),
                    By_Order_Of = table.Column<string>(type: "TEXT", nullable: false),
                    Payee = table.Column<string>(type: "TEXT", nullable: false),
                    Payer = table.Column<string>(type: "TEXT", nullable: false),
                    Payment_Method = table.Column<string>(type: "TEXT", nullable: false),
                    Payment_Processor = table.Column<string>(type: "TEXT", nullable: false),
                    PpdId = table.Column<string>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: false),
                    Reference_Number = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
