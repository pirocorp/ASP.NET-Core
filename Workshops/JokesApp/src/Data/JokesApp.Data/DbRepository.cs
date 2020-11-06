namespace JokesApp.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.EntityFrameworkCore;

    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        public DbRepository(JokesAppDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected JokesAppDbContext Context { get; set; }

        public IQueryable<TEntity> All() => this.DbSet;

        /// <summary>
        /// Add entity to repository
        /// </summary>
        /// <remarks>The result of method is not awaited, because method just passes the result up.</remarks>
        /// <param name="entity">Entity to be added to repository</param>
        public Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        /// <summary>
        /// Saves changes in repository
        /// </summary>
        /// <remarks>The result of method is not awaited, because method just passes the result up.</remarks>
        public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            this.Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Request manual disposing of the repository
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements manual disposing of repository 
        /// </summary>
        /// <param name="disposing">Condition</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
