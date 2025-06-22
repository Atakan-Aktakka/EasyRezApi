using Microsoft.AspNetCore.Mvc;
using MediatR;
using EasyRez.Application.Reservation.Commands.ServiceCommands;
using EasyRez.Application.Reservation.Queries.ServiceQueries;
using EasyRez.Application.Reservation.Common.Models;

namespace EasyRez.Api.Controllers.Reservation
{
    [Route("api/reservation/service")]
    public class ServiceController : ApiController
    {
        private readonly ISender _mediator;

        public ServiceController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetServiceQuery(id));
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>>> GetAll()
        {
            var result = await _mediator.Send(new ListServicesQuery());
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<ServiceDto>>> GetByCustomer(Guid customerId)
        {
            var result = await _mediator.Send(new ListServicesByCustomerQuery(customerId));
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateServiceCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                response => Ok(new { id = response }),
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] UpdateServiceCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            
            var result = await _mediator.Send(command);
            return result.Match(
                response => Ok(new { id = response }),
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteServiceCommand(id));
            return result.Match(
                response => Ok(),
                Problem
            );
        }
    }
} 