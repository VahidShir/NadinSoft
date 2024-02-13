using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinSoft.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProduceDate",
                table: "Products");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProduceDate",
                table: "Products",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProduceDate",
                table: "Products",
                column: "ProduceDate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProduceDate",
                table: "Products");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ProduceDate",
                table: "Products",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProduceDate",
                table: "Products",
                column: "ProduceDate",
                unique: true,
                filter: "[ProduceDate] IS NOT NULL");
        }
    }
}
