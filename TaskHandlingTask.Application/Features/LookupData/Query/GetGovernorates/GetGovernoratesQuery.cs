using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.SharedModels;

namespace TicketsHandling.Application.Features.LookupData.Query.GetGovernorates
{
    public class GetGovernoratesQuery : IRequest<Common.SharedModels.Response<List<LookupItemDto>>> { }

    // Get Governorates
    public class GetGovernoratesQueryHandler : ResponseHandler,
        IRequestHandler<GetGovernoratesQuery, Common.SharedModels.Response<List<LookupItemDto>>>
    {
        private readonly LookupsData _lookupsData;

        public GetGovernoratesQueryHandler(IOptions<LookupsData> lookupsData)
        {
            _lookupsData = lookupsData.Value;
        }

        public Task<Common.SharedModels.Response<List<LookupItemDto>>> Handle(GetGovernoratesQuery request, CancellationToken cancellationToken)
        {
            var governorates = _lookupsData.Governorates.Select(g => new LookupItemDto { Id = g.Id, Name = g.Name }).ToList();
            if (governorates.Count == 0)
                return Task.FromResult(Success(new List<LookupItemDto>(), "Governorates not found"));
            return Task.FromResult(Success(governorates, "Governorates retrieved successfully"));
        }
    }

}
