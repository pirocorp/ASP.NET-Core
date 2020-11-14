namespace LearningSystem.Web.Models.Admin.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AddToRoleInputModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}
