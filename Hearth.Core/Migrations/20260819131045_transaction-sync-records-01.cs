using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class transactionsyncrecords01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionSyncRecords");
        }
    }
}
