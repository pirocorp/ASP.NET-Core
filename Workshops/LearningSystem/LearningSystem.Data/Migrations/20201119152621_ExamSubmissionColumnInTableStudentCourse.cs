namespace LearningSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ExamSubmissionColumnInTableStudentCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ExamSubmission",
                table: "StudentCourses",
                type: "varbinary(max)",
                maxLength: 2097152,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamSubmission",
                table: "StudentCourses");
        }
    }
}
