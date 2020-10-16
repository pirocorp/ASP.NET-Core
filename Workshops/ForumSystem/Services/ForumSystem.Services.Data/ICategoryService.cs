namespace ForumSystem.Services.Data
{
    using ForumSystem.Data.Models;

    public interface ICategoryService : IDeletableEntityService<Category>
    {
        TModel GetByName<TModel>(string name);
    }
}
