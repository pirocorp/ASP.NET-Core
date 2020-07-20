namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Services.Mapping;

    public abstract class DeletableEntityService<TEntity> : IDeletableEntityService<TEntity>
        where TEntity : class, IDeletableEntity
    {
        private readonly IDeletableEntityRepository<TEntity> entityRepository;

        protected DeletableEntityService(IDeletableEntityRepository<TEntity> entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public IEnumerable<TOut> GetAll<TOut, TOrder>(
            int? count,
            Expression<Func<TEntity, TOrder>> order = null)
            where TOrder : IComparable<TOrder>
        {
            var query = this.entityRepository
                .All();

            if (order != null)
            {
                query = query
                    .OrderBy(order);
            }

            if (count.HasValue)
            {
                query = query
                    .Take(count.Value);
            }

            return query
                .To<TOut>()
                .ToList();
        }
    }
}
