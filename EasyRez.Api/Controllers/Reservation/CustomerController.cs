using Microsoft.AspNetCore.Mvc;
using MediatR;
using EasyRez.Application.Reservation.Common.Models;
using EasyRez.Application.Reservation.Queries.CustomerQueries;
using EasyRez.Application.Reservation.Commands.CustomerCommands;

namespace EasyRez.Api.Controllers.Reservation
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ApiController
    {
        private readonly ISender _mediator;

        public CustomerController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetCustomerQuery(id));
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAll()
        {
            var result = await _mediator.Send(new ListCustomersQuery());
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            
            var result = await _mediator.Send(command);
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return result.Match(
                response => Ok(response),
                Problem
            );
        }
    }
} 