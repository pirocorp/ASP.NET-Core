namespace ForumSystem.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using ForumSystem.Data;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using TestViewModels;
    using Xunit;

    /// <summary>
    /// PostService unit tests
    /// </summary>
    public class PostServiceTests
    {
        [Fact]
        public async Task TestGetById()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(builder.Options));

            for (var i = 1; i <= 100; i++)
            {
                var newPost = new Post()
                {
                    Title = $"{i}. Title",
                    Content = $"{i}. Content",
                };

                await repository.AddAsync(newPost);
            }

            await repository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(MyTestPostViewModel).Assembly);

            var postService = new PostService(repository);
            
            for (var i = 1; i <= 100; i++)
            {
                var post = postService.GetById<MyTestPostViewModel>(i);

                Assert.Equal(i, post.Id);
                Assert.Equal($"{i}. Title", post.Title);
                Assert.Equal($"{i}. Content", post.Content);
            }
        }
    }
}
