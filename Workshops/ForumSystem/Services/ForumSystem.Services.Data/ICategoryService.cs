namespace ForumSystem.Services.Data
{
    using ForumSystem.Data.Models;

    public interface ICategoryService : IDeletableEntityService<Category>
    {
        TEntity GetByName<TEntity>(string name);
    }
}
