using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class ruletableattempt02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RuleConditions_Rule_RuleId",
                table: "RuleConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rule",
                table: "Rule");

            migrationBuilder.RenameTable(
                name: "Rule",
                newName: "Rules");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Rules",
                newName: "RuleType");

            migrationBuilder.AddColumn<int>(
                name: "BankCategoryId",
                table: "Rules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rules",
                table: "Rules",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_RuleConditions_Rules_RuleId",
                table: "RuleConditions",
                column: "RuleId",
                principalTable: "Rules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RuleConditions_Rules_RuleId",
                table: "RuleConditions");

            migrationBuilder.DropTable(
                name: "BankCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rules",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "BankCategoryId",
                table: "Rules");

            migrationBuilder.RenameTable(
                name: "Rules",
                newName: "Rule");

            migrationBuilder.RenameColumn(
                name: "RuleType",
                table: "Rule",
                newName: "Discriminator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rule",
                table: "Rule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RuleConditions_Rule_RuleId",
                table: "RuleConditions",
                column: "RuleId",
                principalTable: "Rule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
