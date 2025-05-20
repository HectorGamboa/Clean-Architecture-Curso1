using CleanArchitecture.Application.Rentals.GetRental;
using CleanArchitecture.Application.Rentals.RentalReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controller.Rentals
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly ISender _sender;
        public RentalsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlquiler(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetRentalQuery(id);
            var resultado = await _sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado.Value) : NotFound(resultado.Error);
        }

        [HttpPost]
        public async Task<IActionResult> RentalReservation(Guid id, RentalRequest request, CancellationToken cancellationToken)
        {
            var command = new RentalReservationCommand(
                request.VehicleId,
                request.UserId,
                request.StartDate,
                request.EndDate
            );
            
            var resultado = await _sender.Send(command, cancellationToken);
            if (resultado.IsFailure)
            {
                return BadRequest(resultado.Error);
            }
       
            return CreatedAtAction(nameof(GetAlquiler), new { id = resultado.Value }, resultado.Value);
            
        }

    }
}