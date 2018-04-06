using Microsoft.EntityFrameworkCore.Migrations;

namespace SweetShop.DAL.Migrations
{
    public partial class changeconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Companies_CompanyId",
                table: "ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Customers_CustomerId",
                table: "ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers");

            migrationBuilder.DropIndex(
                name: "IX_ProductCustomers_CompanyId",
                table: "ProductCustomers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ProductCustomers");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductCustomers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ProductCustomers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Customers_CustomerId",
                table: "ProductCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Customers_CustomerId",
                table: "ProductCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductCustomers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "ProductCustomers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ProductCustomers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCustomers_CompanyId",
                table: "ProductCustomers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Companies_CompanyId",
                table: "ProductCustomers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Customers_CustomerId",
                table: "ProductCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
