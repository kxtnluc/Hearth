using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class ruletableattempt01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RuleConditions_TransactionCategoryRules_TransactionCategoryRuleId",
                table: "RuleConditions");

            migrationBuilder.DropIndex(
                name: "IX_RuleConditions_TransactionCategoryRuleId",
                table: "RuleConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionCategoryRules",
                table: "TransactionCategoryRules");

            migrationBuilder.DropColumn(
                name: "RuleTable",
                table: "RuleConditions");

            migrationBuilder.DropColumn(
                name: "TransactionCategoryRuleId",
                table: "RuleConditions");

            migrationBuilder.RenameTable(
                name: "TransactionCategoryRules",
                newName: "Rule");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionCategoryId",
                table: "Rule",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Rule",
                type: "TEXT",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rule",
                table: "Rule",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RuleConditions_RuleId",
                table: "RuleConditions",
                column: "RuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RuleConditions_Rule_RuleId",
                table: "RuleConditions",
                column: "RuleId",
                principalTable: "Rule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RuleConditions_Rule_RuleId",
                table: "RuleConditions");

            migrationBuilder.DropIndex(
                name: "IX_RuleConditions_RuleId",
                table: "RuleConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rule",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Rule");

            migrationBuilder.RenameTable(
                name: "Rule",
                newName: "TransactionCategoryRules");

            migrationBuilder.AddColumn<string>(
                name: "RuleTable",
                table: "RuleConditions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TransactionCategoryRuleId",
                table: "RuleConditions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionCategoryId",
                table: "TransactionCategoryRules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionCategoryRules",
                table: "TransactionCategoryRules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RuleConditions_TransactionCategoryRuleId",
                table: "RuleConditions",
                column: "TransactionCategoryRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RuleConditions_TransactionCategoryRules_TransactionCategoryRuleId",
                table: "RuleConditions",
                column: "TransactionCategoryRuleId",
                principalTable: "TransactionCategoryRules",
                principalColumn: "Id");
        }
    }
}
