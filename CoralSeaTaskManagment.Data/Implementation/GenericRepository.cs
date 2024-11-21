using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoralSeaTaskManagment.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            //Categories.Add(Category)
            _dbSet.Add(entity);
        }

        
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, string? Includeword = null)
        {
            IQueryable<T> query = _dbSet;

            await Task.Run(() =>
              {
                  
                  if (predicate != null)
                  {
                      query = query.Where(predicate);
                  }
                  if (Includeword != null)
                  {
                      //_context.Category.Include("product,users,family")
                      foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                      {
                          query = query.Include(item);
                      }
                  }
                  return   query.ToListAsync();
              });
            return await query.ToListAsync() ;
             

        }

        public T GetFirstorDefault(Expression<Func<T, bool>>? predicate = null, string? Includeword = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (Includeword != null)
            {
                //_context.Category.Include("product,users,family")
                foreach (var item in Includeword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.DefaultIfEmpty().SingleOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
