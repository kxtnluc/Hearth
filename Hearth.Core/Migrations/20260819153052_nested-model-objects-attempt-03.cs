using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hearth.Core.Migrations
{
    /// <inheritdoc />
    public partial class nestedmodelobjectsattempt03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Counter_Party_Name",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PpdId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Store_Number",
                table: "Transactions",
                newName: "Location_Store_Number");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Transactions",
                newName: "Location_Region");

            migrationBuilder.RenameColumn(
                name: "Reference_Number",
                table: "Transactions",
                newName: "PaymentMeta_Reference_Number");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Transactions",
                newName: "PaymentMeta_Reason");

            migrationBuilder.RenameColumn(
                name: "Postal_Code",
                table: "Transactions",
                newName: "Location_Postal_Code");

            migrationBuilder.RenameColumn(
                name: "Payment_Processor",
                table: "Transactions",
                newName: "PaymentMeta_Payment_Processor");

            migrationBuilder.RenameColumn(
                name: "Payment_Method",
                table: "Transactions",
                newName: "PaymentMeta_Payment_Method");

            migrationBuilder.RenameColumn(
                name: "Payer",
                table: "Transactions",
                newName: "PaymentMeta_Payer");

            migrationBuilder.RenameColumn(
                name: "Payee",
                table: "Transactions",
                newName: "PaymentMeta_Payee");

            migrationBuilder.RenameColumn(
                name: "Lon",
                table: "Transactions",
                newName: "Location_Lon");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "Transactions",
                newName: "Location_Lat");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Transactions",
                newName: "Location_Country");

            migrationBuilder.RenameColumn(
                name: "Confidence_Level",
                table: "Transactions",
                newName: "PersonalFinanceCategory_Confidence_Level1");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Transactions",
                newName: "Location_City");

            migrationBuilder.RenameColumn(
                name: "By_Order_Of",
                table: "Transactions",
                newName: "PaymentMeta_By_Order_Of");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Transactions",
                newName: "Location_Address");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Transactions",
                newName: "Transaction_Id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "Account_Id");

            migrationBuilder.RenameColumn(
                name: "Plaid_Category_Id",
                table: "Transactions",
                newName: "PersonalFinanceCategory_Detailed");

            migrationBuilder.RenameColumn(
                name: "Personal_Finance_Category_Primary",
                table: "Transactions",
                newName: "PersonalFinanceCategory_Confidence_Level");

            migrationBuilder.RenameColumn(
                name: "Personal_Finance_Category_Detailed",
                table: "Transactions",
                newName: "PaymentMeta_Ppd_Id");

            migrationBuilder.RenameColumn(
                name: "Personal_Finance_Category_Confidence_Level",
                table: "Transactions",
                newName: "Merchant_Category_Code");

            migrationBuilder.AlterColumn<string>(
                name: "Location_Store_Number",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Location_Region",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_Reference_Number",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_Reason",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Location_Postal_Code",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_Payment_Processor",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_Payment_Method",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_Payer",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_Payee",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Location_Country",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalFinanceCategory_Confidence_Level1",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Location_City",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMeta_By_Order_Of",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Location_Address",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "TransactionCounterparties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Logo_Url = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    Entity_Id = table.Column<string>(type: "TEXT", nullable: true),
                    Confidence_Level = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCounterparties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCounterparties_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCounterparties_TransactionId",
                table: "TransactionCounterparties",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionCounterparties");

            migrationBuilder.RenameColumn(
                name: "PersonalFinanceCategory_Confidence_Level1",
                table: "Transactions",
                newName: "Confidence_Level");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Reference_Number",
                table: "Transactions",
                newName: "Reference_Number");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Reason",
                table: "Transactions",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Payment_Processor",
                table: "Transactions",
                newName: "Payment_Processor");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Payment_Method",
                table: "Transactions",
                newName: "Payment_Method");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Payer",
                table: "Transactions",
                newName: "Payer");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Payee",
                table: "Transactions",
                newName: "Payee");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_By_Order_Of",
                table: "Transactions",
                newName: "By_Order_Of");

            migrationBuilder.RenameColumn(
                name: "Location_Store_Number",
                table: "Transactions",
                newName: "Store_Number");

            migrationBuilder.RenameColumn(
                name: "Location_Region",
                table: "Transactions",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "Location_Postal_Code",
                table: "Transactions",
                newName: "Postal_Code");

            migrationBuilder.RenameColumn(
                name: "Location_Lon",
                table: "Transactions",
                newName: "Lon");

            migrationBuilder.RenameColumn(
                name: "Location_Lat",
                table: "Transactions",
                newName: "Lat");

            migrationBuilder.RenameColumn(
                name: "Location_Country",
                table: "Transactions",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Location_City",
                table: "Transactions",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Location_Address",
                table: "Transactions",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Transaction_Id",
                table: "Transactions",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "PersonalFinanceCategory_Detailed",
                table: "Transactions",
                newName: "Plaid_Category_Id");

            migrationBuilder.RenameColumn(
                name: "PersonalFinanceCategory_Confidence_Level",
                table: "Transactions",
                newName: "Personal_Finance_Category_Primary");

            migrationBuilder.RenameColumn(
                name: "PaymentMeta_Ppd_Id",
                table: "Transactions",
                newName: "Personal_Finance_Category_Detailed");

            migrationBuilder.RenameColumn(
                name: "Merchant_Category_Code",
                table: "Transactions",
                newName: "Personal_Finance_Category_Confidence_Level");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Confidence_Level",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reference_Number",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Payment_Processor",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Payment_Method",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Payer",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Payee",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "By_Order_Of",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Store_Number",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Postal_Code",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Counter_Party_Name",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EntityId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PpdId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
