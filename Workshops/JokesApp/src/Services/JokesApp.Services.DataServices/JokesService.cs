namespace JokesApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common;
    using Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Home;

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
                .To<IndexJokeViewModel>()
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

        public async Task<TOut> GetJokeByIdAsync<TOut>(int id)
            => await this.jokesRepository.All()
                .Where(j => j.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();
    }
}
