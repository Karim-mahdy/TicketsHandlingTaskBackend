using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Hubs
{
    public class TicketHub:Hub
    {
        // Method to send updates to clients
        public async Task SendTicketUpdate(Ticket ticket)
        {
            await Clients.All.SendAsync("UpdateTicketStatus", ticket);
        }
    }
}
