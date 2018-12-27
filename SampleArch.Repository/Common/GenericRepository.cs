using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model;

namespace SampleArch.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
       where T : BaseEntity
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {

            return _dbset.AsEnumerable<T>();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = null;
            if (predicate != null)
            {
                query = _dbset.Where(predicate).AsEnumerable<T>();
            }
            else
            {
                query = _dbset.AsEnumerable();
            }
                
            return query;
        }

        public IEnumerable<T> FindByWithInclude(string includings, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = null;
            if (predicate != null)
            {
                query = _dbset.Include(includings).Where(predicate).AsEnumerable<T>();
            }
            else
            {
                query = _dbset.Include(includings).AsEnumerable();
            }

            return query;
        }

        public IEnumerable<object> ListByFilter(int languagID, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public IEnumerable<object> ListByFilter(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual T Get(object PrimaryKeyValue)
        {

            return _dbset.Find(PrimaryKeyValue);
        }

        public virtual T GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
               return _dbset.FirstOrDefault();
            }

            T query = _dbset.Where(predicate).FirstOrDefault();
            return query;
        }

        public IEnumerable<T> GetAllWithInclude(string includings)
        {

            return _dbset.Include(includings).AsEnumerable<T>();
        }

        public T GetWithInclude(object v, string includings)
        {
           return _dbset.Include(includings).FirstOrDefault();
        }

        public T GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate, string includings)
        {
            throw new NotImplementedException();
        }


       
    }
}
