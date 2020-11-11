namespace JokesApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<TOut>> GetAllAsync<TOut>()
            => await this.categoriesRepository.All()
                .OrderBy(c => c.Name)
                .To<TOut>()
                .ToListAsync();

        public async Task<TOut> GetById<TOut>(int id)
            => await this.categoriesRepository.All()
                .Where(c => c.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public bool Exists(int categoryId)
            => this.categoriesRepository.All()
                .Any(c => c.Id.Equals(categoryId));

        public int? GetCategoryId(string name)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Name == name);
            return category?.Id;
        }
    }
}