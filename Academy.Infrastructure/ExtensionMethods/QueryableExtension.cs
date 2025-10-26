using Academy.Domain.Entities;
using Academy.Domain.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Academy.Infrastructure.ExtensionMethods
{
    internal static class QueryableExtension
    {
        internal static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, params string[] includes) where T : Entity
        {
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    if (!string.IsNullOrWhiteSpace(include))
                    {
                        query = query.Include(include);
                    }
                }
            }

            return query;
        }

        internal static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize) where T : Entity
        {
            var totalCount = await query.CountAsync();
            var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return new PagedResult<T>
            {
                Data = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

    }
}
