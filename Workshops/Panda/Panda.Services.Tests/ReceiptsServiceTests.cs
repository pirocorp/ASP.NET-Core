namespace Panda.Services.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using App.Models.ViewModels.Receipt;
    using Data;
    using Infrastructure;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    using Panda.App.Models;
    using Panda.Models;
    using Xunit;

    public class ReceiptsServiceTests
    {
        [Fact]
        public async Task TestCreateAsync_EmptyDatabase_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());
            RegisterAutoMappings();

            var receiptsService = new ReceiptsService(context);

            var input = new ReceiptCreateServiceModel()
            {
                Weight = 100,
                PackageId = "1",
                RecipientId = "1",
            };

            var receiptId = await receiptsService.CreateAsync(input);
            var receipt = await context.Receipts.FirstOrDefaultAsync(r => r.Id.Equals(receiptId));

            Assert.Equal(receiptId, receipt.Id);
            Assert.Equal(input.PackageId, receipt.PackageId);
            Assert.Equal(input.RecipientId, receipt.RecipientId);
            Assert.Equal(GlobalConstants.FeeRatio * (decimal)input.Weight, receipt.Fee);
        }

        [Fact]
        public async Task TestCreateAsync_NotEmptyDatabase_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());

            var receiptsService = new ReceiptsService(context);

            for (var i = 0; i < 10; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = $"{i}1",
                };

                await context.AddAsync(item);
            }

            await context.SaveChangesAsync();

            var input = new ReceiptCreateServiceModel()
            {
                Weight = 100,
                PackageId = "1",
                RecipientId = "1",
            };

            RegisterAutoMappings();

            var receiptId = await receiptsService.CreateAsync(input);
            var receipt = await context.Receipts.FirstOrDefaultAsync(r => r.Id.Equals(receiptId));

            Assert.Equal(receiptId, receipt.Id);
            Assert.Equal(input.PackageId, receipt.PackageId);
            Assert.Equal(input.RecipientId, receipt.RecipientId);
            Assert.Equal(GlobalConstants.FeeRatio * (decimal)input.Weight, receipt.Fee);
        }

        [Fact]
        public async Task TestGetAllAsync_NotEmptyDatabase_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());

            var receiptsService = new ReceiptsService(context);

            var expectedCount = 100;

            for (var i = 0; i < expectedCount; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = $"{i}1",
                };

                await context.AddAsync(item);
            }

            await context.SaveChangesAsync();

            RegisterAutoMappings();
            var all = await receiptsService.GetAllAsync<ReceiptIndexViewModel>();
            
            Assert.Equal(expectedCount, all.Count());
        }

        [Fact]
        public async Task TestGetAllAsync_EmptyDatabase_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());
            RegisterAutoMappings();

            var receiptsService = new ReceiptsService(context);

            var expectedCount = 0;

            var all = await receiptsService.GetAllAsync<ReceiptIndexViewModel>();
            
            Assert.Equal(expectedCount, all.Count());
        }

        [Fact]
        public async Task TestGetAllByUserAsync_UserHasReceipts_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());

            var receiptsService = new ReceiptsService(context);

            var expectedCount = 10;

            for (var i = 0; i < 100; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = $"user{i}",
                };

                await context.AddAsync(item);
            }

            var userId = "this user";

            for (var i = 0; i < expectedCount; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = userId,
                };

                await context.AddAsync(item);
            }

            await context.SaveChangesAsync();

            RegisterAutoMappings();
            var all = await receiptsService.GetAllByUserAsync<ReceiptIndexViewModel>(userId);
            
            Assert.Equal(expectedCount, all.Count());
        }

        [Fact]
        public async Task TestGetAllByUserAsync_UserHasNoReceipts_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());

            var receiptsService = new ReceiptsService(context);

            var expectedCount = 0;

            for (var i = 0; i < 100; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = $"user{i}",
                };

                await context.AddAsync(item);
            }

            var userId = "this user";

            RegisterAutoMappings();
            var all = await receiptsService.GetAllByUserAsync<ReceiptIndexViewModel>(userId);
            
            Assert.Equal(expectedCount, all.Count());
        }

        [Fact]
        public async Task TestCreateAsync_ReceiptExists_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());

            var receiptsService = new ReceiptsService(context);

            for (var i = 0; i < 10; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = $"{i}1",
                };

                await context.AddAsync(item);
            }

            await context.SaveChangesAsync();

            RegisterAutoMappings();

            var input = new ReceiptCreateServiceModel()
            {
                Weight = 100,
                PackageId = "1",
                RecipientId = "1",
            };

            RegisterAutoMappings();

            var receiptId = await receiptsService.CreateAsync(input);

            var receipt = await receiptsService.GetByIdAsync<ReceiptIndexViewModel>(receiptId);

            Assert.NotNull(receipt);
            Assert.Equal(receiptId, receipt.Id);
        }

        [Fact]
        public async Task TestCreateAsync_ReceiptNotExists_ShouldCreateReceiptCorrectly()
        {
            await using var context = new PandaDbContext(CreateNewContextOptions());

            var receiptsService = new ReceiptsService(context);

            for (var i = 0; i < 10; i++)
            {
                var item = new Receipt()
                {
                    Fee = i + 500,
                    PackageId = $"{i}1",
                    RecipientId = $"{i}1",
                };

                await context.AddAsync(item);
            }

            await context.SaveChangesAsync();

            RegisterAutoMappings();

            var receiptId = "Invalid ID";
            var receipt = await receiptsService.GetByIdAsync<ReceiptIndexViewModel>(receiptId);

            Assert.Null(receipt);
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

        private static void RegisterAutoMappings()
        {
            AutoMapperConfig
                .RegisterMappings(
                    typeof(ErrorViewModel).GetTypeInfo().Assembly,
                    typeof(PackageCreateServiceModel).GetTypeInfo().Assembly);
        }
    }
}
