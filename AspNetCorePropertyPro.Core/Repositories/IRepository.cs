using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetById(object id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleorDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        void RemoveAsync(TEntity entity);
        void RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}
