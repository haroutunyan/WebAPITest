using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public IQueryable<TEntity> DbSet { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
