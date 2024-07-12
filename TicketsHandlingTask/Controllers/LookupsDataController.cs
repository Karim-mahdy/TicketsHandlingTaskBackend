
using Microsoft.AspNetCore.Http;

using TicketsHandling.Application.Features.LookupData.Query;

using TicketsHandlingTask.API.Base;

namespace TicketsHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsDataController : ControllerMain
    {
        private readonly IMediator _mediator;

        public LookupsDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("governorates")]
        public async Task<IActionResult> GetGovernorates()
            => GetResponse(await _mediator.Send(new GetGovernoratesQuery()));

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities([FromQuery] int governorateId)
            => GetResponse(await _mediator.Send(new GetCitiesQuery { GovernorateId = governorateId }));

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts([FromQuery] int cityId)
            => GetResponse(await _mediator.Send(new GetDistrictsQuery { CityId = cityId }));
    }
}
