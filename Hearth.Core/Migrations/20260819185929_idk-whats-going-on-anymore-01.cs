using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class idkwhatsgoingonanymore01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Account_Number",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Request_Id",
                table: "Accounts",
                newName: "Official_Name");

            migrationBuilder.RenameColumn(
                name: "Offical_Name",
                table: "Accounts",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "Last_Modified",
                table: "Accounts",
                newName: "LastDateRequested");

            migrationBuilder.RenameColumn(
                name: "Last_Date_Requested",
                table: "Accounts",
                newName: "InitalDateRequested");

            migrationBuilder.RenameColumn(
                name: "Item_Id",
                table: "Accounts",
                newName: "Balances_Unofficial_Currency_Code");

            migrationBuilder.RenameColumn(
                name: "Is_Open",
                table: "Accounts",
                newName: "IsOpen");

            migrationBuilder.RenameColumn(
                name: "Institution_Name",
                table: "Accounts",
                newName: "Balances_Limit");

            migrationBuilder.RenameColumn(
                name: "Inital_Date_Requested",
                table: "Accounts",
                newName: "Balances_Iso_Currency_Code");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "Accounts",
                newName: "Account_Id");

            migrationBuilder.RenameColumn(
                name: "Balance_Current",
                table: "Accounts",
                newName: "Balances_Current");

            migrationBuilder.RenameColumn(
                name: "Balance_Available",
                table: "Accounts",
                newName: "Balances_Available");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Official_Name",
                table: "Accounts",
                newName: "Request_Id");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Accounts",
                newName: "Offical_Name");

            migrationBuilder.RenameColumn(
                name: "LastDateRequested",
                table: "Accounts",
                newName: "Last_Modified");

            migrationBuilder.RenameColumn(
                name: "IsOpen",
                table: "Accounts",
                newName: "Is_Open");

            migrationBuilder.RenameColumn(
                name: "InitalDateRequested",
                table: "Accounts",
                newName: "Last_Date_Requested");

            migrationBuilder.RenameColumn(
                name: "Balances_Unofficial_Currency_Code",
                table: "Accounts",
                newName: "Item_Id");

            migrationBuilder.RenameColumn(
                name: "Balances_Limit",
                table: "Accounts",
                newName: "Institution_Name");

            migrationBuilder.RenameColumn(
                name: "Balances_Iso_Currency_Code",
                table: "Accounts",
                newName: "Inital_Date_Requested");

            migrationBuilder.RenameColumn(
                name: "Balances_Current",
                table: "Accounts",
                newName: "Balance_Current");

            migrationBuilder.RenameColumn(
                name: "Balances_Available",
                table: "Accounts",
                newName: "Balance_Available");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "Accounts",
                newName: "BankId");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Account_Number",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
