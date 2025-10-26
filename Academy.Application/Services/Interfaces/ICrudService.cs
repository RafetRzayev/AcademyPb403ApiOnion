using Academy.Application.Dtos.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface ICrudServiceAsync<TDto, TCreateDto, TUpdateDto, TEntity>
    {
        Task<IList<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes);
        Task<PageResultDto<TDto>> GetAllWithPaginationAsync(Expression<Func<TEntity, bool>>? predicate = null, int page = 1, int size = 10, params string[] includes);
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto?> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
        Task<TDto?> CreateAsync(TCreateDto entity);
        Task<TDto?> UpdateAsync(int id, TUpdateDto entity);
        Task DeleteAsync(int id);
    }
}
