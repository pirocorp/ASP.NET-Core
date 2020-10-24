namespace Panda.Services.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using App.Models;
    using App.Models.ViewModels.Package;
    using Data;
    using Infrastructure;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Moq;
    using Panda.Models;
    using Xunit;

    public class PackagesServiceTests
    {
        [Fact]
        public async Task TestCreateAsync_ShouldCreatePackageCorrectly()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            RegisterAutoMappings();

            var statusesService = new Mock<IStatusesService>();

            var expectedStatusId = "Status Id";

            statusesService
                .Setup(s => s.GetPackageStatusIdByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedStatusId));

            var packagesService = new PackagesService(context, statusesService.Object);

            var date = DateTime.UtcNow;

            var model = new PackageCreateServiceModel()
            {
                Description = "Description",
                EstimatedDeliveryDate = date,
                RecipientId = "Recipient Id",
                ShippingAddress = "Shipping address",
                StatusId = "Invalid Status",
                Weight = 0.1D,
            };
            
            var packageId = await packagesService.CreateAsync(model);
            var package = await context.Packages.FirstOrDefaultAsync(p => p.Id.Equals(packageId));

            Assert.NotNull(package);
            Assert.Equal(packageId, package.Id);
            Assert.Equal(model.Description, package.Description);
            Assert.Equal(model.EstimatedDeliveryDate, date);
            Assert.Equal(model.RecipientId, package.RecipientId);
            Assert.Equal(model.ShippingAddress, package.ShippingAddress);
            Assert.Equal(model.StatusId, expectedStatusId);
            Assert.Equal(model.Weight, package.Weight);
            Assert.Equal(model.ShippingAddress, package.ShippingAddress);
        }

        [Fact]
        public async Task GetPackagesByStatusCodeAsync_ShouldReturnAllPackagesWithGivenStatusCode()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());

            var statusesService = new Mock<IStatusesService>();

            var expectedStatusId = "Given Status Id";

            for (var i = 0; i < 100; i++)
            {
                var package = new Package()
                {
                    Description = "Description",
                    EstimatedDeliveryDate = DateTime.UtcNow,
                    RecipientId = "Recipient Id",
                    ShippingAddress = "Shipping address",
                    StatusId = "Invalid Status",
                    Weight = 0.1D,
                };

                await context.AddAsync(package);
            }

            await context.SaveChangesAsync();

            var expectedCount = 12;

            for (var i = 0; i < expectedCount; i++)
            {
                var package = new Package()
                {
                    Description = "Description",
                    EstimatedDeliveryDate = DateTime.UtcNow,
                    RecipientId = "Recipient Id",
                    ShippingAddress = "Shipping address",
                    StatusId = expectedStatusId,
                    Weight = 0.1D,
                };

                await context.AddAsync(package);
            }

            await context.SaveChangesAsync();

            statusesService
                .Setup(s => s.GetPackageStatusIdByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedStatusId));

            var packagesService = new PackagesService(context, statusesService.Object);

            RegisterAutoMappings();

            var packages = await packagesService.GetPackagesByStatusCodeAsync<PackageViewModel>(expectedStatusId);
            var packagesOverloadMethod = await packagesService.GetPackagesByStatusCodeAsync<PackageViewModel>(ShipmentStatus.Pending);

            Assert.Equal(expectedCount, packages.Count());
            Assert.Equal(expectedCount, packagesOverloadMethod.Count());
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
