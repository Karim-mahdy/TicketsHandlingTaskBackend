using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TicketsHandling.Application.Common.Abstraction.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<T> GetFirstOrDefaultAsync(
             Expression<Func<T, bool>> filter = null!,
             string includeProperties = null!,
               Func<IQueryable<T>,
             IOrderedQueryable<T>> orderBy = null!
         );

        Task<IEnumerable<TType>> GetSpecificSelectAsync<TType>(
         Expression<Func<T, bool>> filter,
         Expression<Func<T, TType>> select,
         string includeProperties = null!,
         int? skip = null,
         int? take = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!
         ) where TType : class;

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null!,
             Expression<Func<T, IQueryable<T>>> select = null!,
             Expression<Func<T, T>> selector = null!,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, Expression<Func<T, bool>> includeFilter = null!,
             string includeProperties = null!,
             int? skip = null,
             int? take = null);

        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Remove(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
    }
}
