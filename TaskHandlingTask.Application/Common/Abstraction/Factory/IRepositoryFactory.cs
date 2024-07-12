using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Repositories;

namespace TicketsHandling.Application.Common.Abstraction.Factory
{
    public interface IRepositoryFactory
    {
        IBaseRepository<T> GetRepository<T>() where T : class;
    }
}
