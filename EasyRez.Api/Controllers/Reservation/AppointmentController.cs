using Microsoft.AspNetCore.Mvc;
using MediatR;
using EasyRez.Application.Reservation.Commands.AppointmentCommands;
using EasyRez.Application.Reservation.Queries.AppointmentQueries;
using EasyRez.Application.Reservation.Common.Models;

namespace EasyRez.Api.Controllers.Reservation
{
    [Route("api/reservation/appoinment")]
    public class AppointmentController : ApiController
    {
        private readonly ISender _mediator;

        public AppointmentController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetAppointmentQuery(id));

            return result.Match(
                data => Ok(data),
                Problem
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentDto>>> GetAll()
        {
            var result = await _mediator.Send(new ListAppointmentsQuery());
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<AppointmentDto>>> GetByCustomer(Guid customerId)
        {
            var result = await _mediator.Send(new ListAppointmentsByCustomerQuery(customerId));
            return result.Match(
                response => Ok(response),
                Problem
            );
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                response => Ok(new { id = response }),
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] UpdateAppointmentCommand command)
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
            var result = await _mediator.Send(new DeleteAppointmentCommand(id));
            return result.Match(
                response => Ok(),
                Problem
            );
        }
    }
} 