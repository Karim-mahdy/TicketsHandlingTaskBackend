using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Common.Abstraction.Repositories
{
    public interface ITicketsRpostitory : IBaseRepository<Ticket>
    {
    }
}
