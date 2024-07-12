using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Domain.Models;

namespace TicketsHandling.Application.Features.Tickets.Query
{
    //Query
    public class GetByIdQuery : IRequest<Response<TicketDto>>
    {
        public int Id { get; set; }

        public GetByIdQuery(int id)
        {
            Id = id;
        }
    }

    //DTO
    public class TicketDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int Governorate { get; set; }
        public int City { get; set; }
        public int District { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsHandled { get; set; }
        public string StatusColor { get; set; }
    }

    //Handler
    public class GetByIdQueryHandler : ResponseHandler, IRequestHandler<GetByIdQuery, Response<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<TicketDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);

            if (ticket == null)
            {
                return NotFound<TicketDto>("Ticket not found");
            }

            var ticketDto = _mapper.Map<TicketDto>(ticket);
            return Success(ticketDto, "Ticket retrieved successfully");
        }
    }
  
    //Mapper
    public class GetByIdQueryProfile : Profile
    {
        public GetByIdQueryProfile()
        {
            CreateMap<Ticket, TicketDto>();
        }
    }
}
