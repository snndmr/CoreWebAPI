using Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context) => _context = context;

        public IQueryable<T> FindAll(bool trackChanges) => trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => trackChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}
