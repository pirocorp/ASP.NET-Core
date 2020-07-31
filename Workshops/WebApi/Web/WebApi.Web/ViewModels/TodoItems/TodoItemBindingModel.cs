namespace WebApi.Web.ViewModels.TodoItems
{
    using System.ComponentModel.DataAnnotations;

    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class TodoItemBindingModel : IMapTo<TodoItem>
    {
        [Required]
        public string Title { get; set; }
    }
}
