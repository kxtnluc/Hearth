using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class bankingreworkagain02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalFinanceCategory_Detailed",
                table: "Transactions",
                newName: "Personal_Finance_Category_Detailed");

            migrationBuilder.RenameColumn(
                name: "PersonalFinanceCategory_Confidence_Level1",
                table: "Transactions",
                newName: "Personal_Finance_Category_Confidence_Level");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Personal_Finance_Category_Detailed",
                table: "Transactions",
                newName: "PersonalFinanceCategory_Detailed");

            migrationBuilder.RenameColumn(
                name: "Personal_Finance_Category_Confidence_Level",
                table: "Transactions",
                newName: "PersonalFinanceCategory_Confidence_Level1");
        }
    }
}
