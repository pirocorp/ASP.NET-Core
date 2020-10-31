namespace ForumSystem.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    /// <summary>
    /// Integration test of vote service, repository and database.
    /// </summary>
    public class VotesServiceTests
    {
        [Fact]
        public async Task OneDownVotesShouldCountOnce()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfRepository<Vote>(new ApplicationDbContext(builder.Options));
            var service = new VotesService(repository);

            await service.VoteAsync(1, "1", VoteType.DownVote);

            var downVotes = service.GetDownVotesCount(1);
            var upVotes = service.GetUpVotesCount(1);
            var votes = service.GetPostVotes(1).Count();

            Assert.Equal(1, downVotes);
            Assert.Equal(0, upVotes);
            Assert.Equal(1, votes);
        }

        [Fact]
        public async Task TwoDownVotesShouldCountOnce()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfRepository<Vote>(new ApplicationDbContext(builder.Options));
            var service = new VotesService(repository);

            await service.VoteAsync(1, "1", VoteType.DownVote);
            await service.VoteAsync(1, "1", VoteType.DownVote);

            var downVotes = service.GetDownVotesCount(1);
            var upVotes = service.GetUpVotesCount(1);
            var votes = service.GetPostVotes(1).Count();

            Assert.Equal(0, downVotes);
            Assert.Equal(0, upVotes);
            Assert.Equal(1, votes);
        }

        [Fact]
        public async Task ThreeDownVotesShouldCountOnce()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfRepository<Vote>(new ApplicationDbContext(builder.Options));
            var service = new VotesService(repository);

            await service.VoteAsync(1, "1", VoteType.DownVote);
            await service.VoteAsync(1, "1", VoteType.DownVote);
            await service.VoteAsync(1, "1", VoteType.DownVote);

            var downVotes = service.GetDownVotesCount(1);
            var upVotes = service.GetUpVotesCount(1);
            var votes = service.GetPostVotes(1).Count();

            Assert.Equal(1, downVotes);
            Assert.Equal(0, upVotes);
            Assert.Equal(1, votes);
        }

        [Fact]
        public async Task ChangingDownVoteToUpVote()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfRepository<Vote>(new ApplicationDbContext(builder.Options));
            var service = new VotesService(repository);

            await service.VoteAsync(1, "1", VoteType.DownVote);
            await service.VoteAsync(1, "1", VoteType.UpVote);

            var downVotes = service.GetDownVotesCount(1);
            var upVotes = service.GetUpVotesCount(1);
            var votes = service.GetPostVotes(1).Count();

            Assert.Equal(0, downVotes);
            Assert.Equal(1, upVotes);
            Assert.Equal(1, votes);
        }
    }
}
