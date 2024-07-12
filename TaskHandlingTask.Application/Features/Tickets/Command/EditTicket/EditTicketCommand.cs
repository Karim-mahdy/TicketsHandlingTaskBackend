using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Features.Tickets.Command.EditTicket
{
    public record EditTicketCommand(EditTicketCommandDto Ticket)
                : IRequest<Response<string>>;

    // Dto
    public class EditTicketCommandDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int Governorate { get; set; }
        public int City { get; set; }
        public int District { get; set; }
    }

    // Handler
    public class EditTicketCommandHandler : ResponseHandler,
        IRequestHandler<EditTicketCommand, Response<string>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EditTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<Response<string>> Handle(EditTicketCommand request, CancellationToken cancellationToken)
        {
            //Get Ticket From Database
            var ticket = await unitOfWork.TicketRepository.GetFirstOrDefaultAsync(filter: x => x.Id == request.Ticket.Id);

            if (ticket == null)
                return NotFound<string>("Ticket Not Found");

            //maping data  
            mapper.Map(request.Ticket, ticket);

            unitOfWork.TicketRepository.Update(ticket);
            await unitOfWork.CompleteAsync();
            return EditSuccess("Updated Successfully");
        }


    }

    // Mapper
    public sealed class CreateTicketProfile : Profile
    {
        public CreateTicketProfile()
        {
            CreateMap<EditTicketCommandDto, Ticket>().ReverseMap();
        }
    }

}
