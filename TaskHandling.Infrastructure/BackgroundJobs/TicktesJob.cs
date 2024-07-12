using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Factory;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Application.Hubs;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Persistence.BackgroundJobs
{


    public class TicktesJob
    {
        private readonly IHubContext<TicketHub> _hubContext;
        public readonly IUnitOfWork unitOfWork;
        public TicktesJob(IHubContext<TicketHub> hubContext , IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            this.unitOfWork = unitOfWork;
        }

        [AutomaticRetry(Attempts = 0)]
        public async Task UpdateTicketStatuses()
       {
            var tickets = await unitOfWork.TicketRepository.GetAllAsync();
            var currentTime = DateTime.Now;

            foreach (var ticket in tickets)
            {
                // Calculate Elapsed Time for each ticket based on creation time
                var timeElapsed = currentTime - ticket.CreationDateTime;

                if (ticket.IsHandled)
                    continue;

                // Get the corresponding strategy based on the elapsed time
                var strategy = TicketStatusStrategyFactory.GetStrategy(timeElapsed);

                if (strategy != null)
                {
                    strategy.UpdateStatus(ticket, timeElapsed);
                    unitOfWork.TicketRepository.Update(ticket);
                    await unitOfWork.CompleteAsync();
                    // Update the ticket status in the SignalR hub
                    await _hubContext.Clients.All.SendAsync("UpdateTicketStatus", ticket.Id, ticket.StatusColor);
                    if (strategy is RedStatusStrategy)
                    {
                        await _hubContext.Clients.All.SendAsync("HandleTicket", ticket);

                    }
                }
            }
        }
    }
}
