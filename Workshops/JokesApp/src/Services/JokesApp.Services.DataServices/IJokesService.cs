﻿namespace JokesApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Home;
    using Models.Jokes;

    public interface IJokesService
    {
        Task<IEnumerable<IndexJokeViewModel>> GetRandomJokesAsync(int count);

        Task<int> GetCountAsync();

        Task<int> CreateAsync(int categoryId, string content);

        Task<JokeDetailsViewModel> GetJokeByIdAsync(int id);
    }
}