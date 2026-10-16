using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate3 : Migration
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
                    Account_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Balances_Available = table.Column<decimal>(type: "TEXT", nullable: true),
                    Balances_Current = table.Column<decimal>(type: "TEXT", nullable: true),
                    Balances_Iso_Currency_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Balances_Limit = table.Column<decimal>(type: "TEXT", nullable: true),
                    Balances_Unofficial_Currency_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Mask = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Official_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Subtype = table.Column<string>(type: "TEXT", nullable: true),
                    IsOpen = table.Column<bool>(type: "INTEGER", nullable: false),
                    Bank_Item_Id = table.Column<string>(type: "TEXT", nullable: true),
                    HexColor = table.Column<string>(type: "TEXT", nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AccountRoutingNumber = table.Column<string>(type: "TEXT", nullable: true),
                    HearthName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.UniqueConstraint("AK_Accounts_Account_Id", x => x.Account_Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Purchase_Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Expected_Growth_Or_Decay = table.Column<decimal>(type: "TEXT", nullable: false),
                    Asset_Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Compound_Rate = table.Column<int>(type: "INTEGER", nullable: false),
                    Purchase_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LoanId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: true),
                    Hex_Color = table.Column<string>(type: "TEXT", nullable: false),
                    Ignore = table.Column<bool>(type: "INTEGER", nullable: false),
                    Debit = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Item_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Access_Token = table.Column<string>(type: "TEXT", nullable: false),
                    Request_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Institution_Id = table.Column<string>(type: "TEXT", nullable: true),
                    InitalDateRequested = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastDateRequested = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Loan_Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Amortized = table.Column<bool>(type: "INTEGER", nullable: false),
                    Principal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Term = table.Column<int>(type: "INTEGER", nullable: false),
                    Interest_Rate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Compound = table.Column<int>(type: "INTEGER", nullable: false),
                    Payment_Frequency = table.Column<int>(type: "INTEGER", nullable: false),
                    Due_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Downpayment = table.Column<decimal>(type: "TEXT", nullable: true),
                    Principal_Paid = table.Column<decimal>(type: "TEXT", nullable: false),
                    Interest_Paid = table.Column<decimal>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<decimal>(type: "TEXT", nullable: true),
                    HexColor = table.Column<string>(type: "TEXT", nullable: false),
                    IconName = table.Column<string>(type: "TEXT", nullable: true),
                    IsNeed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ignore = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionSyncRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Next_Cursor = table.Column<string>(type: "TEXT", nullable: false),
                    Has_More = table.Column<bool>(type: "INTEGER", nullable: false),
                    Request_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Transactions_Update_Status = table.Column<string>(type: "TEXT", nullable: false),
                    Write_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Item_Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionSyncRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Pin = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    RuleType = table.Column<string>(type: "TEXT", maxLength: 34, nullable: false),
                    BankCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    TransactionCategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_TransactionCategories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Transaction_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Account_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Account_Owner = table.Column<string>(type: "TEXT", nullable: true),
                    Authorized_Date = table.Column<string>(type: "TEXT", nullable: true),
                    Authorized_Datetime = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Check_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Datetime = table.Column<string>(type: "TEXT", nullable: true),
                    Iso_Currency_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Logo_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Merchant_Entity_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Merchant_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Merchant_Category_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Payment_Channel = table.Column<string>(type: "TEXT", nullable: true),
                    Pending = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pending_Transaction_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Personal_Finance_Category_Icon_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Transaction_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Transaction_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Unofficial_Currency_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Address = table.Column<string>(type: "TEXT", nullable: true),
                    Location_City = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Country = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Lat = table.Column<double>(type: "REAL", nullable: true),
                    Location_Lon = table.Column<double>(type: "REAL", nullable: true),
                    Location_Postal_Code = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Region = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Store_Number = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_By_Order_Of = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Payee = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Payer = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Payment_Method = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Payment_Processor = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Ppd_Id = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Reason = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentMeta_Reference_Number = table.Column<string>(type: "TEXT", nullable: true),
                    PersonalFinanceCategory_Primary = table.Column<string>(type: "TEXT", nullable: true),
                    PersonalFinanceCategory_Detailed = table.Column<string>(type: "TEXT", nullable: true),
                    PersonalFinanceCategory_Confidence_Level = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "Accounts",
                        principalColumn: "Account_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RuleConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RuleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Field = table.Column<string>(type: "TEXT", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: false),
                    Match = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuleConditions_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCounterparties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Logo_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    Entity_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Confidence_Level = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCounterparties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCounterparties_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Account_Id",
                table: "Accounts",
                column: "Account_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RuleConditions_RuleId",
                table: "RuleConditions",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_TransactionCategoryId",
                table: "Rules",
                column: "TransactionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCounterparties_TransactionId",
                table: "TransactionCounterparties",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Account_Id",
                table: "Transactions",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "BankCategories");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "RuleConditions");

            migrationBuilder.DropTable(
                name: "TransactionCounterparties");

            migrationBuilder.DropTable(
                name: "TransactionSyncRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "TransactionCategories");
        }
    }
}
