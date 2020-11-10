namespace JokesApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<DropDownViewModel>> GetAllAsync()
            => await this.categoriesRepository.All()
                .OrderBy(c => c.Name)
                .Select(c => new DropDownViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

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