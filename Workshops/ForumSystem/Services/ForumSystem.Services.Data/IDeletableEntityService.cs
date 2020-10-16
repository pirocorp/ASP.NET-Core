namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using ForumSystem.Data.Common.Models;

    public interface IDeletableEntityService<TEntity, in TKey>
        where TEntity : BaseDeletableModel<TKey>, IDeletableEntity
    {
        IEnumerable<TOut> GetAll<TOut>();

        IEnumerable<TOut> GetAll<TOut>(int count);

        IEnumerable<TOut> GetAll<TOut>(
            Expression<Func<TEntity,
                bool>> filter);

        IEnumerable<TOut> GetAll<TOut>(
            int count,
            Expression<Func<TEntity,
                bool>> filter);

        IEnumerable<TOut> GetAll<TOut, TOrder>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrder>> order)
            where TOrder : IComparable<TOrder>;

        IEnumerable<TOut> GetAll<TOut, TOrder>(
            int? count,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrder>> order)
            where TOrder : IComparable<TOrder>;

        bool Exists(TKey key);
    }
}
