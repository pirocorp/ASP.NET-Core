namespace ForumSystem.Services.Data
{
    using System.Linq;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class CategoryService : DeletableEntityService<Category>, ICategoryService
    {
        public CategoryService(IDeletableEntityRepository<Category> entityRepository)
            : base(entityRepository)
        {
        }

        public TModel GetByName<TModel>(string name)
        {
            var category = this.EntityRepository
                .All()
                .Where(x => x.Name == name)
                .To<TModel>()
                .FirstOrDefault();

            return category;
        }
    }
}
