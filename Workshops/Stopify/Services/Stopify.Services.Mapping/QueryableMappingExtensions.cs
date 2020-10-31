﻿namespace Stopify.Services.Mapping
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper.QueryableExtensions;

    /// <summary>
    /// Extension methods for easy using of automapper.
    /// </summary>
    public static class QueryableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            object parameters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters);
        }

        public static TDestination To<TDestination>(
            this object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AutoMapperConfig.MapperInstance.Map<TDestination>(source);
        }
    }
}
