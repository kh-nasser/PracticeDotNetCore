using Microsoft.EntityFrameworkCore;
using ProjectRepositoryPattern_DataLayer.Context;
using ProjectRepositoryPattern_DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositoryPattern_DataLayer.Services
{
    public class MyGenericRepository<TEntity> : IMyGenericRepository<TEntity> where TEntity : class
    {
        private ProjectRepositoryPatternContext _context;//save
        private DbSet<TEntity> _dbSet;//crud

        public MyGenericRepository(ProjectRepositoryPatternContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> conditionWhere = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includesRelatioship = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (conditionWhere != null)
            {
                query = query.Where(conditionWhere);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includesRelatioship != null)
            {
                foreach (var include in includesRelatioship)
                {
                    query = query.Include(include);
                }
            }
            
            return query;
        }

        public virtual TEntity GetById(object id, bool AsNoTracking = false)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public virtual int Save()
        {
            var affectedRow = _context.SaveChanges();
            return affectedRow;
        }
    }
}
