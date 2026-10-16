using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class reports4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HideAllTime",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HideBudget",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HideCategoryGraphsRates",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HidePeriodRates",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockPrimaryYear",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockSecondaryYear",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HideAllTime",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "HideBudget",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "HideCategoryGraphsRates",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "HidePeriodRates",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LockPrimaryYear",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "LockSecondaryYear",
                table: "Reports");
        }
    }
}
