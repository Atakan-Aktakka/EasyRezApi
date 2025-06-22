using Ardalis.Specification;
using EasyRez.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRez.Application.Common.Interfaces.Services
{
    public interface IRepository<TEntity, TId>
     where TEntity : Entity<TId>
     where TId : notnull
    {
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<TEntity?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification<TEntity>, ISpecification<TEntity>;
        Task<TResult?> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);
        Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<List<TEntity>> AddRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);

    }
}
