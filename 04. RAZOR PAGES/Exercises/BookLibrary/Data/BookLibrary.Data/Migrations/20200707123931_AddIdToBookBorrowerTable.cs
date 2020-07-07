using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibrary.Data.Migrations
{
    public partial class AddIdToBookBorrowerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksBorrowers",
                table: "BooksBorrowers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BooksBorrowers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksBorrowers",
                table: "BooksBorrowers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BooksBorrowers_BookId",
                table: "BooksBorrowers",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksBorrowers",
                table: "BooksBorrowers");

            migrationBuilder.DropIndex(
                name: "IX_BooksBorrowers_BookId",
                table: "BooksBorrowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BooksBorrowers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksBorrowers",
                table: "BooksBorrowers",
                columns: new[] { "BookId", "BorrowerId" });
        }
    }
}
