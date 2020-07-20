namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using ForumSystem.Data.Common.Models;

    public interface IDeletableEntityService<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IEnumerable<TOut> GetAll<TOut, TOrder>(int? count, Expression<Func<TEntity, TOrder>> order = null)
            where TOrder : IComparable<TOrder>;
    }
}
