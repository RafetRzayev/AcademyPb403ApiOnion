using Academy.Application.Dtos.Pagination;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities;
using Academy.Domain.Pagination;
using Academy.Domain.Repositories;
using AutoMapper;
using System.Linq.Expressions;

namespace Academy.Application.Services.Managers;

public class CrudManager<TDto, TCreateDto, TUpdateDto, TEntity> : ICrudServiceAsync<TDto, TCreateDto, TUpdateDto, TEntity>
    where TEntity : Entity
{
    protected readonly IRepositoryAsync<TEntity> RepositoryAsync;
    protected readonly IMapper Mapper;

    public CrudManager(IRepositoryAsync<TEntity> repositoryAsync, IMapper mapper)
    {
        RepositoryAsync = repositoryAsync;
        Mapper = mapper;
    }

    public virtual async Task<TDto?> CreateAsync(TCreateDto createDto)
    {
        var mappedEntity = Mapper.Map<TCreateDto, TEntity>(createDto);

        var createdEntity = await RepositoryAsync.CreateAsync(mappedEntity);

        if (createdEntity == null) throw new Exception("Cannot create entity");

        var result = Mapper.Map<TEntity, TDto>(createdEntity);

        return result;
    }

    public virtual async Task DeleteAsync(int id)
    {
        await RepositoryAsync.DeleteAsync(id);
    }

    public virtual async Task<IList<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes)
    {
        var entities = await RepositoryAsync.GetAllAsync(predicate, includes);

        var dtos = Mapper.Map<IList<TEntity>, IList<TDto>>(entities);

        return dtos;
    }

    public virtual async Task<PageResultDto<TDto>> GetAllWithPaginationAsync(Expression<Func<TEntity, bool>>? predicate = null, int page = 1, int size = 10, params string[] includes)
    {
        var entities = await RepositoryAsync.GetAllWithPaginationAsync(predicate, page, size, includes);

        var dtos = Mapper.Map<PagedResult<TEntity>, PageResultDto<TDto>>(entities);

        return dtos;
    }

    public virtual async Task<TDto?> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        var entity = await RepositoryAsync.GetAsync(predicate, includes);

        if (entity == null) throw new Exception("Not found");

        var dto = Mapper.Map<TEntity, TDto>(entity);

        return dto;
    }

    public virtual async Task<TDto?> GetByIdAsync(int id)
    {
        var entity = await RepositoryAsync.GetByIdAsync(id);

        if (entity == null) throw new Exception("Not found");

        var dto = Mapper.Map<TEntity, TDto>(entity);

        return dto;
    }

    public async Task<TDto?> UpdateAsync(int id, TUpdateDto updateDto)
    {
        var entity = await RepositoryAsync.GetByIdAsync(id);

        if (entity == null) throw new Exception("Not found");

        entity = Mapper.Map(updateDto, entity);

        var updatedEntity = await RepositoryAsync.UpdateAsync(entity);

        if (updatedEntity == null) throw new Exception("Cannot updated");

        var result = Mapper.Map<TEntity, TDto>(updatedEntity);

        return result;
    }
}
