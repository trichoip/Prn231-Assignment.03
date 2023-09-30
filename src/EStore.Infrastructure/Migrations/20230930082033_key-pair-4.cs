using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class keypair4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PubId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "PubId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PubId",
                table: "Books",
                column: "PubId",
                principalTable: "Publishers",
                principalColumn: "PubId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PubId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "PubId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PubId",
                table: "Books",
                column: "PubId",
                principalTable: "Publishers",
                principalColumn: "PubId");
        }
    }
}
