using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.SharedModels;

namespace TicketsHandling.Application.Features.LookupData.Query.GetDistricts
{

    public class GetDistrictsQuery : IRequest<Common.SharedModels.Response<List<LookupItemDto>>>
    {
        public int CityId { get; set; }
    }


    // Get Districts
    public class GetDistrictsQueryHandler : ResponseHandler,
       IRequestHandler<GetDistrictsQuery, Common.SharedModels.Response<List<LookupItemDto>>>
    {
        private readonly LookupsData _lookupsData;

        public GetDistrictsQueryHandler(IOptions<LookupsData> lookupsData)
        {
            _lookupsData = lookupsData.Value;
        }

        public Task<Common.SharedModels.Response<List<LookupItemDto>>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
        {
            var districts = _lookupsData.Districts
                .Where(d => d.CityId == request.CityId)
                .Select(d => new LookupItemDto { Id = d.Id, Name = d.Name })
                .ToList();

            if (districts.Count == 0)
                return Task.FromResult(Success(new List<LookupItemDto>(), "Districts not found"));

            return Task.FromResult(Success(districts, "Districts retrieved successfully"));
        }
    }
}
