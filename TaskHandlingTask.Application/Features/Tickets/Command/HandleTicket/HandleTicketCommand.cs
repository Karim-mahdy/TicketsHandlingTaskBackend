using Azure;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Application.Hubs;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Features.Tickets.Command.HandleTicket
{

    public class HandleTicketCommand : IRequest<Common.SharedModels.Response<string>>
    {
        public int Id { get; set; }

        public HandleTicketCommand(int id)
        {
            Id = id;
        }
    }
    public class HandleTicketCommandHandler : ResponseHandler,
       IRequestHandler<HandleTicketCommand, Common.SharedModels.Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<TicketHub> _hubContext;

        public HandleTicketCommandHandler(IUnitOfWork unitOfWork, IHubContext<TicketHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<Common.SharedModels.Response<string>> Handle(HandleTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);

            if (ticket == null)
                return NotFound<string>("Ticket Not Found");

            ticket.IsHandled = true;

            _unitOfWork.TicketRepository.Update(ticket);
            await _unitOfWork.CompleteAsync();

            await _hubContext.Clients.All.SendAsync("HandleTicket", ticket);

            return Success("Ticket handled successfully");
        }
    }
}