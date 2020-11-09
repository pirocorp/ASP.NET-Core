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

    public class JokesService : IJokesService
    {
        private readonly IRepository<Joke> jokesRepository;

        public JokesService(IRepository<Joke> jokesRepository)
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
                    CategoryName = j.Category.Name
                })
                .Take(count)
                .ToListAsync();

        public async Task<int> GetCountAsync()
            => await this.jokesRepository.All().CountAsync();
    }
}
