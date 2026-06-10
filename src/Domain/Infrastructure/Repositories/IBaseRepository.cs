using Domain.Common;
using Domain.Entities;

namespace Domain.Infrastructure.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class, IId
    {
        Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken);
        Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedResult<TEntity>> GetAsync(PagedRequest pagedRequest, CancellationToken cancellationToken);

        abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
