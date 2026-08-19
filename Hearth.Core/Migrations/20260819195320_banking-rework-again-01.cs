using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class bankingreworkagain01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "InitalDateRequested",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Institution_Id",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastDateRequested",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Accounts",
                newName: "Bank_Item_Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "InitalDateRequested",
                table: "Banks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institution_Id",
                table: "Banks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDateRequested",
                table: "Banks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Banks",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitalDateRequested",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Institution_Id",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "LastDateRequested",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Banks");

            migrationBuilder.RenameColumn(
                name: "Bank_Item_Id",
                table: "Accounts",
                newName: "LastModified");

            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Banks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InitalDateRequested",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institution_Id",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDateRequested",
                table: "Accounts",
                type: "TEXT",
                nullable: true);
        }
    }
}
