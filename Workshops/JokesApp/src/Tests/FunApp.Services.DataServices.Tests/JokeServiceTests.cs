namespace FunApp.Services.DataServices.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JokesApp.Data;
    using JokesApp.Data.Models;
    using JokesApp.Services.DataServices;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class JokeServiceTests
    {
        [Fact]
        public async Task GetCountAsyncShouldReturnsCorrectNumber()
        {
            var options = new DbContextOptionsBuilder<JokesAppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var dbContext = new JokesAppDbContext(options);

            await dbContext.Jokes.AddRangeAsync(new List<Joke>()
            {
                new Joke(),
                new Joke(),
                new Joke(),
                new Joke(),
            });
            await dbContext.SaveChangesAsync();

            var repo = new DbRepository<Joke>(dbContext);
            var service = new JokesService(repo);

            Assert.Equal(4, (await service.GetCountAsync()));
        }
    }
}
