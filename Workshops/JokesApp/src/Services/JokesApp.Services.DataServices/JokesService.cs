namespace JokesApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Home;
    using Models.Jokes;

    public class JokesService : IJokesService
    {
        private readonly IRepository<Joke> jokesRepository;

        public JokesService(
            IRepository<Joke> jokesRepository)
        {
            this.jokesRepository = jokesRepository;
        }

        public async Task<IEnumerable<IndexJokeViewModel>> GetRandomJokesAsync(int count)
            => await this.jokesRepository
                .All()
                .OrderBy(x => Guid.NewGuid())
                .Select(j => new IndexJokeViewModel()
                {
                    Content = j.Content
                        .Replace("\r\n", "<br />")
                        .Replace("\n", "<br />"),
                    CategoryName = j.Category.Name,
                    Id = j.Id
                })
                .Take(count)
                .ToListAsync();

        public async Task<int> GetCountAsync()
            => await this.jokesRepository.All().CountAsync();

        public async Task<int> CreateAsync(int categoryId, string content)
        {
            var joke = new Joke()
            {
                CategoryId = categoryId,
                Content = content
            };

            await this.jokesRepository.AddAsync(joke);
            await this.jokesRepository.SaveChangesAsync();

            return joke.Id;
        }

        public async Task<JokeDetailsViewModel> GetJokeByIdAsync(int id)
            => await this.jokesRepository.All()
                .Where(j => j.Id.Equals(id))
                .Select(j => new JokeDetailsViewModel()
                {
                    CategoryName = j.Category.Name,
                    Content = j.Content,
                })
                .FirstOrDefaultAsync();
    }
}
