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

namespace TicketsHandling.Application.Features.Tickets.Command.DeleteTicket
{

    public class DeleteTicketCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteTicketCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteTicketCommandHandler : ResponseHandler,
       IRequestHandler<DeleteTicketCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<TicketHub> _hubContext;

        public DeleteTicketCommandHandler(IUnitOfWork unitOfWork, IHubContext<TicketHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }
        public async Task<Response<string>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);

            if (ticket == null)
                return NotFound<string>("Ticket Not Found");

            _unitOfWork.TicketRepository.Remove(ticket);
            await _unitOfWork.CompleteAsync();

            return Deleted<string>();

        }
    }
}
