using Abstraction.Repository;
using Domain.Common;
using Infrastructure.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly AppContext _context;
        readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(AppContext context) => _context = context;

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;

            IRepository<TEntity> repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
    }
}
