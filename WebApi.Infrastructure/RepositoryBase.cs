using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.ApiData;

namespace WebApi.Infrastructure
{
    public abstract class RepositoryBase<T>
         : IRepository<T>
         where T : class
    {
        private ApplicationDbContext _dataContext;
        private DbSet<T> _dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }
        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }
        protected ApplicationDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }
        public virtual IQueryable<T> GetAll()
        {
            return _dbset.AsQueryable<T>();
        }
        public virtual T GetById(Guid id)
        {
            return _dbset.Find(id);
        }
        public virtual IQueryable<T> GetAllWithNavigation(string[] children)
        {
            if (children != null)
            {
                foreach (var child in children)
                {
                    _dbset.Include(child);
                }
            }
            return _dbset;
        }
        public IQueryable<T> GetAllWithNavigation(string[] children, Expression<Func<T, bool>> where)
        {
            if (children != null)
            {
                foreach (var child in children)
                {
                    _dbset.Include(child);
                }
            }
            return _dbset.Where(where);
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).AsQueryable<T>();
        }
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }
        public virtual void Delete(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Deleted;
        }
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Save(T entity)
        {
            _dbset.Add(entity);
        }
        public async Task DeleteAsync(Guid Id)
        {
            var entity = await _dbset.FindAsync(Id);
            _dbset.Remove(entity);
        }
        public async Task DeleteAsync(System.Linq.Expressions.Expression<Func<T, bool>> match)
        {
            var entities = await _dbset.Where(match).ToListAsync();
            foreach (var entity in entities)
            {
                _dbset.Remove(entity);
            }
        }
        public virtual IEnumerable<T> GetFiltered(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }
        public virtual async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> where)
        {
            return await Task.FromResult<IEnumerable<T>>(_dbset.Where(where).ToList());
        }
        public virtual IQueryable<T> GetAllQueryable()
        {
            return _dbset.AsQueryable<T>();
        }

        public void Delete(Guid id)
        {
            var entity = _dbset.Find(id);
            _dbset.Remove(entity);
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return _dbset;
            }
        }
        public virtual IQueryable<T> TableAsNoTracking
        {
            get
            {
                return _dbset.AsNoTracking();
            }
        }
    }
}
