namespace Panda.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PackageStatusesColumnNameIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PackageStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PackageStatuses_Name",
                table: "PackageStatuses",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackageStatuses_Name",
                table: "PackageStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PackageStatuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
