namespace JokesApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Home;

    public interface IJokesService
    {
        Task<IEnumerable<IndexJokeViewModel>> GetRandomJokesAsync(int count);

        Task<int> GetCountAsync();
    }
}