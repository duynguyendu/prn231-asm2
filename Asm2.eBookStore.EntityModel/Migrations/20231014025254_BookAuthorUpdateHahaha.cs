using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asm2.eBookStore.EntityModel.Migrations
{
    /// <inheritdoc />
    public partial class BookAuthorUpdateHahaha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Publishers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_PublisherId",
                table: "Publishers",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Publishers_PublisherId",
                table: "Publishers",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Publishers_PublisherId",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_PublisherId",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Publishers");
        }
    }
}
