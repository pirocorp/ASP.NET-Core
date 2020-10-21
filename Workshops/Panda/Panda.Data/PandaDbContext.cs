namespace Panda.Data
{
    using Microsoft.EntityFrameworkCore;

    public class PandaDbContext : DbContext
    {
        public PandaDbContext(DbContextOptions<PandaDbContext> options)
            : base(options)
        {
        }
    }
}
