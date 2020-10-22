namespace Panda.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Panda.Models;

    public class PackageStatusSeeder : ISeeder
    {
        public async Task SeedAsync(PandaDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.PackageStatuses.Any())
            {
                var statuses = new List<PackageStatus>()
                {
                    new PackageStatus() { Name = "Pending" },
                    new PackageStatus() { Name = "Shipped" },
                    new PackageStatus() { Name = "Delivered" },
                    new PackageStatus() { Name = "Acquired" },
                };

                await dbContext.AddRangeAsync(statuses);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
