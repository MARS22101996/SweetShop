using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SweetShop.DAL.Migrations
{
    public partial class addcascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCustomers_Products_ProductId",
                table: "ProductCustomers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
