using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Features.Tickets.Command.CreateTicket
{
    public class CreateTicketCommand : IRequest<Response<string>>
    {
        public string PhoneNumber { get; set; }
        public int Governorate { get; set; }
        public int City { get; set; }
        public int District { get; set; }
    }

    public class CreateTicketCommandHandler : ResponseHandler,
        IRequestHandler<CreateTicketCommand, Response<string>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public CreateTicketCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                CreationDateTime = DateTime.Now,
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                District = request.District
            };

            await _unitOfWork.TicketRepository.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();

            return Created("Created Successfully");
        }
    }
}
