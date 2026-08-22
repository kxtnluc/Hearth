using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class bankattempt03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Access_Token",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Request_Id",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "Banks",
                newName: "Institution_Id");

            migrationBuilder.CreateTable(
                name: "BankUserAccessTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BankId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Access_Token = table.Column<string>(type: "TEXT", nullable: false),
                    Request_Id = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUserAccessTokens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankUserAccessTokens");

            migrationBuilder.RenameColumn(
                name: "Institution_Id",
                table: "Banks",
                newName: "BankId");

            migrationBuilder.AddColumn<string>(
                name: "Access_Token",
                table: "Banks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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
    }
}
