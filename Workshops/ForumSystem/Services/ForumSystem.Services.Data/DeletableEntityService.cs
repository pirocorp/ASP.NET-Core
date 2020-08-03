﻿namespace ForumSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using ForumSystem.Data.Common.Models;
    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Services.Mapping;

    /// <summary>
    /// Base service class for all entities supporting soft delete.
    /// </summary>
    /// <typeparam name="TEntity">Entity(Domain model).</typeparam>
    public abstract class DeletableEntityService<TEntity> : IDeletableEntityService<TEntity>
        where TEntity : class, IDeletableEntity
    {
        protected DeletableEntityService(IDeletableEntityRepository<TEntity> entityRepository)
        {
            this.EntityRepository = entityRepository;
        }

        protected IDeletableEntityRepository<TEntity> EntityRepository { get; }

        /// <summary>
        /// Project data from database to desired TOut model.
        /// </summary>
        /// <typeparam name="TOut">Desired model.</typeparam>
        /// <returns>Projected elements. IEnumerable&lt;TOut&gt;.</returns>
        public IEnumerable<TOut> GetAll<TOut>()
        {
            return this.GetAll<TOut, string>(null, null, null);
        }

        /// <summary>
        /// Project data from database to desired TOut model.
        /// </summary>
        /// <typeparam name="TOut">Desired model.</typeparam>
        /// <param name="filter">Function expression returning bool.</param>
        /// <returns>Projected elements. IEnumerable&lt;TOut&gt;.</returns>
        public IEnumerable<TOut> GetAll<TOut>(
            Expression<Func<TEntity,
                bool>> filter)
        {
            return this.GetAll<TOut, string>(null, filter, null);
        }

        /// <summary>
        /// Project data from database to desired TOut model.
        /// </summary>
        /// <typeparam name="TOut">Desired model.</typeparam>
        /// <typeparam name="TOrder">The collection will be ordered by this type.
        /// TOrder must implement IComparable&lt;TOrder&gt;.
        /// </typeparam>
        /// <param name="filter">Function expression returning bool.</param>
        /// <param name="order">Function expression returning TOrder
        /// (type by witch collection will be ordered).
        /// </param>
        /// <returns>Projected elements. IEnumerable&lt;TOut&gt;.</returns>
        public IEnumerable<TOut> GetAll<TOut, TOrder>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrder>> order)
            where TOrder : IComparable<TOrder>
        {
            return this.GetAll<TOut, TOrder>(null, filter, order);
        }

        /// <summary>
        /// Project data from database to desired TOut model.
        /// </summary>
        /// <typeparam name="TOut">Desired model.</typeparam>
        /// <param name="count">Number of elements.</param>
        /// <returns>Projected elements. IEnumerable&lt;TOut&gt;.</returns>
        public IEnumerable<TOut> GetAll<TOut>(int count)
        {
            return this.GetAll<TOut, string>(count, null, null);
        }

        /// <summary>
        /// Project data from database to desired TOut model.
        /// </summary>
        /// <typeparam name="TOut">Desired model.</typeparam>
        /// <param name="count">Number of elements.</param>
        /// <param name="filter">Function expression returning bool.</param>
        /// <returns>Projected elements. IEnumerable&lt;TOut&gt;.</returns>
        public IEnumerable<TOut> GetAll<TOut>(
            int count,
            Expression<Func<TEntity,
                bool>> filter)
        {
            return this.GetAll<TOut, string>(count, filter, null);
        }

        /// <summary>
        /// Project data from database to desired TOut model.
        /// </summary>
        /// <typeparam name="TOut">Desired model.</typeparam>
        /// <typeparam name="TOrder">The collection will be ordered by this type.
        /// TOrder must implement IComparable&lt;TOrder&gt;.
        /// </typeparam>
        /// <param name="count">How many element to be returned.</param>
        /// <param name="filter">Function expression returning bool.</param>
        /// <param name="order">Function expression returning TOrder
        /// (type by witch collection will be ordered).
        /// </param>
        /// <returns>Projected elements. IEnumerable&lt;TOut&gt;.</returns>
        public IEnumerable<TOut> GetAll<TOut, TOrder>(
            int? count,
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TOrder>> order)
            where TOrder : IComparable<TOrder>
        {
            var query = this.EntityRepository
                .All();

            if (filter != null)
            {
                query = query
                    .Where(filter);
            }

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
