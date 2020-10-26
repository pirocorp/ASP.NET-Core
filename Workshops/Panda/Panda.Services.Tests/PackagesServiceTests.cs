namespace Panda.Services.Tests
{
    using System;
    using System.Collections.Generic;
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

        [Fact]
        public async Task ExistsAsync_Exists()
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

            var existsPackage = new Package()
            {
                Description = "Description",
                EstimatedDeliveryDate = DateTime.UtcNow,
                RecipientId = "Recipient Id",
                ShippingAddress = "Shipping address",
                StatusId = expectedStatusId,
                Weight = 0.1D,
            };

            await context.AddAsync(existsPackage);
            await context.SaveChangesAsync();

            var packagesService = new PackagesService(context, statusesService.Object);

            RegisterAutoMappings();

            Assert.True(await packagesService.ExistsAsync(existsPackage.Id));
        }

        [Fact]
        public async Task ExistsAsync_NotExists()
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

            var packagesService = new PackagesService(context, statusesService.Object);

            RegisterAutoMappings();

            Assert.False(await packagesService.ExistsAsync("Invalid Id"));
        }

        [Fact]
        public async Task ChangeStatusAsync_Success()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());
            RegisterAutoMappings();

            var statusesService = new Mock<IStatusesService>();
            var expectedStatusId = "New Status Id";

            var package = new Package()
            {
                Description = "Description",
                EstimatedDeliveryDate = DateTime.UtcNow,
                RecipientId = "Recipient Id",
                ShippingAddress = "Shipping address",
                StatusId = "Status Id",
                Weight = 0.1D,
            };

            await context.AddAsync(package);
            await context.SaveChangesAsync();

            statusesService
                .Setup(s => s.GetPackageStatusIdByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedStatusId));

            statusesService
                .Setup(s => s.ExistsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var packagesService = new PackagesService(context, statusesService.Object);

            var success = await packagesService.ChangeStatusAsync(package.Id, ShipmentStatus.Acquired);
            var successOverload = await packagesService.ChangeStatusAsync(package.Id, package.StatusId);

            Assert.True(success);
            Assert.True(successOverload);
            Assert.Equal(expectedStatusId, package.StatusId);
        }

        [Fact]
        public async Task ChangeStatusAsync_Fail()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());
            RegisterAutoMappings();

            var statusesService = new Mock<IStatusesService>();
            var expectedStatusId = "New Status Id";

            var package = new Package()
            {
                Description = "Description",
                EstimatedDeliveryDate = DateTime.UtcNow,
                RecipientId = "Recipient Id",
                ShippingAddress = "Shipping address",
                StatusId = "Status Id",
                Weight = 0.1D,
            };

            await context.AddAsync(package);
            await context.SaveChangesAsync();

            statusesService
                .Setup(s => s.GetPackageStatusIdByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(expectedStatusId));

            statusesService
                .Setup(s => s.ExistsAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            var packagesService = new PackagesService(context, statusesService.Object);

            var success = await packagesService.ChangeStatusAsync(package.Id, ShipmentStatus.Acquired);
            var successOverload = await packagesService.ChangeStatusAsync(package.Id, package.StatusId);

            Assert.False(success);
            Assert.False(successOverload);
            Assert.NotEqual(expectedStatusId, package.StatusId);
        }

        [Fact]
        public async Task GetByIdAsync_Exists()
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

            var existsPackage = new Package()
            {
                Description = "Description",
                EstimatedDeliveryDate = DateTime.UtcNow,
                RecipientId = "Recipient Id",
                ShippingAddress = "Shipping address",
                StatusId = expectedStatusId,
                Weight = 0.1D,
            };

            await context.AddAsync(existsPackage);
            await context.SaveChangesAsync();

            var packagesService = new PackagesService(context, statusesService.Object);

            RegisterAutoMappings();
            var resultPackage = await packagesService.GetByIdAsync<PackageAcquireModel>(existsPackage.Id);

            Assert.NotNull(resultPackage);
            Assert.Equal(existsPackage.Id, resultPackage.Id);
        }

        [Fact]
        public async Task GetByIdAsync_NotExists()
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
            var packagesService = new PackagesService(context, statusesService.Object);

            RegisterAutoMappings();
            var resultPackage = await packagesService.GetByIdAsync<PackageAcquireModel>("Invalid Id");

            Assert.Null(resultPackage);
        }

        [Fact]
        public async Task GetAllPackagesInTheSystemAsync_ShouldReturnAllPackagesIntTheSystem()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());
            RegisterAutoMappings();

            foreach (ShipmentStatus item in Enum.GetValues(typeof(ShipmentStatus)))
            {
                var status = new PackageStatus()
                {
                    Name = item.ToString(),
                };

                await context.PackageStatuses.AddAsync(status);
            }

            await context.SaveChangesAsync();

            var statuses = new List<PackageStatus>()
            {
                await context.PackageStatuses.FirstOrDefaultAsync(ps =>
                    ps.Name.Equals(ShipmentStatus.Pending.ToString())),

                await context.PackageStatuses.FirstOrDefaultAsync(ps =>
                    ps.Name.Equals(ShipmentStatus.Delivered.ToString())),

                await context.PackageStatuses.FirstOrDefaultAsync(ps =>
                    ps.Name.Equals(ShipmentStatus.Shipped.ToString())),
            };

            var packagesInTheSystem = 23;

            for (var i = 0; i < packagesInTheSystem; i++)
            {
                var package = new Package()
                {
                    Description = "Description",
                    EstimatedDeliveryDate = DateTime.UtcNow,
                    RecipientId = "Recipient Id",
                    ShippingAddress = "Shipping address",
                    Status = statuses[i % 3],
                    Weight = 0.1D,
                };

                await context.AddAsync(package);
            }

            await context.SaveChangesAsync();

            var acquiredStatus =
                await context.PackageStatuses.FirstOrDefaultAsync(ps =>
                    ps.Name.Equals(ShipmentStatus.Acquired.ToString()));

            for (var i = 0; i < 222; i++)
            {
                var package = new Package()
                {
                    Description = "Description",
                    EstimatedDeliveryDate = DateTime.UtcNow,
                    RecipientId = "Recipient Id",
                    ShippingAddress = "Shipping address",
                    Status = acquiredStatus,
                    Weight = 0.1D,
                };

                await context.AddAsync(package);
            }

            await context.SaveChangesAsync();

            var statusesService = new Mock<IStatusesService>();
            var packagesService = new PackagesService(context, statusesService.Object);

            var packages = (await  packagesService
                .GetAllPackagesInTheSystemAsync<PackageDetailsViewModel>()).ToList();

            Assert.Equal(packagesInTheSystem, packages.Count);
        }

        [Fact]
        public async Task GetAllUserPackagesAsync_ShouldReturnAllOwnedOfTheUserPackages()
        {
            using var context = new PandaDbContext(CreateNewContextOptions());
            RegisterAutoMappings();

            for (var i = 0; i < 5; i++)
            {
                var package = new Package()
                {
                    Description = "Description",
                    EstimatedDeliveryDate = DateTime.UtcNow,
                    RecipientId = $"Recipient Id {i}",
                    ShippingAddress = "Shipping address",
                    StatusId = "StatusId",
                    Weight = 0.1D,
                };

                await context.AddAsync(package);
            }

            await context.SaveChangesAsync();

            var givenRecipientPackagesCount = 3;
            var givenRecipientId = "xxx";

            for (var i = 0; i < givenRecipientPackagesCount; i++)
            {
                var package = new Package()
                {
                    Description = "Description",
                    EstimatedDeliveryDate = DateTime.UtcNow,
                    RecipientId = givenRecipientId,
                    ShippingAddress = "Shipping address",
                    StatusId = "StatusId",
                    Weight = 0.1D,
                };

                await context.AddAsync(package);
            }

            await context.SaveChangesAsync();

            var statusesService = new Mock<IStatusesService>();
            var packagesService = new PackagesService(context, statusesService.Object);

            var packages = (await  packagesService
                .GetAllUserPackagesAsync<PackageViewModel>(givenRecipientId)).ToList();

            Assert.Equal(givenRecipientPackagesCount, packages.Count);
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
