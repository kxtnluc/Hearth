using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class JoinTransactionAndAccountattempt01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsOpen",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Accounts_Account_Id",
                table: "Accounts",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Account_Id",
                table: "Transactions",
                column: "Account_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_Account_Id",
                table: "Transactions",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Account_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_Account_Id",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Account_Id",
                table: "Transactions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Accounts_Account_Id",
                table: "Accounts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOpen",
                table: "Accounts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }
    }
}
