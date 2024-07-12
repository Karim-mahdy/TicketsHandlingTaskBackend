using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.Abstraction.Repositories;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Domain.Models;
using TicketsHandling.Domain.Models.Base;

namespace TicketsHandling.Application.Features.Tickets.Query
{
    public class GetAllTicketsPaginatedQuery : IRequest<Response<PaginatedResult<GetAllTicketsPaginatedQueryDto>>>
    {
        public PaginationRequest PaginationRequest { get; set; }

        public GetAllTicketsPaginatedQuery(PaginationRequest paginationRequest)
        {
            PaginationRequest = paginationRequest;
        }
    }

    public class GetAllTicketsPaginatedQueryDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int GovernorateId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsHandled { get; set; }
        public string StatusColor { get; set; }
    }
    public class GetAllTicketsPaginatedQueryHandler : ResponseHandler,
     IRequestHandler<GetAllTicketsPaginatedQuery, Response<PaginatedResult<GetAllTicketsPaginatedQueryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<LookupsData> _lookupsData;

        public GetAllTicketsPaginatedQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IOptions<LookupsData> lookupsData)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _lookupsData = lookupsData;
        }

        public async Task<Response<PaginatedResult<GetAllTicketsPaginatedQueryDto>>> Handle(GetAllTicketsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.TicketRepository.GetAllAsync(
              
                take: request.PaginationRequest.PageSize,
                skip: (request.PaginationRequest.PageNumber - 1) * request.PaginationRequest.PageSize);

            if (tickets == null || !tickets.Any())
            {
                return NotFound<PaginatedResult<GetAllTicketsPaginatedQueryDto>>("No Tickets Found");
            }

            var totalCount = await _unitOfWork.TicketRepository.CountAsync();

            var ticketDtos = _mapper.Map<IEnumerable<GetAllTicketsPaginatedQueryDto>>(tickets).ToList();

            foreach (var ticketDto in ticketDtos)
            {
                var governorate = _lookupsData.Value.Governorates.FirstOrDefault(g => g.Id == ticketDto.GovernorateId);
                var city = _lookupsData.Value.Cities.FirstOrDefault(c => c.Id == ticketDto.CityId);
                var district = _lookupsData.Value.Districts.FirstOrDefault(d => d.Id == ticketDto.DistrictId);

                ticketDto.Governorate = governorate?.Name ?? "Unknown";
                ticketDto.City = city?.Name ?? "Unknown";
                ticketDto.District = district?.Name ?? "Unknown";
            }


            var paginatedResult = new PaginatedResult<GetAllTicketsPaginatedQueryDto>(
                succeeded: true,
                data: ticketDtos,
                count: totalCount,
                page: request.PaginationRequest.PageNumber,
                pageSize: request.PaginationRequest.PageSize
            );

            return Success(paginatedResult, "All Tickets Retrieved Successfully");
        }
    }
    public class GetAllTicketsPaginatedQueryProfile : Profile
    {
        public GetAllTicketsPaginatedQueryProfile()
        {
            CreateMap<Ticket, GetAllTicketsPaginatedQueryDto>()
            .ForMember(x => x.GovernorateId, x => x.MapFrom(y => y.Governorate))
            .ForMember(x => x.CityId, x => x.MapFrom(y => y.City))
            .ForMember(x => x.DistrictId, x => x.MapFrom(y => y.District));
        }
    }


}
