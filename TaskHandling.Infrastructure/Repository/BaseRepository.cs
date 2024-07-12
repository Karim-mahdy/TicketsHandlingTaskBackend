using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Repositories;

namespace TicketsHandling.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) =>
        (await dbSet.FindAsync(id))!;



        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null!,
              string includeProperties = null!
                , Func<IQueryable<T>,
              IOrderedQueryable<T>> orderBy = null!
              )
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty).AsSplitQuery().AsNoTracking();
            if (orderBy != null)
                return (await orderBy(query).FirstOrDefaultAsync())!;

            return (await query.FirstOrDefaultAsync())!;
        }

        public async Task<IEnumerable<TType>> GetSpecificSelectAsync<TType>(
         Expression<Func<T, bool>> filter,
         Expression<Func<T, TType>> select,
         string includeProperties = null!,
         int? skip = null,
         int? take = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!

         ) where TType : class
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (includeProperties != null)
            {
                query.AsSplitQuery();
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty).IgnoreQueryFilters();
            }

            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.Select(select).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null!,
        Expression<Func<T, IQueryable<T>>> select = null!,
        Expression<Func<T, T>> selector = null!,
        Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null!,
        Expression<Func<T, bool>> includeFilter = null!,
        string includeProperties = null!,
        int? skip = null,
        int? take = null)
        {
            IQueryable<T> query = dbSet;

            if (includeFilter is not null)
                query = query.Include(includeFilter);

            if (select != null)
                query = (IQueryable<T>)query.Select(select);
            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty).AsSplitQuery().AsNoTracking();

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null!, string includeProperties = null!)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return await query.CountAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            dbSet.Remove(entity);
            return entity;
        }
    }

}
