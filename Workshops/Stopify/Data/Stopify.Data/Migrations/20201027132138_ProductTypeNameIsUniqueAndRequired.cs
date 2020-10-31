using Microsoft.EntityFrameworkCore.Migrations;

namespace Stopify.Data.Migrations
{
    public partial class ProductTypeNameIsUniqueAndRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUri",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "PictureUri",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
