using Academy.Domain.Entities;
using Academy.Domain.Pagination;
using System.Linq.Expressions;

namespace Academy.Domain.Repositories;

public interface IRepositoryAsync<T> where T : Entity
{
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params string[] includes);
    Task<PagedResult<T>> GetAllWithPaginationAsync(Expression<Func<T, bool>>? predicate = null, int page = 1, int size = 10, params string[] includes);
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, params string[] includes);
    Task<T?> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task DeleteAsync(int id);
}