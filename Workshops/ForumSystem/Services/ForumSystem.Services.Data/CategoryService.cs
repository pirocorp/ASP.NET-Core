namespace ForumSystem.Services.Data
{
    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;

    public class CategoryService : DeletableEntityService<Category>, ICategoryService, IDeletableEntityService<Category>
    {
        public CategoryService(IDeletableEntityRepository<Category> entityRepository)
            : base(entityRepository)
        {
        }
    }
}
