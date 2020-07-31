namespace WebApi.Web.ViewModels.TodoItems
{
    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class TodoItemViewModel : IMapFrom<TodoItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }
    }
}
