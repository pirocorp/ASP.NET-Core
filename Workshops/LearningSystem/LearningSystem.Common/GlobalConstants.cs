namespace LearningSystem.Common
{
    public static class GlobalConstants
    {
        public const int AllowedExamUploadFileSize = 2 * 1024 * 1024;

        public const string AdministratorArea = "Admin";
        public const string BlogArea = "Blog";

        public const string AdministratorRole = "Admin";
        public const string BlogAuthorRole = "BlogAuthor";
        public const string TrainerRole = "Trainer";

        public const string RemoveRole = "None";

        public const string TempDataSuccessMessageKey = "SuccessMessage";

        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const int ArticlesPageSize = 5;
        public const int CoursesPageSize = 5;
        public const int UsersPageSize = 5;

        public const int ContentDemoLength = 600;
        public const string DateTimePreferredFormat = "dd/MM/yyyy";

        public const string PdfCertificateFormat = @"
<!DOCTYPE html>
<html>
    <head>
        <meta charset=""UTF-8"">
    </head>
    <body>
        <h1>Certificate</h1>
        <h2>{3} - Grade {4}<h2>
        <br />
        <h2>{0} Course</h2>
        <h3>{1} - {2}<h3>
        <h4>Signed by: {5}</h4>
    </body
</html>
";
    }
}
