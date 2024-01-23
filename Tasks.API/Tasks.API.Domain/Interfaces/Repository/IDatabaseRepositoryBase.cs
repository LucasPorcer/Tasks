using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tasks.API.Domain.Interfaces.Repository
{
    public interface IDatabaseRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity obj);
        void Add(IEnumerable<TEntity> objs);
        TEntity GetById(Guid id);
        IList<TEntity> GetAll();
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Remove(TEntity obj);
        TEntity Remove(long id);
        void Remove(IEnumerable<TEntity> objs);

        void Update(TEntity obj);
    }
}
