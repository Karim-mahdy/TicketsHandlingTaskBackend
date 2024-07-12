using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Factory;
using TicketsHandling.Application.Common.Constents;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Persistence.BackgroundJobs
{
    public class RedStatusStrategy : ITicketStatusStrategy
    {
        public void UpdateStatus(Ticket ticket, TimeSpan timeElapsed)
        {
            ticket.IsHandled = true;
            ticket.StatusColor = StatusColor.Red.ToString();
        }
    }

    public class BlueStatusStrategy : ITicketStatusStrategy
    {
        public void UpdateStatus(Ticket ticket, TimeSpan timeElapsed)
        {
            ticket.StatusColor = StatusColor.Blue.ToString();
        }
    }

    public class GreenStatusStrategy : ITicketStatusStrategy
    {
        public void UpdateStatus(Ticket ticket, TimeSpan timeElapsed)
        {
            ticket.StatusColor = StatusColor.Green.ToString();
        }
    }

    public class YellowStatusStrategy : ITicketStatusStrategy
    {
        public void UpdateStatus(Ticket ticket, TimeSpan timeElapsed)
        {
            ticket.StatusColor = StatusColor.Yellow.ToString();
        }
    }

    public static class TicketStatusStrategyFactory
    {
        public static ITicketStatusStrategy GetStrategy(TimeSpan timeElapsed)
        {
            if (timeElapsed.TotalMinutes >= 60)
            {
                return new RedStatusStrategy();  
            }
            else if (timeElapsed.TotalMinutes >= 45)
            {
                return new BlueStatusStrategy();
            }
            else if (timeElapsed.TotalMinutes >= 30)
            {
                return new GreenStatusStrategy();
            }
            else if (timeElapsed.TotalMinutes >= 15)
            {
                return new YellowStatusStrategy();
            }
            else
            {
                return null; // No status change needed
            }
        }
    }

}
