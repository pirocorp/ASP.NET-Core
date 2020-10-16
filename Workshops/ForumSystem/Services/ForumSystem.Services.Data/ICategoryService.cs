namespace ForumSystem.Services.Data
{
    using ForumSystem.Data.Models;

    public interface ICategoryService : IDeletableEntityService<Category, int>
    {
        TModel GetByName<TModel>(string name);
    }
}
