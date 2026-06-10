using Domain.Common;
using Domain.Entities;
using Domain.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity>(DatabaseContext databaseContext) : IBaseRepository<TEntity>
        where TEntity : class, IId
    {
        public async Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await databaseContext.AddAsync(entity, cancellationToken);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await databaseContext.FindAsync<TEntity>(id, cancellationToken);
        }

        public async Task<Domain.Common.PagedResult<TEntity>> GetAsync(PagedRequest pagedRequest, CancellationToken cancellationToken)
        {
            var query = databaseContext.Set<TEntity>().AsQueryable();

            query = FilterBy(pagedRequest, query);
            query = SortBy(pagedRequest, query);
            query = Pagination(pagedRequest, query);

            var items = await query.ToListAsync(cancellationToken);

            return new Domain.Common.PagedResult<TEntity>()
            {
                Items = items,
                PageNumber = pagedRequest.PageNumber,
                PageSize = pagedRequest.PageSize,
            };
        }

        private IQueryable<TEntity> FilterBy(PagedRequest pagedRequest, IQueryable<TEntity> query)
        {
            foreach (var filter in pagedRequest.FilterBy)
            {
                query = query.Where($"{filter.Key} == @0", filter.Value);
            }

            return query;
        }

        private IQueryable<TEntity> SortBy(PagedRequest pagedRequest, IQueryable<TEntity> query)
        {
            var sortBy = pagedRequest.SortBy ?? "Id";
            string sortDirection = pagedRequest.IsDescending ? "descending" : "ascending";

            string ordering = $"{sortBy} {sortDirection}";
            return query.OrderBy(ordering);
        }

        private IQueryable<TEntity> Pagination(PagedRequest pagedRequest, IQueryable<TEntity> query)
        {
            return query.Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize)
                                   .Take(pagedRequest.PageSize);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var entityToRemove = await GetAsync(id, cancellationToken);
            databaseContext.Remove(entityToRemove);
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
