using Microsoft.EntityFrameworkCore.Migrations;

namespace Stopify.Data.Migrations
{
    public partial class OrderStatusesNameIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_Name",
                table: "OrderStatuses",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_Name",
                table: "OrderStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OrderStatuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
