using Microsoft.EntityFrameworkCore;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Specificatios;
using System.Linq;

namespace microStore.Services.ProductApi.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // Modify the queryable entity (inputQuery) using the spec's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Aggregate any Includes
            query = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            query = specification.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            query = specification.ThenIncludes
                  .Aggregate(query, (current, include) => include(current));

            // Ordering
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply paging if required
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                            .Take(specification.Take);
            }

            return query;
        }
    }
}
