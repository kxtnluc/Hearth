using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class bankattempt04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankUserAccessTokens");

            migrationBuilder.RenameColumn(
                name: "Institution_Name",
                table: "Banks",
                newName: "Item_Id");

            migrationBuilder.RenameColumn(
                name: "Institution_Id",
                table: "Banks",
                newName: "Access_Token");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Banks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Request_Id",
                table: "Banks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Banks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Request_Id",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "Item_Id",
                table: "Banks",
                newName: "Institution_Name");

            migrationBuilder.RenameColumn(
                name: "Access_Token",
                table: "Banks",
                newName: "Institution_Id");

            migrationBuilder.CreateTable(
                name: "BankUserAccessTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Access_Token = table.Column<string>(type: "TEXT", nullable: false),
                    BankId = table.Column<int>(type: "INTEGER", nullable: false),
                    Request_Id = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUserAccessTokens", x => x.Id);
                });
        }
    }
}
