using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Persistence.Repository
{
    public class TicketsRpostitory : BaseRepository<Ticket>, ITicketsRpostitory
    {
        public TicketsRpostitory(ApplicationDbContext context) : base(context)
        {
        }
    }
}
