using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.SharedModels;

namespace TicketsHandling.Application.Features.LookupData.Query.GetCities
{
    public class GetCitiesQuery : IRequest<Common.SharedModels.Response<List<LookupItemDto>>>
    {
        public int GovernorateId { get; set; }
    }
    // Get Cities
    public class GetCitiesQueryHandler : ResponseHandler,
    IRequestHandler<GetCitiesQuery, Common.SharedModels.Response<List<LookupItemDto>>>
    {
        private readonly LookupsData _lookupsData;

        public GetCitiesQueryHandler(IOptions<LookupsData> lookupsData)
        {
            _lookupsData = lookupsData.Value;
        }

        public Task<Common.SharedModels.Response<List<LookupItemDto>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = _lookupsData.Cities
                .Where(c => c.GovernorateId == request.GovernorateId)
                .Select(c => new LookupItemDto { Id = c.Id, Name = c.Name })
                .ToList();

            if (cities.Count == 0)
                return Task.FromResult(Success(new List<LookupItemDto>(), "Cities not found"));

            return Task.FromResult(Success(cities, "Cities retrieved successfully"));
        }
    }

}
