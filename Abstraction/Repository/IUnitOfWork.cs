using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
    }
}
