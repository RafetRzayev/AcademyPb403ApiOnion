using Academy.Domain.Entities;
using Academy.Domain.Pagination;
using Academy.Domain.Repositories;
using Academy.Infrastructure.DataContext;
using Academy.Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Academy.Infrastructure.Repositories;

public class EfCoreRepositoryAsync<T> : IRepositoryAsync<T> where T : Entity
{
    protected readonly AppDbContext DbContext;

    public EfCoreRepositoryAsync(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<T?> CreateAsync(T entity)
    {
        var entityEntry = await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var existEntity = await DbContext.Set<T>().FindAsync(id);

        if (existEntity == null) throw new Exception($"Entity not found with given key : {id}");

        DbContext.Set<T>().Remove(existEntity);

        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params string[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();

        if (predicate != null)
            query = query.Where(predicate);

        query = query.ApplyIncludes(includes);

        var result = await query.ToListAsync();

        return result;
    }

    public virtual async Task<PagedResult<T>> GetAllWithPaginationAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int size = 10, params string[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();

        if (predicate != null)
            query = query.Where(predicate);

        query = query.ApplyIncludes(includes);

        var result = await query.ToPagedResultAsync(page, size);

        return result;
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params string[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();

        query = query.ApplyIncludes(includes);

        var result = await query.FirstOrDefaultAsync(predicate);

        return result;
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var result = await DbContext.Set<T>().FindAsync(id);

        return result;
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        var entityEntry = DbContext.Set<T>().Update(entity);

        await DbContext.SaveChangesAsync();

        return entityEntry.Entity;
    }
}
