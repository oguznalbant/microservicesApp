using ECom.Order.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECom.Order.Core.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        //IReadOnlyList purpose is only reading.Its base of IList
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        // Get method with order by ,includes and tracking options
        Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeString = null,
            bool disableTracking = true);

        Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            bool disableTracking = true);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task Delete(TEntity entity);
    }
}
