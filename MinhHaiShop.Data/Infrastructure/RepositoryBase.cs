using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MinhHaiShop.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private MinhHaiShopDbContext dataContext;

        protected RepositoryBase(MinhHaiShopDbContext context)
        {
            dataContext = context;
        }

        //auto generate
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        protected MinhHaiShopDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        public virtual void Add(T entity)
        {
            dataContext.Set<T>().Add(entity);
        }


        public virtual void Update(T entity)
        {
            dataContext.Set<T>().Attach(entity);
            dataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }
        public virtual void Delete(int? id)
        {
            var entity = dataContext.Set<T>().Find(id);
            dataContext.Set<T>().Remove(entity);
        }
        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dataContext.Set<T>().Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dataContext.Set<T>().Remove(obj);
        }
        public virtual void DeleteRange(List<T> obj)
        {
            dataContext.Set<T>().RemoveRange(obj);
        }
        public bool CheckUniqueName(Expression<Func<T, bool>> where)
        {
            //Check Unique Name of T

            return dataContext.Set<T>().Where(where).FirstOrDefault() == null ? true : false;
        }
        public virtual void AddMulti(IEnumerable<T> entity)
        {
            dataContext.Set<T>().AddRange(entity);
        }
        public virtual T GetSingleById(int? id)
        {
            return dataContext.Set<T>().Find(id);
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return dataContext.Set<T>().Where(where).ToList();
        }
        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dataContext.Set<T>().Count(where);
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return dataContext.Set<T>().AsQueryable();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return dataContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return dataContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dataContext.Set<T>().Where<T>(predicate).AsQueryable() : dataContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dataContext.Set<T>().Count<T>(predicate) > 0;
        }
        public void Commit() { dataContext.SaveChangesAsync(); }

        public virtual T Create(T entity)
        {
            return dataContext.Set<T>().Add(entity).Entity;
        }
    }
}
