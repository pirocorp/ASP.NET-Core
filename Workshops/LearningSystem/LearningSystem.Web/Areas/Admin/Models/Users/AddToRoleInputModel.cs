namespace LearningSystem.Web.Areas.Admin.Models.Users
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
