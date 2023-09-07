using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuniMVC.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FoodId1",
                table: "Foods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodId1",
                table: "Foods",
                column: "FoodId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Foods_FoodId1",
                table: "Foods",
                column: "FoodId1",
                principalTable: "Foods",
                principalColumn: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Foods_FoodId1",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodId1",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FoodId1",
                table: "Foods");
        }
    }
}
