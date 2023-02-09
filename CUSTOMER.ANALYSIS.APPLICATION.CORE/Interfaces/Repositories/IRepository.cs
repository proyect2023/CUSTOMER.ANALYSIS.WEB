using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(int id);
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> ListAsync();
        void AddAsync(TEntity entity);
        int Save();
    }
}
