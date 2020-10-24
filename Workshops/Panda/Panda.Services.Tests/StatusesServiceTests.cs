namespace Panda.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Panda.Models;
    using Xunit;

    public class StatusesServiceTests
    {
        [Fact]
        public async Task TestGetPackageStatusIdByNameAsync_ShouldReturnCorrectUserId()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var status = new PackageStatus()
            {
                Name = "Test Status",
            };

            await context.PackageStatuses.AddAsync(status);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var serviceStatusId =  await statusesService.GetPackageStatusIdByNameAsync(status.Name);

            Assert.Equal(status.Id, serviceStatusId);
        }

        [Fact]
        public async Task TestGetPackageStatusIdByNameAsync_IfNotFound_ShouldReturnStringEmpty()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var status = new PackageStatus()
            {
                Name = "Test Status",
            };

            await context.PackageStatuses.AddAsync(status);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var serviceStatusId =  await statusesService.GetPackageStatusIdByNameAsync("Invalid Name");

            Assert.Equal(string.Empty, serviceStatusId);
        }

        [Fact]
        public async Task TestGetPackageStatusIdByNameAsync_IfNameIsNull_ShouldReturnStringEmpty()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var status = new PackageStatus()
            {
                Name = "Test Status",
            };

            await context.PackageStatuses.AddAsync(status);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var serviceStatusId =  await statusesService.GetPackageStatusIdByNameAsync(null);

            Assert.Equal(string.Empty, serviceStatusId);
        }

        [Fact]
        public async Task TestExistsAsync_StatusIdIsPresent_ShouldReturnTrue()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var status = new PackageStatus()
            {
                Name = "Test Status",
            };

            await context.PackageStatuses.AddAsync(status);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var exists =  await statusesService.ExistsAsync(status.Id);

            Assert.True(exists);
        }

        [Fact]
        public async Task TestExistsAsync_ExistInMultipleStatuses_ShouldReturnTrue()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var statuses = new List<PackageStatus>()
            {
                new PackageStatus() { Name = "Test Status 1" },
                new PackageStatus() { Name = "Test Status 2" },
                new PackageStatus() { Name = "Test Status 3" },
                new PackageStatus() { Name = "Test Status 4" },
                new PackageStatus() { Name = "Test Status 5" },
                new PackageStatus() { Name = "Test Status 6" },
            };

            await context.PackageStatuses.AddRangeAsync(statuses);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var exists =  await statusesService.ExistsAsync(statuses[2].Id);

            Assert.True(exists);
        }

        [Fact]
        public async Task TestExistsAsync_NotExistsMultipleStatuses_ShouldReturnTrue()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var statuses = new List<PackageStatus>()
            {
                new PackageStatus() { Name = "Test Status 1" },
                new PackageStatus() { Name = "Test Status 2" },
                new PackageStatus() { Name = "Test Status 3" },
                new PackageStatus() { Name = "Test Status 4" },
                new PackageStatus() { Name = "Test Status 5" },
                new PackageStatus() { Name = "Test Status 6" },
            };

            await context.PackageStatuses.AddRangeAsync(statuses);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var exists =  await statusesService.ExistsAsync("Invalid Id");

            Assert.False(exists);
        }

        [Fact]
        public async Task TestExistsAsync_NotExistsSingleStatus_ShouldReturnTrue()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var statuses = new List<PackageStatus>()
            {
                new PackageStatus() { Name = "Test Status 1" }
            };

            await context.PackageStatuses.AddRangeAsync(statuses);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var exists =  await statusesService.ExistsAsync("Invalid Id");

            Assert.False(exists);
        }

        [Fact]
        public async Task TestExistsAsync_NotExistsStringEmpty_ShouldReturnTrue()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var statuses = new List<PackageStatus>()
            {
                new PackageStatus() { Name = "Test Status 1" },
                new PackageStatus() { Name = "Test Status 2" },
                new PackageStatus() { Name = "Test Status 3" },
                new PackageStatus() { Name = "Test Status 4" },
                new PackageStatus() { Name = "Test Status 5" },
                new PackageStatus() { Name = "Test Status 6" },
            };

            await context.PackageStatuses.AddRangeAsync(statuses);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var exists =  await statusesService.ExistsAsync(string.Empty);

            Assert.False(exists);
        }

        [Fact]
        public async Task TestExistsAsync_NotExistsNull_ShouldReturnTrue()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var statuses = new List<PackageStatus>()
            {
                new PackageStatus() { Name = "Test Status 1" },
                new PackageStatus() { Name = "Test Status 2" },
                new PackageStatus() { Name = "Test Status 3" },
                new PackageStatus() { Name = "Test Status 4" },
                new PackageStatus() { Name = "Test Status 5" },
                new PackageStatus() { Name = "Test Status 6" },
            };

            await context.PackageStatuses.AddRangeAsync(statuses);
            await context.SaveChangesAsync();

            var statusesService = new StatusesService(context);
            var exists =  await statusesService.ExistsAsync(null);

            Assert.False(exists);
        }

        private static DbContextOptions<PandaDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // The key to keeping the databases unique and not shared is 
            // generating a unique db name for each.
            var dbName = Guid.NewGuid().ToString();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<PandaDbContext>();
            builder.UseInMemoryDatabase(dbName)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
