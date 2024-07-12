using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Factory;
using TicketsHandling.Application.Common.Abstraction.Repositories;

namespace TicketsHandling.Persistence.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationDbContext _context;

        public RepositoryFactory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            return new BaseRepository<T>(_context);
        }
    }

}
