using Abstraction.Repository;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositoy
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        readonly AppContext _context;
        public Repository(AppContext context)
        {
            _context = context;
        }
        public IQueryable<TEntity> DbSet => _context.Set<TEntity>();

        public TEntity Add(TEntity entity) => _context.Set<TEntity>().Add(entity).Entity;

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) => DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

        public void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<TEntity>().Attach(entity);
            _context.Set<TEntity>().Update(entity);
        }
    }
}
