namespace CameraBazaar.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CameraTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<int>(nullable: false),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    MinShutterSpeed = table.Column<int>(nullable: false),
                    MaxShutterSpeed = table.Column<int>(nullable: false),
                    MinISO = table.Column<int>(nullable: false),
                    MaxISO = table.Column<int>(nullable: false),
                    IsFullFrame = table.Column<bool>(nullable: false),
                    VideoResolution = table.Column<string>(maxLength: 15, nullable: false),
                    LightMetering = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 6000, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cameras_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_UserId",
                table: "Cameras",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cameras");
        }
    }
}
