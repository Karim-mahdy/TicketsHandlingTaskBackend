using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Common.Abstraction.Factory
{
    public interface ITicketStatusStrategy
    {
        void UpdateStatus(Ticket ticket, TimeSpan timeElapsed);
    }
}
