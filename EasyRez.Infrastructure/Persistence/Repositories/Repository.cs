
using EasyRez.Application.Common.Interfaces.Services;
using EasyRez.Core;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace EasyRez.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
         where TEntity : Entity<TId>
         where TId : notnull
    {
        protected readonly EasyRezDbContext _dbContext;
        private readonly ISpecificationEvaluator _specificationEvaluator;

        public Repository(EasyRezDbContext dbContext)
        {
            _dbContext = dbContext;
            _specificationEvaluator = SpecificationEvaluator.Default;
        }

        public virtual async Task<TEntity> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

            return entity;
        }

        public virtual async Task<List<TEntity>> AddRangeAsync(
            List<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

            return entities;
        }

        public virtual async Task DeleteAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task DeleteRangeAsync(
            List<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public virtual async Task<List<TEntity>> ListAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> ListAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

            return specification.PostProcessingAction == null
                ? queryResult
                : specification.PostProcessingAction(queryResult).ToList();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification<TEntity>, ISpecification<TEntity>
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TResult?> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);

            return Task.FromResult(entities);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification, true).CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().CountAsync(cancellationToken);
        }

        protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification, evaluateCriteriaOnly);
        }
        protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException("Specification is required");
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return _specificationEvaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
        }


    }
}
