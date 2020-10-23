namespace Panda.Services
{
    using System.Linq;
    using Data;

    public class PackageStatusService : IPackageStatusService
    {
        private readonly PandaDbContext pandaDb;

        public PackageStatusService(PandaDbContext pandaDb)
        {
            this.pandaDb = pandaDb;
        }

        public string GetPackageStatusIdByName(string name)
        {
            if (name is null)
            {
                return string.Empty;
            }

            var status = this.pandaDb
                .PackageStatuses
                .FirstOrDefault(ps => ps.Name.Equals(name));

            return status is null ? string.Empty : status.Id;
        }
    }
}
