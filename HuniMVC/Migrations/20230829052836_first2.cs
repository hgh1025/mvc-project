using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuniMVC.Migrations
{
    /// <inheritdoc />
    public partial class first2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MovieId",
                table: "Messages",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Movies_MovieId",
                table: "Messages",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Movies_MovieId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MovieId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Messages");
        }
    }
}
