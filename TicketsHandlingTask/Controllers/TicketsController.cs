using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsHandling.Application.Features.Tickets.Command;
using TicketsHandling.Application.Features.Tickets.Command.CreateTicket;
using TicketsHandling.Application.Features.Tickets.Command.DeleteTicket;
using TicketsHandling.Application.Features.Tickets.Command.EditTicket;
using TicketsHandling.Application.Features.Tickets.Command.HandleTicket;
using TicketsHandling.Application.Features.Tickets.Query;


namespace TicketsHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerMain
    {
        private readonly IMediator _mediator;
        public TicketsController(IMediator mediator)
        => _mediator = mediator;

        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets([FromQuery] PaginationRequest paginationRequest)
          => GetResponse(await _mediator.Send(new GetAllTicketsPaginatedQuery(paginationRequest)));

        [HttpGet("GetTicketById/{id}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int id)
          => GetResponse(await _mediator.Send(new GetByIdQuery(id)));

        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
          => GetResponse(await _mediator.Send(command));

        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] EditTicketCommand command)
          => GetResponse(await _mediator.Send(command));

        [HttpPut("handle/{id}")]
        public async Task<IActionResult> HandleTicket([FromRoute] int id)
          => GetResponse(await _mediator.Send(new HandleTicketCommand(id)));

        [HttpDelete("DeleteTicket/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
          => GetResponse(await _mediator.Send(new DeleteTicketCommand(id)));

    }
}
