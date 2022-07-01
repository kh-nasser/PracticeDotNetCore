using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositoryPattern_DataLayer.Repository
{
    public interface IMyGenericRepository<TEntity> where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> conditionWhere = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includesRelatioship = null);
        TEntity GetById(object id, bool AsNoTracking = false);
        void Insert(TEntity entity);
        int Save();
        void Update(TEntity entity);
    }
}
