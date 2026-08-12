using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class jointablesattempt01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionCategoryRuleId",
                table: "RuleConditions",
                type: "INTEGER",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RuleConditions_TransactionCategoryRules_TransactionCategoryRuleId",
                table: "RuleConditions");

            migrationBuilder.DropIndex(
                name: "IX_RuleConditions_TransactionCategoryRuleId",
                table: "RuleConditions");

            migrationBuilder.DropColumn(
                name: "TransactionCategoryRuleId",
                table: "RuleConditions");
        }
    }
}
