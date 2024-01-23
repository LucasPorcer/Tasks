using Microsoft.EntityFrameworkCore;
using Tasks.API.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tasks.API.InfraData.Database.Tasks;

namespace Tasks.API.InfraData.Repositories
{
    public class DatabaseRepositoryBase<TEntity> : IDatabaseRepositoryBase<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;
        private TasksDbContext _context;

        public DatabaseRepositoryBase(TasksDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            var entity = _dbSet.Add(obj).Entity;
            _context.SaveChanges();

            return entity;
        }

        public void Add(IEnumerable<TEntity> objs)
        {
            _dbSet.AddRange(objs);
            _context.SaveChanges();
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Remove(TEntity obj)
        {
            var entity =  _dbSet.Remove(obj).Entity;
            _context.SaveChanges();

            return entity;
        }

        public TEntity Remove(long id)
        {
            TEntity obj = _dbSet.Find(id);
            var entity = _dbSet.Remove(obj).Entity;

            _context.SaveChanges();

            return entity;
        }

        public void Remove(IEnumerable<TEntity> objs)
        {
            _dbSet.RemoveRange(objs);
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);
            _context.SaveChanges();
        }
    }
}
